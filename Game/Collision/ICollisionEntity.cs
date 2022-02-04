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
    public interface ICollisionEntity
    {
        bool IsRigidBody { get; }
    }
}