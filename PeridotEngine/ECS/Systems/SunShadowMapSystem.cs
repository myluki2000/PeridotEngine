using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.ECS.Components;
using PeridotEngine.Graphics.Cameras;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components;
using Color = Microsoft.Xna.Framework.Color;

namespace PeridotEngine.ECS.Systems
{
    public class SunShadowMapSystem
    {
        private readonly Scene3D scene;

        private readonly ComponentQuery<PositionRotationScaleComponent> sunLights;

        private RenderTarget2D? rt;

        public SunShadowMapSystem(Scene3D scene)
        {
            this.scene = scene;

            sunLights = scene.Ecs.Query().Has<SunLightComponent>()
                                         .Has<PositionRotationScaleComponent>()
                                         .OnComponents<PositionRotationScaleComponent>();
        }

        public Texture2D? GenerateShadowMap(MeshRenderingSystem meshRenderingSystem, out Vector3 lightPosition, out Matrix lightViewProjection)
        {
            switch (sunLights.EntityCount)
            {
                case > 1:
                    throw new Exception("At most 1 sun can exist per scene.");
                case 0:
                    lightViewProjection = Matrix.Identity;
                    lightPosition = Vector3.Zero;
                    return null;
            }

            GraphicsDevice gd = Globals.GraphicsDevice;

            if(rt == null || rt.IsDisposed) 
                rt = new(gd, 1920,
                         1080, false, SurfaceFormat.Single, DepthFormat.Depth24);

            RenderTargetBinding[] originalRenderTargets = gd.GetRenderTargets();
            gd.SetRenderTarget(rt);
            gd.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.White, float.MaxValue, 0);

            gd.DepthStencilState = DepthStencilState.Default;
            gd.BlendState = BlendState.Opaque;

            Camera camera = new OrthographicCamera();

            sunLights.ForEach((uint _, PositionRotationScaleComponent posC) =>
            {
                camera.Position = posC.Position;
                camera.Yaw = posC.Rotation.Y;
                camera.Pitch = posC.Rotation.X;
                camera.Roll = posC.Rotation.Z;
            });

            EffectBase depthEffect = scene.Resources.EffectPool.Effect<DepthEffect>();
            depthEffect.View = camera.GetViewMatrix();
            depthEffect.Projection = camera.GetProjectionMatrix();
            
            meshRenderingSystem.Meshes.ForEach((uint entityId, StaticMeshComponent meshC, PositionRotationScaleComponent posC) =>
            {
                if (!meshC.CastShadows) return;
                meshRenderingSystem.RenderMesh(entityId, meshC, posC, depthEffect);
            });

            gd.SetRenderTargets(originalRenderTargets);
            
            lightViewProjection = depthEffect.View * depthEffect.Projection;

            // for sunlights we don't use the light's actual position and instead use a sufficiently far away position in the opposite
            // direction of the light's ray direction, calculated from the world origin (0, 0, 0)
            lightPosition = camera.GetLookDirection() * -1000000;
            return rt;
        }
    }
}
