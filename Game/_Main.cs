/*using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteFontPlus;
using Fantasy.Framework2D;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Input;
using MonoGame.Extended.Content;

using MonoGame.Aseprite.Documents;
using MonoGame.Aseprite.Graphics;

namespace Fantasy
{  
    public class _Map 
    {
        public struct Tile {

            public _Actor Actor;
            public bool IsOccupied() {
                return Actor != null;
            }
        }

        public Tile[,] Tiles = new Tile[5,5];

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for(int x = 0; x < Tiles.GetLongLength(0); x++) 
            {
                for(int y = 0; y < Tiles.GetLongLength(1); y++) 
                {
                    sb.AppendFormat("{0, 3}  |", Tiles[x, y].IsOccupied() ? Tiles[x,y].Actor.ID.ToString() : "");
                }  
                sb.AppendLine();
                sb.AppendLine(String.Join(null, Enumerable.Repeat("-", 6*5)));
            }

            return sb.ToString();
        }
    }

    public class _Actor {
        public static int _ID;
        public int ID;
        public int X;
        public int Y;
        public DateTime LastMoved = DateTime.Now;

        public _Actor() {
            ID = _ID++;
        }

        public bool Move(_Map map, int x, int y) {
            int newX = X + x;
            int newY = Y + y;

            if(newX < 0 || newY < 0 || newX >= 5 || newY >=5) 
                return false;
            
            if(map.Tiles[X + x, Y + y].IsOccupied()) {
                return false;
            }

            map.Tiles[X, Y].Actor = null;

            X += x;
            Y += y;

            map.Tiles[X, Y].Actor = this;

            return true;
        }

    }

    public class Main : Microsoft.Xna.Framework.Game
    {
        private OrthographicCamera _camera;

        private GraphicsDeviceManager _graphics;
        private SpriteLayer _spriteLayer;
        private SpriteText _spriteText;
        private SpriteFont _font;

        private SpriteBatch _spriteBatch;
private Vector2 _motwPosition;

        public Fantasy.Game.World world = new Fantasy.Game.World();
        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            IsMouseVisible = true;
            Window.IsBorderless = true;            
        }
        
        AnimatedSprite _sprite;

        protected override void LoadContent()
        {            
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var fontBakeResult = TtfFontBaker.Bake(File.ReadAllBytes(@"C:\\Windows\\Fonts\\Cour.ttf"),
                16,
                1024,
                1024,
                new[]
                {
                    CharacterRange.BasicLatin,
                    CharacterRange.Latin1Supplement,
                    CharacterRange.LatinExtendedA,
                    CharacterRange.Cyrillic
                }
            );

            _font = fontBakeResult.CreateSpriteFont(GraphicsDevice);
            Material.DefaultTexture2D = new Texture2D(GraphicsDevice, 1, 1);
            Material.DefaultTexture2D.SetData<Color>( new Color[] { Color.White });
                        
            map.Tiles[0,0].Actor = new _Actor() { X = 0, Y = 0};
            map.Tiles[4,4].Actor = new _Actor() { X = 4, Y = 4};
            map.Tiles[4,0].Actor = new _Actor() { X = 4, Y = 0};
            map.Tiles[0,4].Actor = new _Actor() { X = 0, Y = 4};

            _Actors.Add(map.Tiles[0,0].Actor);
            _Actors.Add(map.Tiles[4,4].Actor);
            _Actors.Add(map.Tiles[4,0].Actor);
            _Actors.Add(map.Tiles[0,4].Actor);
            
            _spriteLayer = new SpriteLayer(GraphicsDevice);
            _spriteText = new SpriteText(map.ToString(), _font);      
            _spriteLayer._sprites.Add(_spriteText);
           //var sprite = new Sprite();

            //_spriteLayer._sprites.Add(sprite);

            _spriteText.Location = new Vector2(0, 0);

            _tiledMap = Content.Load<TiledMap>("sample");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap); 

            var viewportadapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 600);
    _camera = new OrthographicCamera(viewportadapter);    

//SpriteSheet spriteSheet = new JsonContentLoader().Load<SpriteSheet>(Content,"adventurer.sf" );

//var x = new JsonContentLoader();




AsepriteDocument aseprite = Content.Load<AsepriteDocument>("adventurer");
 _sprite = new AnimatedSprite(aseprite);

    //MonoGame

    //var sprite = Content.Load<AnimatedSprite>("adventurer.sf", new JsonContentLoader());
    
    //sprite.Play("idle");
    //_motwPosition = new Vector2(100, 100);
    //_motwSprite = sprite;

    }

    private Vector2 GetMovementDirection()
{
    var movementDirection = Vector2.Zero;
    var state = KeyboardExtended.GetState();
    if (state.IsKeyDown(Keys.Down))
    {
        movementDirection += Vector2.UnitY;
    }
    if (state.IsKeyDown(Keys.Up))
    {
        movementDirection -= Vector2.UnitY;
    }
    if (state.IsKeyDown(Keys.Left))
    {
        movementDirection -= Vector2.UnitX;
    }
    if (state.IsKeyDown(Keys.Right))
    {
        movementDirection += Vector2.UnitX;
    }
    
    // Can't normalize the zero vector so test for it before normalizing
    if (movementDirection != Vector2.Zero)
    {
        movementDirection.Normalize(); 
    }
    
    return movementDirection;
}

private void MoveCamera(GameTime gameTime)
{
    var speed = 200;
    var seconds = gameTime.GetElapsedSeconds();
    var movementDirection = GetMovementDirection();
    _cameraPosition += speed * movementDirection * seconds;
}
        

private Vector2 _cameraPosition;
TiledMap _tiledMap;
TiledMapRenderer _tiledMapRenderer;
        _Map map = new _Map();
        List<_Actor> _Actors = new List<_Actor>();

        Random _random = new Random();

protected void UpdateSprite(GameTime gameTime)
{
    var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
    var walkSpeed = deltaSeconds * 128;
    var keyboardState = Keyboard.GetState();
    var animation = "idle";

    if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
    {
        animation = "walkNorth";
        _motwPosition.Y -= walkSpeed;
    }

    if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
    {
        animation = "walkSouth";
        _motwPosition.Y += walkSpeed;
    }

    if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
    {
        animation = "walkWest";
        _motwPosition.X -= walkSpeed;
    }

    if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
    {
        animation = "walkEast";
        _motwPosition.X += walkSpeed;
    }

    _sprite.Play("Tag");

    //if (keyboardState.IsKeyDown(Keys.R))
     //   Camera.ZoomIn(deltaSeconds);

    //if (keyboardState.IsKeyDown(Keys.F))
     //   Camera.ZoomOut(deltaSeconds);

    _sprite.Update(deltaSeconds);


    base.Update(gameTime);
}

        protected override void Update(GameTime gameTime)
        {
            foreach(var actor in _Actors) {
                if( (DateTime.Now - actor.LastMoved).Seconds > 1 && _random.NextDouble() > .5) {
                    actor.Move(map, (_random.Next() % 3) - 1, (_random.Next() % 3) - 1);
                    actor.LastMoved = DateTime.Now;
                }       
            }

            _spriteText.SetText(map.ToString());

            world.Update(gameTime);
            MoveCamera(gameTime);
    _camera.LookAt(_cameraPosition);

_tiledMapRenderer.Update(gameTime);
UpdateSprite(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);
            
            GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            _tiledMapRenderer.Draw(_camera.GetViewMatrix());

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, blendState: BlendState.NonPremultiplied);
            _sprite.Render(_spriteBatch);
    //_spriteBatch.Draw(_motwSprite, _motwPosition);
    _spriteBatch.End();


        }
    }
}
*/