using Microsoft.Xna.Framework;

namespace Fantasy.Game.Actors.Activities
{   
    public class Idle : Activity
    {
        public Idle(Actor actor) : base(actor)
        {
        }

        protected override void OnStarted()
        {
            Actor.PlayAnimation("Idle_" + Actor.FacingDirection.ToString());
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}