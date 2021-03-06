﻿#nullable enable

using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeridotEngine.Engine.Graphics;
using PeridotEngine.Engine.Resources;
using PeridotEngine.Engine.Utility;
using PeridotEngine.Engine.World.Physics;

namespace PeridotEngine.Engine.World.WorldObjects.Entities
{
    abstract class Character : Sprite, IEntity, IPhysicsObject, IRenderedObject
    {
        public abstract string Name { get; set; }
        public abstract Level? Level { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }

        public abstract float MaxSpeed { get; set; }

        /// <inheritdoc />
        public abstract float Drag { get; set; }
        public bool HasPhysics { get; set; }

        /// <inheritdoc />
        public bool IsGrounded { get; set; }
        public Rectangle BoundingRect => new Rectangle(Position.ToPoint(), Size.ToPoint());
        /// <inheritdoc />
        public string? Id { get; set; }
        /// <inheritdoc />
        public string? Class { get; set; }
        public abstract void Initialize(Level level);
        public new abstract void Update(GameTime gameTime);

        public virtual bool ContainsPointOnScreen(Point point, Camera camera)
        {
            return BoundingRect.Contains(point.Transform(camera.GetMatrix().Invert()));
        }

        /// <inheritdoc />
        public bool DisableBatching => false;

        /// <inheritdoc />
        public void Draw(SpriteBatch sb, Camera camera, Material.TextureType texType = Material.TextureType.Diffuse)
        {
            if (Material != null)
            {
                base.Draw(sb, texType);
            }
            else if(texType == Material.TextureType.Diffuse)
            {
                DrawOutline(sb, Color.Red, camera);
            }
        }

        /// <inheritdoc />
        public virtual void DrawOutline(SpriteBatch sb, Color color, Camera camera)
        {
            Utility.Utility.DrawOutline(sb, BoundingRect.Transform(camera.GetMatrix()), color);
        }

        /// <inheritdoc />
        public abstract XElement ToXml(LazyLoadingMaterialDictionary materialDictionary);
    }
}
