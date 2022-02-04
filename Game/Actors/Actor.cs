using Microsoft.Xna.Framework;
using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Aseprite.Documents;
using MonoGame.Extended.Collisions;
using Fantasy.Framework2D;
using System.Linq;
using System.Collections.Generic;
using Fantasy.Game.Actors.Activities;

namespace Fantasy.Game.Actors
{   
    public abstract class Actor : DrawableEntity, ICollisionActor
    {
        public Activity CurrentActivity
        {
            get;
            private set;
        }
        
        public float MoveSpeed 
        { 
            get; 
            protected set;
        }
        public FacingDirection FacingDirection 
        { 
            get;
            set;
        }
        
        public Actor(Microsoft.Xna.Framework.Game game) : base(game)
        {
            MoveSpeed = 2.0f;
        }     
        
        public override void Update(GameTime gameTime)
        {
            if(CurrentActivity != null) 
            {
                CurrentActivity.Update(gameTime);
            } 
            else 
            {
                AssignNewActorActivity();
            }

            Sprite.Update(gameTime);
        }
        public override void DrawTo(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Sprite.Render(spriteBatch);

            var bnd = (RectangleF)Bounds;

            spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red);
        }

        public void Walk(Vector2 direction)
        {
            SetActivity(new Walk(this, Position + (direction * MoveSpeed)));
        }

        public void SetActivity(Activity Activity)
        {
            if(CurrentActivity != null) CurrentActivity.Abort();
            CurrentActivity = Activity;
            if(CurrentActivity != null) CurrentActivity.Start();
        }

        public abstract void AssignNewActorActivity();
    }
}