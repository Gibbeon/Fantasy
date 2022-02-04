using Microsoft.Xna.Framework;

namespace Fantasy.Game.Actors.Activities
{   
    public class Walk : Activity
    {
        public Vector2 Initial { get; set; }
        public Vector2 Target { get; set; }

        public Walk(Actor actor, Vector2 target) : base(actor)
        {
            Initial = Actor.Position;
            Target = target;
        }

        public override void Initialize()
        {
            
        }

        protected void UpdateFacing()
        {
             var move = Target - Actor.Position;

            if(move.X > 0.01) Actor.FacingDirection = FacingDirection.Right;
            else if(move.X < -0.01) Actor.FacingDirection = FacingDirection.Left;
            else if(move.Y < -0.01) Actor.FacingDirection = FacingDirection.Up;
            else if(move.Y > 0.01) Actor.FacingDirection = FacingDirection.Down;
            else return; // no movement

            Actor.PlayAnimation("Walk_" + Actor.FacingDirection.ToString());
        }

        public override void Update(GameTime gameTime)
        {
            var diff = Target - Actor.Position;
            
            UpdateFacing();

            if(Vector2.DistanceSquared(Target, Actor.Position) > (Actor.MoveSpeed * Actor.MoveSpeed))
            {
                Actor.Position += Vector2.Normalize(diff) * Actor.MoveSpeed;
            } 
            else
            {
                Actor.Position = Target;
                Finish();
            }          
        }
    }
}