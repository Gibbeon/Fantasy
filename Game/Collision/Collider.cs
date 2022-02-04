using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace Fantasy.Game
{
    public class Collider : ICollisionActor
    {
        public IShapeF Bounds { get; set; }

        public Collider(IShapeF bounds)
        {
            Bounds = bounds;
        }

        public virtual void OnCollision(CollisionEventArgs collisionInfo)
        {
            // doesn't move
        }
    }
}