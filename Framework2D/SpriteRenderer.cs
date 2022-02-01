using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Fantasy.Framework2D
{  
    public class SpriteRenderer
    {   
        public SpriteBatch SpriteBatch
        {
            get => _spriteBatch;
        }
        
        private SpriteRenderState _default;
        private SpriteBatch _spriteBatch;
        public OrthographicCamera Camera { get; protected set; }
        public List<ISpriteBatchDrawable> SpriteBatchDrawables { get; protected set; }

        public SpriteRenderer(GraphicsDevice device, SpriteRenderState defaultState = null) 
        {
            _spriteBatch = new SpriteBatch(device);
            _default = defaultState ?? new SpriteRenderState() { SpriteSortMode = SpriteSortMode.Texture };
            SpriteBatchDrawables = new List<ISpriteBatchDrawable>();
        }

        public void Begin(OrthographicCamera camera = null)
        {
            SpriteBatchDrawables.Clear();
            Camera = camera;
        }

        public void Draw(ISpriteBatchDrawable drawable)
        {
            if( Camera != null && 
                drawable.BoundingRectangle != null && 
                Camera.Contains(drawable.BoundingRectangle) != ContainmentType.Disjoint) {
                SpriteBatchDrawables.Add(drawable);
            }       
        }

        public void End(GameTime gameTime)
        {
            foreach(var group in SpriteBatchDrawables.GroupBy(n => n.RenderState ?? _default)) {
                _spriteBatch.Begin(
                    group.Key.SpriteSortMode,
                    group.Key.BlendState,
                    group.Key.SamplerState,
                    group.Key.DepthStencilState,
                    group.Key.RasterizerState,
                    group.Key.Effect,
                    Camera == null ? Matrix.Identity : Camera.GetViewMatrix()
                );

                foreach(var item in group) {
                    item.DrawTo(gameTime, _spriteBatch);
                }
                _spriteBatch.End();
            }
        }
    }
}
