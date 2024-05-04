using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components.PropertiesControls;

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
                ValuesChanged?.Invoke(this, this);
            }
        }

        public Quaternion Rotation
        {
            get => rotation;
            set
            {
                rotation = value;
                matrixOutdated = true;
                ValuesChanged?.Invoke(this, this);
            }
        }

        public Vector3 Scale
        {
            get => scale;
            set
            {
                scale = value;
                matrixOutdated = true;
                ValuesChanged?.Invoke(this, this);
            }
        }

        /// <summary>
        /// A counter which is incremented each time the transformation matrix is updated. Can be
        /// used by other systems to check if the matrix has been updated since last retrieval.
        /// Overflows back to 0 when it reaches uint.MaxValue.
        /// </summary>
        [JsonIgnore]
        public uint MatrixVersion { get; private set; } = 0;

        [JsonIgnore] public bool HasParent => parentEntityPosRotScaleComponent != null;

        public uint? ParentEntityId
        {
            get => parentEntityId;
            set
            {
                parentEntityId = value;
                parentEntityPosRotScaleComponent = null;
                parentEntityMatrixVersion = 0;
                matrixOutdated = true;
                ValuesChanged?.Invoke(this, this);
            }
        }

        public event EventHandler<PositionRotationScaleComponent>? ValuesChanged; 

        private PositionRotationScaleComponent? parentEntityPosRotScaleComponent;
        private uint parentEntityMatrixVersion = 0;
        private uint? parentEntityId;

        [JsonIgnore]
        public Matrix Transformation
        {
            get
            {
                bool refresh = matrixOutdated 
                               || (parentEntityPosRotScaleComponent != null 
                                   && parentEntityMatrixVersion != parentEntityPosRotScaleComponent.MatrixVersion);

                if (!refresh) return transformation;

                transformation = Matrix.CreateScale(scale)
                                 * Matrix.CreateFromQuaternion(rotation)
                                 //* Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z)
                                 * Matrix.CreateTranslation(position);

                if (parentEntityId != null)
                {
                    if (parentEntityPosRotScaleComponent == null)
                    {
                        parentEntityPosRotScaleComponent = Scene.Ecs.EntityById(parentEntityId.Value)
                            ?.GetComponent<PositionRotationScaleComponent>();
                    }
                    transformation *= parentEntityPosRotScaleComponent.Transformation;
                    parentEntityMatrixVersion = parentEntityPosRotScaleComponent.MatrixVersion;
                }

                // unchecked increment by 1, overflows back to 0 when it reaches uint.MaxValue
                MatrixVersion = unchecked(MatrixVersion + 1);

                return transformation;
            }
        }

        public PositionRotationScaleComponent(Scene3D scene) : base(scene)
        {
        }

        private Vector3 position;
        private Quaternion rotation;
        private Vector3 scale = Vector3.One;
        private Matrix transformation = Matrix.Identity;

        private bool matrixOutdated = true;
    }
}
