using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Aseprite.Graphics;
using MonoGame.Aseprite.Documents;
using Fantasy.Framework2D;
using MonoGame.Extended.Collisions;
using System.Linq;

namespace Fantasy.Game
{
    public class Area
    {
        protected Microsoft.Xna.Framework.Game _game;
        protected TiledMap _tiledMap;
        protected TiledMapRenderer _tiledMapRenderer;

        public Rectangle Bounds
        {
            get => new Rectangle(0, 0, _tiledMap.WidthInPixels, _tiledMap.HeightInPixels);
        }
       
        public Area(Microsoft.Xna.Framework.Game game)
        {
            _game = game;
        }

        public virtual void Initialize(string map) 
        {
            _tiledMap = _game.Content.Load<TiledMap>(map);
            _tiledMapRenderer = new TiledMapRenderer(_game.GraphicsDevice, _tiledMap); 
        }

        public virtual void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime); 
        }

        public virtual void Draw(GameTime gameTime, Matrix viewMatrix)
        {
            _game.GraphicsDevice.SamplerStates[0]   = SamplerState.PointClamp;
            _game.GraphicsDevice.BlendState         = BlendState.AlphaBlend;
            _tiledMapRenderer.Draw(viewMatrix);
        }
        
        public virtual List<ICollisionActor> GetCollisionActors() {
            return _tiledMap.GetCollisionActors();
        }
    }
}