using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Fantasy.Framework2D
{
    public interface ISpriteBatchDrawable : IDrawable
    {
        SpriteRenderState RenderState { get; }
        Rectangle BoundingRectangle { get; }
        void DrawTo(GameTime gameTime, SpriteBatch spriteBatch);

        void IDrawable.Draw(GameTime gameTime) { throw new NotSupportedException(); }
    }

    public abstract class SpriteBatchDrawable : SimpleDrawableGameComponent, ISpriteBatchDrawable
    {        
        public SpriteRenderState RenderState { get; protected set; }
        public Rectangle BoundingRectangle { get; }
        public abstract void DrawTo(GameTime gameTime, SpriteBatch spriteBatch);
        public override void Draw(GameTime gameTime) { 
            throw new NotSupportedException(); 
        }
    }

}
