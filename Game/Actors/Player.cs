using Fantasy.Game.Actors.Activities;

namespace Fantasy.Game.Actors
{
    public class Player : Actor
    {
        public Player(Microsoft.Xna.Framework.Game game) : base(game)
        {

        }

        public override void AssignNewActorActivity()
        {
            SetActivity(new Idle(this));
        }
    }
}