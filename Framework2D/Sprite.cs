using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Framework2D
{
    /*public class Sprite : SpriteBatchDrawable
    {        
        public Material Material
        {
            get; 
            set;
        }
        
        public Rectangle Bounds
        {
            get; 
            set;
        }

        public Color AmbientColor 
        {
            get; 
            set;
        }

        public SpriteEffects SpriteEffects {
            get;
            set;
        }

        public Vector2 Location
        {
            get { return new Vector2(Bounds.Location.X, Bounds.Location.Y); }
            set { Bounds = new Rectangle((int)value.X, (int)value.Y, (int)Size.X, (int)Size.Y); }
        }

        public Vector2 Size
        {
            get { return new Vector2(Bounds.Size.X, Bounds.Size.Y); }
            set { Bounds = new Rectangle((int)Location.X, (int)Location.Y, (int)value.X, (int)value.Y); }
        }

        public float Depth
        {
            get; 
            set;
        }      

        public float Rotation 
        {
            get; 
            set;
        }

        public float Scale
        {
            get;
            set;
        }

        public Sprite() : this(new Material()) 
        {
            AmbientColor = Color.White;
        }

        public Sprite(Material material) {
            Material = material;
            Bounds = new Rectangle(0, 0, 32, 32);
            Scale = 1.0f;
        }

        protected Rectangle GetProjectedBounds() 
        {
            return new Rectangle(
                Bounds.X + (int)((Bounds.Width * .5f)),
                Bounds.Y + (int)((Bounds.Height * .5f)),
                (int)(Bounds.Height * Scale),
                (int)(Bounds.Width * Scale));
        }

        protected Vector2 GetCenterOffset(Rectangle source)
        {
            return new Vector2(source.X + source.Width * .5f, source.Y + source.Height * .5f );
        }

        public override void DrawTo(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(!Visible) return;
                        
            spriteBatch.Draw(
                Material.ColorMap.Texture2D, 
                GetProjectedBounds(),
                Material.ColorMap.View,
                new Color(AmbientColor, Material.Alpha), 
                Rotation, 
                GetCenterOffset(Material.ColorMap.View),
                SpriteEffects,
                Depth);
        }
    }*/
}
