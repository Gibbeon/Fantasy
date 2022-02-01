using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Fantasy.Framework2D;

namespace Fantasy.Game
{
    public class World
    {
        Microsoft.Xna.Framework.Game _game;
        List<Creature> _creatures;

        public Area CurrentArea {
            get;
            protected set;
        }
        public Creature CurrentActor 
        {
            get => _creatures.FirstOrDefault();
        }

        public List<Creature> Creatures
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
            CurrentArea.Initialize("sample");

            _creatures = new List<Creature>();
            var creature = new Creature(_game);
            creature.Initialize("adventurer");
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