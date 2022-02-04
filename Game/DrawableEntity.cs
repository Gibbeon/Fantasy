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
    public abstract class DrawableEntity : SpriteBatchDrawable, ICollisionEntity, ICollisionActor
    {
        protected Microsoft.Xna.Framework.Game Game
        {
            get;
            private set;
        }
        public virtual MonoGame.Aseprite.Graphics.Sprite Sprite
        {
            get;
            protected set;
        }
        public Dictionary<string, string> AnimationMap
        {
            get;
            protected set;
        }

        public IShapeF Bounds // 
        { 
            get
            {
                return  new RectangleF(Position.X, Position.Y, Size.Width * Scale.X, Size.Height * Scale.Y);
            }                
        }
        
        public Vector2 Position
        {
            get => Sprite.Position;
            set { Sprite.Position = value; }
        }
        public Size2 Size
        {
            get => new Size2(Sprite.Width, Sprite.Height);
        }
        public Vector2 Scale 
        { 
            get => Sprite.Scale; 
            set { Sprite.Scale = value; }
        }
        public Vector2 Origin 
        { 
            get => Sprite.Origin; 
        }
        public float Rotation 
        { 
            get => Sprite.Rotation; 
            set { Sprite.Rotation = value; }
        }
        public Color Color 
        { 
            get => Sprite.Color; 
            set { Sprite.Color = value; }
        }
        public float Depth 
        { 
            get => Sprite.LayerDepth; 
            set { Sprite.LayerDepth = value; }
        }
        public bool IsInitialized
        {
            get;
            protected set;
        }

        public bool IsRigidBody
        {
            get;
            protected set;
        }
        
        public DrawableEntity(Microsoft.Xna.Framework.Game game)
        {
            Game = game;
            AnimationMap = new Dictionary<string, string>();

        }
        public virtual void Initialize(string name)
        {
            Contract.Assert(!IsInitialized);

            AsepriteDocument aseprite = Game.Content.Load<AsepriteDocument>(name);

            if(aseprite.Tags.Count == 0)
            {
                Sprite = new MonoGame.Aseprite.Graphics.Sprite(aseprite.Texture);
                IsRigidBody = true;
            } else 
            {                
                Sprite = new MonoGame.Aseprite.Graphics.AnimatedSprite(aseprite);
                IsRigidBody = false;
            }
            RenderState = new SpriteRenderState() {
                BlendState = BlendState.NonPremultiplied
            };

            IsInitialized = true;
        }       
        
        public override void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);
        }
        public override void DrawTo(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Sprite.Render(spriteBatch);
            spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red);
        }

        public virtual void OnCollision(CollisionEventArgs collisionInfo)
        {
            var other = collisionInfo.Other as ICollisionEntity;
            if(this.IsRigidBody)
            {
                return; // do not react
            } 
            else if(other != null && other.IsRigidBody)
            {
                Position -= collisionInfo.PenetrationVector;                
            }
        }

        public virtual void PlayAnimation(string name)
        {   
            var animSprite = Sprite as MonoGame.Aseprite.Graphics.AnimatedSprite;
            if(animSprite != null)
            {   
                
                var spriteAnimation = AnimationMap.GetValueOrDefault(name,  name);

                if(animSprite.CurrentAnimation.Name == spriteAnimation) return;
                
                animSprite.Play(spriteAnimation);
            }
        }
    }
}