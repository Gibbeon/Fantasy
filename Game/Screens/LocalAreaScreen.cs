using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Input.InputListeners;
using Fantasy.Framework2D;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;
using Fantasy.Game.Actors;

namespace Fantasy.Game.Screens
{  
    public class LocalAreaScreen : GameScreen
    {
        public Fantasy.Game.World _world;        
        private KeyboardListener  _keyboardListener;        
        private OrthographicCamera _camera;
        private SpriteRenderer _spriteRenderer;  
        CollisionComponent _collisionComponent;
        //MapTileReticule _mapTileReticule;
        public LocalAreaScreen(Microsoft.Xna.Framework.Game game) : base (game)
        {
            
        }

        public override void Initialize() 
        {
            _camera         = new OrthographicCamera(Game.GraphicsDevice); 
            _spriteRenderer    = new SpriteRenderer(Game.GraphicsDevice);

           // _mapTileReticule = new MapTileReticule(Game);
            //_mapTileReticule.Initialize();

            _world          = new Game.World(Game);
            _world.Initialize(); 

            _collisionComponent = new CollisionComponent(_world.CurrentArea.Bounds);
            _collisionComponent.Initialize();

            _collisionComponent.Insert(_world.CurrentActor);

            _world.CurrentArea.GetCollisionActors().ForEach( n => _collisionComponent.Insert(n));

            _keyboardListener = new KeyboardListener( new KeyboardListenerSettings()
            {
                RepeatPress = true,
                InitialDelayMilliseconds = 0           
            });

            _keyboardListener.KeyPressed += MoveSprite;
            
        }
        protected void MoveCamera(object sender, KeyboardEventArgs args)
        {
            switch(args.Key)
            {
                case Microsoft.Xna.Framework.Input.Keys.Left:
                    _camera.Move(new Vector2(1, 0)); 
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Up:
                    _camera.Move(new Vector2(0, -1)); 
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Down:
                    _camera.Move(new Vector2(0, 1)); 
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Right:
                    _camera.Move(new Vector2(-1, 0)); 
                    break;                
                case Microsoft.Xna.Framework.Input.Keys.Escape:
                    Environment.Exit(0);
                    break;
            }
        }

        protected void MoveSprite(object sender, KeyboardEventArgs args)
        {
            switch(args.Key)
            {
                case Microsoft.Xna.Framework.Input.Keys.Left:
                    _world.CurrentActor.Walk(new Vector2(-1, 0)); 
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Up:
                    _world.CurrentActor.Walk(new Vector2(0, -1)); 
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Down:
                    _world.CurrentActor.Walk(new Vector2(0, 1)); 
                    break;                    
                case Microsoft.Xna.Framework.Input.Keys.Right:
                    _world.CurrentActor.Walk(new Vector2(1, 0)); 
                    break;
                    case Microsoft.Xna.Framework.Input.Keys.D1:
                    //(_world.CurrentArea as Fantasy.Game.Farm).TillAt(_world.CurrentActor.GetFacingDirection());
                    break;
                case Microsoft.Xna.Framework.Input.Keys.D2:
                    //(_world.CurrentArea as Fantasy.Game.Farm).WaterAt(_world.CurrentActor.GetFacingDirection());
                    break;
                case Microsoft.Xna.Framework.Input.Keys.D3:
                    //(_world.CurrentArea as Fantasy.Game.Farm).HarvestAt(_world.CurrentActor.GetFacingDirection());
                    break;
                case Microsoft.Xna.Framework.Input.Keys.D9:
                    //(_world.CurrentArea as Fantasy.Game.Farm).PlantAt("rose", _world.CurrentActor.GetFacingDirection());
                    break;
                case Microsoft.Xna.Framework.Input.Keys.D0:
                    //(_world.CurrentArea as Fantasy.Game.Farm).GrowAll();
                    break;
                case Microsoft.Xna.Framework.Input.Keys.Escape:
                    Environment.Exit(0);
                    break;
            }
        }
        public override void Update(GameTime gameTime)
        {
            _keyboardListener.Update(gameTime);
            _world.Update(gameTime);
            _collisionComponent.Update(gameTime);

            //_mapTileReticule.Position = _world.CurrentActor.GetFacingDirection();
            

        }

        public override void Draw(GameTime gameTime)
        {
            _spriteRenderer.Begin(_camera);
            //_spriteRenderer.Draw(_mapTileReticule);
            _world.Draw(gameTime, _spriteRenderer);
            _spriteRenderer.End(gameTime);
        }
    }
}
