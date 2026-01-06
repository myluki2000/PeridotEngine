using JoltPhysicsSharp;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using PeridotEngine.ECS.Components;
using PeridotEngine.Misc;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS;
using Quaternion = System.Numerics.Quaternion;

namespace PeridotEngine.ECS.Systems
{
    public class PhysicsSystem : IDisposable
    {
        public Scene3D Scene { get; }

        private readonly JobSystem _jobSystem;
        private readonly JoltPhysicsSharp.PhysicsSystem _system;
        private BodyInterface BodyInterface => _system.BodyInterface;
        private BodyLockInterface BodyLockInterface => _system.BodyLockInterface;

        private readonly Dictionary<uint, Body> _staticBodies = [];
        private readonly Dictionary<uint, Body> _dynamicBodies = [];

        private readonly ComponentQuery<StaticPhysicsPropComponent, PositionRotationScaleComponent> _staticObjectsQuery;
        private readonly ComponentQuery<DynamicPhysicsPropComponent, PositionRotationScaleComponent> _dynamicObjectsQuery;

        private PhysicsSystemSettings _settings = new PhysicsSystemSettings()
        {
            MaxBodies = 65536,
            MaxBodyPairs = 65536,
            MaxContactConstraints = 65536,
            NumBodyMutexes = 0,
        };

        public PhysicsSystem(Scene3D scene)
        {
            Scene = scene;

            _staticObjectsQuery = scene.Ecs.Query()
                .Has<StaticPhysicsPropComponent>()
                .Has<PositionRotationScaleComponent>()
                .OnComponents<StaticPhysicsPropComponent, PositionRotationScaleComponent>();
            _staticObjectsQuery.EntityListChanged.AddHandler(OnStaticEntitiesChanged);

            _dynamicObjectsQuery = scene.Ecs.Query()
                .Has<DynamicPhysicsPropComponent>()
                .Has<PositionRotationScaleComponent>()
                .OnComponents<DynamicPhysicsPropComponent, PositionRotationScaleComponent>();
            _dynamicObjectsQuery.EntityListChanged.AddHandler(OnDynamicEntitiesChanged);

            _jobSystem = new JobSystemThreadPool();
            
            SetupCollisionFiltering();
            _system = new JoltPhysicsSharp.PhysicsSystem(_settings);
        }

        private void SetupCollisionFiltering()
        {
            // We use only 2 layers: one for non-moving objects and one for moving objects
            ObjectLayerPairFilterTable objectLayerPairFilter = new(2);
            objectLayerPairFilter.EnableCollision(Layers.NonMoving, Layers.Moving);
            objectLayerPairFilter.EnableCollision(Layers.Moving, Layers.Moving);

            // We use a 1-to-1 mapping between object layers and broadphase layers
            BroadPhaseLayerInterfaceTable broadPhaseLayerInterface = new(2, 2);
            broadPhaseLayerInterface.MapObjectToBroadPhaseLayer(Layers.NonMoving, BroadPhaseLayers.NonMoving);
            broadPhaseLayerInterface.MapObjectToBroadPhaseLayer(Layers.Moving, BroadPhaseLayers.Moving);

            ObjectVsBroadPhaseLayerFilterTable objectVsBroadPhaseLayerFilter = new(broadPhaseLayerInterface, 2, objectLayerPairFilter, 2);

            _settings.ObjectLayerPairFilter = objectLayerPairFilter;
            _settings.BroadPhaseLayerInterface = broadPhaseLayerInterface;
            _settings.ObjectVsBroadPhaseLayerFilter = objectVsBroadPhaseLayerFilter;
        }

