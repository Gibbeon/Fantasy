using Microsoft.Xna.Framework;
using System.Linq;
using MonoGame.Aseprite.Graphics;
using MonoGame.Aseprite.Documents;
using Fantasy.Framework2D;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;

namespace Fantasy.Game
{
    public enum CropState
    {
        None,
        Seed,
        Seedling,
        Youngling,
        Grown,
        Harvest
    }

    public class CropTile : SpriteBatchDrawable
    {        
        private Microsoft.Xna.Framework.Game _game;
        public string CropType {get; set; }
        public CropState CropState { get; set; }
        public bool Watered { get; set; }
        MonoGame.Aseprite.Graphics.AnimatedSprite _cropSprite { get; set; }
        MonoGame.Extended.Sprites.Sprite _background { get; set; }

        public CropTile(Microsoft.Xna.Framework.Game game)
        {
            _game = game;
        }

        public void Initialize(Vector2 position)
        {
            AsepriteDocument aseprite = _game.Content.Load<AsepriteDocument>("crops");
            _cropSprite = new MonoGame.Aseprite.Graphics.AnimatedSprite(aseprite);
            //_cropSprite.Stop();

            RenderState = new SpriteRenderState() {
                BlendState = BlendState.NonPremultiplied
            };

            var txt = new Texture2D(_game.GraphicsDevice, 1, 1);
            txt.SetData<Color>( new Color[] { Color.White });

            _background = new MonoGame.Extended.Sprites.Sprite(
                new MonoGame.Extended.TextureAtlases.TextureRegion2D(txt, 0, 0, 31, 31)
            );

            _background.Alpha = .25f;
            
            _cropSprite.Position = position;
        } 

        protected void SetCrop(string crop, CropState state = CropState.Seed)
        {
            CropType = crop;

            if(!string.IsNullOrEmpty(CropType)) {
                //_cropSprite.Play(CropType);
                SetCropState(state);
            }
        }

        protected void SetCropState(CropState state)
        {
            CropState = state;

            if(!string.IsNullOrEmpty(CropType) && state != CropState.None) {
                _cropSprite.Play(state.ToString().ToLower());
                //_cropSprite.CurrentFrameIndex = 10;// + (int)(CropState.Harvest - CropState); 
                //_cropSprite.Update(0.0001f);
                
            }
        }

        public void Plant(string crop)
        {
            if(CropState == CropState.None)
            {
                SetCrop(crop, CropState.Seed);
            }
        }

        public void Water()
        {
            Watered = true;
        }

        public void Grow()
        {
            if(Watered && CropState > CropState.None && CropState < CropState.Grown)
            {
                SetCropState(CropState + 1);
            }

            Watered = false;
        }

        public void Harvest()
        {
            if(CropState == CropState.Grown)
            {
                SetCropState(CropState.None);
            }
        }

        public void Till()
        {
            SetCropState(CropState.None);
        }

        public override void DrawTo(GameTime gameTime, SpriteBatch batch)
        {
            //_cropSprite.Update(0.015f);
            _background.Color = Watered ? Color.Black : Color.Brown;

            batch.Draw(_background, _cropSprite.Position + _background.Origin);
            
            if(CropState != CropState.None)
                _cropSprite.Render(batch);
        }
    }

    public class Farm : Area
    {
        private CropTile[] _cropTiles;

        SpriteBatch _batch;

        public Farm(Microsoft.Xna.Framework.Game game) : base(game)
        {
            _game = game;
        }

        public override void Initialize(string map)
        {
            base.Initialize(map);

            _cropTiles = new CropTile[_tiledMap.Width * _tiledMap.Height];
            _batch = new SpriteBatch(_game.GraphicsDevice);
        }

        public CropTile GetCropTile(Vector2 position)
        {
            return _cropTiles[(int)position.X / _tiledMap.TileWidth + ((int)position.Y / _tiledMap.TileHeight) * _tiledMap.Width];
        }

        protected CropTile CreateCropTileAt(Vector2 position)
        {
            var tile = new CropTile(_game);
            _cropTiles[(int)position.X / _tiledMap.TileWidth + ((int)position.Y / _tiledMap.TileHeight) * _tiledMap.Width] = tile;
            tile.Initialize( 
                new Vector2(
                    ((int)position.X / _tiledMap.TileWidth) * _tiledMap.TileWidth, 
                    ((int)position.Y / _tiledMap.TileHeight) * _tiledMap.TileHeight)
                );
            return tile;
        }

        // player actions
        public void TillAt(Vector2 position)
        {
            var tile = GetCropTile(position) ??  CreateCropTileAt(position);
            tile.Till();        
        }

        public void PlantAt(string crop, Vector2 position)
        {
            var tile = GetCropTile(position);

            if(tile == null) return;

            tile.Plant(crop);
        }

        public void WaterAt(Vector2 position)
        {
            var tile = GetCropTile(position);

            if(tile == null) return;

            tile.Water();
        }

        public void HarvestAt(Vector2 position)
        {
            var tile = GetCropTile(position);

            if(tile == null) return;

            tile.Harvest();
        }

        public void GrowAll() 
        {
            foreach(var tile in _cropTiles.Where(n => n != null))
            {
                tile.Grow();
            }
        }

        public override void Draw(GameTime gameTime, Matrix viewMatrix)
        {
            base.Draw(gameTime, viewMatrix);

            _batch.Begin(transformMatrix: viewMatrix);
            foreach(var tile in _cropTiles.Where(n => n != null)) {
                tile.DrawTo(gameTime, _batch);
            }
            _batch.End();
        }
    }
}