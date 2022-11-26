using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotEngine.ECS.Components
{
    public sealed partial class PositionRotationScaleComponent : ComponentBase
    {
        public Vector3 Position
        {
            get => position;
            set
            {
                position = value;
                matrixOutdated = true;
            }
        }

        public Vector3 Rotation
        {
            get => rotation;
            set
            {
                rotation = value;
                matrixOutdated = true;
            }
        }

        public Vector3 Scale
        {
            get => scale;
            set
            {
                scale = value;
                matrixOutdated = true;
            }
        }

        [JsonIgnore]
        public Matrix Transformation
        {
            get
            {
                if (matrixOutdated)
                {
                    transformation = Matrix.CreateScale(scale)
                                     * Matrix.CreateRotationX(rotation.X)
                                     * Matrix.CreateRotationY(rotation.Y)
                                     * Matrix.CreateRotationZ(rotation.Z)
                                     * Matrix.CreateTranslation(position);
                }

                return transformation;
            }
        }

        public PositionRotationScaleComponent(Scene3D scene) : base(scene)
        {
        }

        private Vector3 position;
        private Vector3 rotation;
        private Vector3 scale = Vector3.One;
        private Matrix transformation = Matrix.Identity;

        private bool matrixOutdated = true;
    }
}
