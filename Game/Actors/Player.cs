namespace Fantasy.Game.Actors
{
    public class Player : BaseActor
    {
        public Player(Microsoft.Xna.Framework.Game game) : base(game)
        {

        }

        public override void AssignNewActorAction()
        {
            SetAction(new IdleAction(this));
        }
    }
}