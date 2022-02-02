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
using MonoGame.Extended.Sprites;
using System.Linq;

namespace Fantasy.Game
{
    public enum Orientation
    {
        Up,
        Down,
        Left,
        Right
    };

    public class MapTileReticule : SpriteBatchDrawable
    {
        //public Activity Activity;
        Microsoft.Xna.Framework.Game _game;
        public Vector2 Position { get; set; }
        public IShapeF Bounds 
        { 
            get
            {
                return _bounds;
            } 
        }
        
        RectangleF _bounds;

        public MapTileReticule(Microsoft.Xna.Framework.Game game)
        {
            _game = game;

        }
        public void Initialize()
        {
            
        }       
       
        public void Update(GameTime gameTime)
        {
            
        }

        public override void DrawTo(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(
               Position, new Size2(32, 32), Color.Blue, 2, 0
            );
        }
    }

    public class Creature : SpriteBatchDrawable, ICollisionActor
    {
        //public Activity Activity;
        Microsoft.Xna.Framework.Game _game;
        private MonoGame.Aseprite.Graphics.AnimatedSprite _sprite;
        public IShapeF Bounds 
        { 
            get
            {
                return _bounds;
            } 
        }

        public float MoveSpeed { get; set; }

        public Orientation Orientation { get; set; }

        public Vector2 GetFacingDirection(int tileSize = 32)
        {
            var position = Bounds.Position + _sprite.Origin;

            switch(Orientation) {
                case Orientation.Up:
                    position.Y -= tileSize / 2;
                    break;
                case Orientation.Down:
                    position.Y += (int)(tileSize / 2);
                    break;
                case Orientation.Left:
                    position.X -= tileSize / 2;
                    break;
                case Orientation.Right:
                    position.X += (int)(tileSize / 2);
                    break;
            }

            return new Vector2(
                (int)(position.X / tileSize) * tileSize,
                (int)(position.Y / tileSize) * tileSize);
        }
        
        RectangleF _bounds;

        public Creature(Microsoft.Xna.Framework.Game game)
        {
            _game = game;
            MoveSpeed = 3.0f;

        }
        public void Initialize(string name)
        {
            AsepriteDocument aseprite = _game.Content.Load<AsepriteDocument>(name);
            _sprite = new MonoGame.Aseprite.Graphics.AnimatedSprite(aseprite);
            RenderState = new SpriteRenderState() {
                BlendState = BlendState.NonPremultiplied
            };
        }       

        public void Move(Vector2 move)
        {
            _sprite.Position += move * MoveSpeed;
            
            if(move.X > 0) Orientation = Orientation.Right;
            else if(move.X < 0) Orientation = Orientation.Left;
            else if(move.Y < 0) Orientation = Orientation.Up;
            else if(move.Y > 0) Orientation = Orientation.Down;
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