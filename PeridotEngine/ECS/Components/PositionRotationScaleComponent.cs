using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using PeridotEngine.Scenes.Scene3D;
using PeridotWindows.ECS;
using PeridotWindows.ECS.Components.PropertiesControls;

namespace PeridotEngine.ECS.Components
{
    public sealed partial class PositionRotationScaleComponent(Scene3D scene) : ComponentBase(scene)
    {
        public Vector3 Position
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;
                matrixOutdated = true;
                RaiseValuesChanged();
            }
        }

        public Quaternion Rotation
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;
                matrixOutdated = true;
                RaiseValuesChanged();
            }
        }

        public Vector3 Scale
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;
                matrixOutdated = true;
                RaiseValuesChanged();
            }
        } = Vector3.One;

        /// <summary>
        /// A counter which is incremented each time the transformation matrix is updated. Can be
        /// used by other systems to check if the matrix has been updated since last retrieval.
        /// Overflows back to 0 when it reaches uint.MaxValue.
        /// </summary>
        [JsonIgnore]
        public ulong MatrixVersion { get; private set; } = 0;

        [JsonIgnore]
        public bool HasParent => ParentEntityId != null;

        public uint? ParentEntityId
        {
            get;
            set
            {
                if (field == value)
                    return;

                field = value;
                parentEntityPosRotScaleComponent = null;
                parentEntityMatrixVersion = 0;
                matrixOutdated = true;
                RaiseValuesChanged();
            }
        }

        private PositionRotationScaleComponent? parentEntityPosRotScaleComponent;
        private ulong parentEntityMatrixVersion = 0;

        [JsonIgnore]
        public Matrix Transformation
        {
            get
            {
                bool refresh = matrixOutdated 
                               || (parentEntityPosRotScaleComponent != null 
                                   && parentEntityMatrixVersion != parentEntityPosRotScaleComponent.MatrixVersion);

                if (!refresh) return field;

                field = Matrix.CreateScale(Scale)
                        * Matrix.CreateFromQuaternion(Rotation)
                        //* Matrix.CreateFromYawPitchRoll(rotation.Y, rotation.X, rotation.Z)
                        * Matrix.CreateTranslation(Position);

                if (ParentEntityId != null)
                {
                    if (parentEntityPosRotScaleComponent == null)
                    {
                        parentEntityPosRotScaleComponent = Scene.Ecs.EntityById(ParentEntityId.Value)
                            ?.GetComponent<PositionRotationScaleComponent>();
                    }
                    field *= parentEntityPosRotScaleComponent.Transformation;
                    parentEntityMatrixVersion = parentEntityPosRotScaleComponent.MatrixVersion;
                }

                // unchecked increment by 1, overflows back to 0 when it reaches uint.MaxValue
                MatrixVersion = unchecked(MatrixVersion + 1);

                return field;
            }
        } = Matrix.Identity;

        private bool matrixOutdated = true;
    }
}
