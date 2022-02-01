using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Aseprite.Graphics;
using MonoGame.Aseprite.Documents;
using MonoGame.Extended.Collisions;
using Fantasy.Framework2D;

namespace Fantasy.Game
{
    public class Creature : SpriteBatchDrawable, ICollisionActor
    {
        //public Activity Activity;
         Microsoft.Xna.Framework.Game _game;
        private AnimatedSprite _sprite;
        public IShapeF Bounds 
        { 
            get
            {
                return _bounds;
            } 
        }

        public float MoveSpeed { get; set; }
        
        RectangleF _bounds;

        public Creature(Microsoft.Xna.Framework.Game game)
        {
            _game = game;
            MoveSpeed = 1.0f;

        }
        public void Initialize(string name)
        {
            AsepriteDocument aseprite = _game.Content.Load<AsepriteDocument>(name);
            _sprite = new AnimatedSprite(aseprite);
            RenderState = new SpriteRenderState() {
                BlendState = BlendState.NonPremultiplied
            };
        }       

        public void Move(Vector2 move)
        {
            _sprite.Position += move * MoveSpeed;
        }
        public void Update(GameTime gameTime)
        {
            _bounds = new RectangleF(_sprite.X, _sprite.Y, _sprite.Width, _sprite.Height); 

            _sprite.Update(gameTime);
        }
        public override void DrawTo(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _sprite.Render(spriteBatch);
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            _sprite.Position -= collisionInfo.PenetrationVector;
        }
    }
}