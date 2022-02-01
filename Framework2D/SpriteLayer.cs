using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fantasy.Framework2D
{
    public class SpriteLayer : Drawable
    {      
        public SpriteBatch Graphics
        {
            get => _spriteBatch;
        }

        public SpriteRenderState RenderState 
        {
            get => _spriteRenderState;
        }
        
        private SpriteBatch _spriteBatch;
        private SpriteRenderState _spriteRenderState;
        public List<ISpriteBatchDrawable> _sprites;
        public SpriteLayer(GraphicsDevice graphicsDevice) 
        {
            _spriteRenderState = new SpriteRenderState();
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _sprites = new List<ISpriteBatchDrawable>();
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Visible) return;

            _spriteBatch.Begin(
                _spriteRenderState.SpriteSortMode,
                _spriteRenderState.BlendState,
                _spriteRenderState.SamplerState,
                _spriteRenderState.DepthStencilState,
                _spriteRenderState.RasterizerState,
                _spriteRenderState.Effect,
                null
                );

            foreach (var drawable in _sprites)
            {
                drawable.DrawTo(gameTime, _spriteBatch);
            }

            _spriteBatch.End();
        }
    }
}
