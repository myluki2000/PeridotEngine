using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.ECS.Components;
using PeridotEngine.Graphics.Camera;
using PeridotEngine.Graphics.Effects;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components;

namespace PeridotEngine.ECS.Systems
{
    public class SunShadowMapSystem
    {
        private readonly Scene3D scene;

        private readonly Query sunLights;

        public SunShadowMapSystem(Scene3D scene)
        {
            this.scene = scene;

            sunLights = scene.Ecs.Query().Has<SunLightComponent>()
                                         .Has<PositionRotationScaleComponent>();
        }

        public Texture2D? GenerateShadowMap()
        {
            switch (sunLights.EntityCount)
            {
                case > 1:
                    throw new Exception("At most 1 sun can exist per scene.");
                case 0:
                    return null;
            }

            GraphicsDevice gd = Globals.Graphics.GraphicsDevice;

            RenderTarget2D rt = new(gd, Globals.Graphics.PreferredBackBufferWidth,
                Globals.Graphics.PreferredBackBufferHeight,
                false, SurfaceFormat.Single, DepthFormat.Depth24);

            gd.SetRenderTarget(rt);

            gd.DepthStencilState = DepthStencilState.Default;

            Camera camera = new();

            sunLights.ForEach((PositionRotationScaleComponent posC) =>
            {
                camera.Position = posC.Position;
                camera.Yaw = posC.Rotation.Y;
                camera.Pitch = posC.Rotation.X;
                camera.Roll = posC.Rotation.Z;
            });

            EffectBase depthEffect = scene.Resources.EffectPool.Effect<DepthEffect>();
            depthEffect.ViewProjection = camera.GetViewMatrix() * camera.GetProjectionMatrix();

            scene.MeshRenderingSystem.RenderMeshes(depthEffect);

            gd.SetRenderTarget(null);

            return rt;
        }
    }
}
