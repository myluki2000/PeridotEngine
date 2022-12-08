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

        private readonly Query sunLights;

        private RenderTarget2D? rt;

        public SunShadowMapSystem(Scene3D scene)
        {
            this.scene = scene;

            sunLights = scene.Ecs.Query().Has<SunLightComponent>()
                                         .Has<PositionRotationScaleComponent>();
        }

        public Texture2D? GenerateShadowMap(out Vector3 lightPosition, out Matrix lightViewProjection)
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

            GraphicsDevice gd = Globals.Graphics.GraphicsDevice;

            if(rt == null) 
                rt = new(gd, 1920,
                         1080, false, SurfaceFormat.Single, DepthFormat.Depth24);

            gd.SetRenderTarget(rt);
            gd.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.White, float.MaxValue, 0);

            gd.DepthStencilState = DepthStencilState.Default;

            Camera camera = new OrthographicCamera();

            sunLights.ForEach((PositionRotationScaleComponent posC) =>
            {
                camera.Position = posC.Position;
                camera.Yaw = posC.Rotation.Y;
                camera.Pitch = posC.Rotation.X;
                camera.Roll = posC.Rotation.Z;
            });

            EffectBase depthEffect = scene.Resources.EffectPool.Effect<DepthEffect>();
            depthEffect.ViewProjection = camera.GetViewMatrix() * camera.GetProjectionMatrix();
            
            scene.MeshRenderingSystem.Meshes.ForEach((StaticMeshComponent meshC, PositionRotationScaleComponent posC) =>
            {
                if (!meshC.CastShadows) return;
                scene.MeshRenderingSystem.RenderMesh(meshC, posC, depthEffect);
            });

            gd.SetRenderTarget(null);
            
            lightViewProjection = depthEffect.ViewProjection;

            // for sunlights we don't use the light's actual position and instead use a sufficiently far away position in the opposite
            // direction of the light's ray direction, calculated from the world origin (0, 0, 0)
            lightPosition = camera.GetLookDirection() * -1000000;
            return rt;
        }

        ~SunShadowMapSystem()
        {
            // not sure if this is necessary, but just do it to make sure we don't get a VRAM memory leak
            rt?.Dispose();
        }
    }
}