        public void Update(GameTime gameTime)
        {
            // bodies for new/deleted objects are added/removed in the OnObjectsChanged handlers, here we only need to check that the
            // properties of the bodies are up to date with the properties of the components
            _staticObjectsQuery.ForEach((uint entityId, StaticPhysicsPropComponent physC, PositionRotationScaleComponent posC) =>
            {
                if (physC.PhysicsBodyPropertiesOutdated)
                {
                    // TODO: Update body properties
                }
            });
            _dynamicObjectsQuery.ForEach((uint entityId, DynamicPhysicsPropComponent physC, PositionRotationScaleComponent posC) =>
            {
                if (physC.PhysicsBodyPropertiesOutdated)
                {
                    // TODO: Update body properties
                }
            });

            // TODO: This shouldn't be called every update and instead only after adding/removing bodies, as far as I understand (research this)
            //_system.OptimizeBroadPhase();

            PhysicsUpdateError error = _system.Update((float)gameTime.ElapsedGameTime.TotalSeconds, 1, _jobSystem);
            if (error != PhysicsUpdateError.None)
                throw new Exception("Physics error.");

            // update entity positions of physics bodies
            _dynamicObjectsQuery.ForEach((uint entityId, DynamicPhysicsPropComponent physC, PositionRotationScaleComponent posC) =>
            {
                Vector3 newPos = BodyInterface.GetPosition(_dynamicBodies[entityId].ID).ToXnaVector3();
                posC.Position = newPos;
            }); 
        }

        private void OnDynamicEntitiesChanged(object? sender, QueryEntityListChangedEventArgs e)
        {
            OnPhysicsEntitiesChanged(e, true);
        }

        private void OnStaticEntitiesChanged(object? sender, QueryEntityListChangedEventArgs e)
        {
            OnPhysicsEntitiesChanged(e, false);
        }

        private void OnPhysicsEntitiesChanged(QueryEntityListChangedEventArgs e, bool isDynamic)
        {
            Archetype.Entity entity = Scene.Ecs.EntityById(e.EntityId) ?? throw new Exception("Could not find entity with ID " + e.EntityId);
            switch (e.Operation)
            {
                case QueryEntityListChangedEventArgs.ChangeOperation.Added:
                    PositionRotationScaleComponent posC = entity.GetComponent<PositionRotationScaleComponent>();
                    if (isDynamic)
                    {
                        using BodyCreationSettings settings = new(
                            new BoxShape(new System.Numerics.Vector3(1.0f)),
                            posC.Position.ToNumericsVector3(),
                            new Quaternion(0, 0, 0, 1),
                            MotionType.Dynamic,
                            Layers.Moving);
                        Body body = BodyInterface.CreateBody(settings);
                        BodyInterface.AddBody(body.ID, Activation.Activate);
                        _dynamicBodies.Add(e.EntityId, body);
                    }
                    else
                    {
                        using BodyCreationSettings settings = new(
                            new BoxShape(new System.Numerics.Vector3(1.0f)),
                            posC.Position.ToNumericsVector3(),
                            new Quaternion(0, 0, 0, 1),
                            MotionType.Static,
                            Layers.Moving);
                        Body body = BodyInterface.CreateBody(settings);
                        BodyInterface.AddBody(body.ID, Activation.Activate);
                        _staticBodies.Add(e.EntityId, body);
                    }
                    break;
                case QueryEntityListChangedEventArgs.ChangeOperation.Removed:
                    if (isDynamic)
                    {
                        Body body = _dynamicBodies[entity.Id];
                        BodyInterface.RemoveAndDestroyBody(body.ID);
                        _dynamicBodies.Remove(entity.Id);
                    }
                    else
                    {
                        Body body = _staticBodies[entity.Id];
                        BodyInterface.RemoveAndDestroyBody(body.ID);
                        _staticBodies.Remove(entity.Id);
                    }
                    break;
                case QueryEntityListChangedEventArgs.ChangeOperation.ComponentsChanged:
                    // don't need to do anything here, the entity is still part of our query, we don't care that other
                    // components may have been added or removed
                    break;
                default:
                    throw new Exception();
            }
        }

        public void Dispose()
        {
            foreach (Body body in _staticBodies.Values)
            {
                BodyInterface.RemoveAndDestroyBody(body.ID);
            }
            _staticBodies.Clear();

            foreach (Body body in _dynamicBodies.Values)
            {
                BodyInterface.RemoveAndDestroyBody(body.ID);
            }
            _dynamicBodies.Clear();

            _jobSystem.Dispose();
            _system.Dispose();
        }

        protected static class Layers
        {
            public static readonly ObjectLayer NonMoving = 0;
            public static readonly ObjectLayer Moving = 1;
        };

        protected static class BroadPhaseLayers
        {
            public static readonly BroadPhaseLayer NonMoving = 0;
            public static readonly BroadPhaseLayer Moving = 1;
        };
        protected const int NumLayers = 2;
    }
}
