using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace Fantasy.Game
{
    public class CollisionActor : ICollisionActor
    {
        public IShapeF Bounds { get; set; }

        public CollisionActor(IShapeF bounds)
        {
            Bounds = bounds;
        }

        public virtual void OnCollision(CollisionEventArgs collisionInfo)
        {
            // doesn't move
        }
    }
}