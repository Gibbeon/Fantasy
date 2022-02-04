using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Fantasy.Framework2D;
using Fantasy.Game.Actors;

namespace Fantasy.Game
{
    public class World
    {
        Microsoft.Xna.Framework.Game _game;
        List<Actor> _creatures;

        public Area CurrentArea {
            get;
            protected set;
        }
        public Actor CurrentActor 
        {
            get => _creatures.FirstOrDefault();
        }

        public List<Actor> Creatures
        {
            get => _creatures;
        }

        public World(Microsoft.Xna.Framework.Game game)
        {
            _game = game;
        }

        public void Initialize() 
        {
            CurrentArea = new Farm(_game);
            CurrentArea.Initialize("maps/tuxemon-town");

            _creatures = new List<Actor>();
            var creature = new Player(_game);
            creature.Initialize("actors/adventurer");
            _creatures.Add(creature);
        }

        public void Update(GameTime gameTime)
        {
            CurrentArea.Update(gameTime);
            foreach (var creature in _creatures)
            {
                creature.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteRenderer spriteRenderer)
        {
            CurrentArea.Draw(gameTime, spriteRenderer.Camera.GetViewMatrix());
            foreach (var creature in _creatures)
            {
                spriteRenderer.Draw(creature);
            }
        }
    }
}