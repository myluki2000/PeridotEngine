﻿using Microsoft.Xna.Framework;
using PeridotEngine.Scenes.Scene3D;

namespace PeridotWindows.ECS.Components
{
    public sealed partial class PositionRotationScaleComponent : IComponent
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

        public Matrix Transformation
        {
            get
            {
                if (matrixOutdated)
                {
                    transformation = Matrix.CreateTranslation(position)
                                     * Matrix.CreateRotationX(rotation.X)
                                     * Matrix.CreateRotationY(rotation.Y)
                                     * Matrix.CreateRotationZ(rotation.Z)
                                     * Matrix.CreateScale(scale);
                }

                return transformation;
            }
        }

        public Scene3D Scene { get; }

        private Vector3 position;
        private Vector3 rotation;
        private Vector3 scale = Vector3.One;
        private Matrix transformation = Matrix.Identity;

        private bool matrixOutdated = false;
    }
}
