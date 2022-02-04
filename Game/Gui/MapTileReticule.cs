
using Microsoft.Xna.Framework;
using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Aseprite.Documents;
using MonoGame.Extended.Collisions;
using Fantasy.Framework2D;
using System.Linq;
using System.Collections.Generic;

namespace Fantasy.Game
{
    public class MapTileReticule : SpriteBatchDrawable
    {
        const int TILE_SIZE = 24;
        public Fantasy.Game.Actors.Actor Player 
        {
             get; 
             protected set;
        }

        public MapTileReticule(Fantasy.Game.Actors.Actor player)
        {
            Player = player;

        }
        public override void Initialize()
        {
            
        }       
       
        public override void Update(GameTime gameTime)
        {
            
        }

        public override void DrawTo(GameTime gameTime, SpriteBatch spriteBatch)
        {

            var position = new Vector2(Player.Position.X + Player.Size.Width / TILE_SIZE, Player.Position.Y + Player.Size.Height - TILE_SIZE);           

            switch(Player.FacingDirection)
            {
                case Actors.FacingDirection.Down:
                    position.Y = Player.Position.Y + Player.Size.Height;
                    break;
                case Actors.FacingDirection.Up:
                    position.Y += -(TILE_SIZE / 2);
                    break;
                case Actors.FacingDirection.Left:
                    position.X -= TILE_SIZE;
                    break;
                case Actors.FacingDirection.Right:
                    position.X += Player.Size.Width;
                    break;
            }         

            spriteBatch.DrawRectangle(
               new Vector2( ((int)position.X / TILE_SIZE + ((((int)position.X % TILE_SIZE)) > (TILE_SIZE / 1.1f) ? 1 : 0)) * TILE_SIZE, 
                            ((int)position.Y / TILE_SIZE + ((((int)position.Y % TILE_SIZE)) > (TILE_SIZE / 4f) ? 1 : 0)) * TILE_SIZE), 
                            new Size2(TILE_SIZE, TILE_SIZE), 
                            Color.Blue
            );
        }
    }
}