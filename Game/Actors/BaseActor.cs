using Microsoft.Xna.Framework;
using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Aseprite.Documents;
using MonoGame.Extended.Collisions;
using Fantasy.Framework2D;
using System.Linq;
using System.Collections.Generic;

namespace Fantasy.Game.Actors
{   
    public enum ActorActionState
    {
        Stopped,
        Playing,
        Aborted
    }
    public abstract class BaseActorAction
    {        
        public ActorActionState State
        {
            get; 
            protected set;
        }

        public BaseActor Actor
        {
            get; 
            protected set;
        }
        public BaseActorAction(BaseActor actor)
        {
            Actor = actor;
        }
        public virtual void Initialize()
        {

        }

        public void Start()
        {
            State = ActorActionState.Playing; 
            OnStarted();
        }

        public void Abort()
        {        
            if(State == ActorActionState.Playing)
            {    
                State = ActorActionState.Aborted;
                Actor.SetAction(null);
                OnAborted();
            }
        }

        public void Finish(bool succeeded = true)
        {
            State = ActorActionState.Stopped;
            Actor.SetAction(null);
            if(succeeded) OnSucceeded(); else OnFailed();
        }
        
        protected virtual void OnStarted()
        {    

        }

        protected virtual void OnAborted()
        {            

        } 

        protected virtual void OnFailed()
        {

        }    
        protected virtual void OnSucceeded()
        {

        }        

        public abstract void Update(GameTime gameTime);
    }

    public class IdleAction : BaseActorAction
    {
        public IdleAction(BaseActor actor) : base(actor)
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

    public class WalkAction : BaseActorAction
    {
        public Vector2 Initial { get; set; }
        public Vector2 Target { get; set; }

        public WalkAction(BaseActor actor, Vector2 target) : base(actor)
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

    public abstract class BaseActor : SpriteBatchDrawable, ICollisionActor
    {
        protected Microsoft.Xna.Framework.Game Game
        {
            get;
            private set;
        }
        public MonoGame.Aseprite.Graphics.AnimatedSprite Sprite
        {
            get;
            protected set;
        }
        public Dictionary<string, string> AnimationMap
        {
            get;
            protected set;
        }
        public BaseActorAction CurrentAction
        {
            get;
            private set;
        }
        public IShapeF Bounds // 
        { 
            get
            {
                if(!(Rotation > 0.01 || Rotation < 0.01))
                {
                    return new RectangleF(Position.X, Position.Y, Size.Width * Scale.X, Size.Height * Scale.Y);
                }
                else
                {
                    var srt = Matrix2.CreateTranslation(-Origin) * Matrix2.CreateRotationZ(Rotation) * Matrix2.CreateScale(Scale) * Matrix2.CreateTranslation(Origin);

                    return RectangleF.CreateFrom(
                        srt.Transform(new Vector2(Position.X, Position.Y)).ToPoint(), 
                        srt.Transform(new Vector2(Position.X + Size.Width, Position.Y + Size.Height)).ToPoint());
                }

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
        
        public BaseActor(Microsoft.Xna.Framework.Game game)
        {
            Game = game;
            AnimationMap = new Dictionary<string, string>();
            MoveSpeed = 2.0f;

        }
        public void Initialize(string name)
        {
            Contract.Assert(!IsInitialized);

            AsepriteDocument aseprite = Game.Content.Load<AsepriteDocument>(name);
            Sprite = new MonoGame.Aseprite.Graphics.AnimatedSprite(aseprite);

            RenderState = new SpriteRenderState() {
                BlendState = BlendState.NonPremultiplied
            };

            AnimationMap.Add("Run_Up", Sprite.Animations.Values.First().Name);
            AnimationMap.Add("Run_Left", Sprite.Animations.Values.First().Name);
            AnimationMap.Add("Run_Right", Sprite.Animations.Values.First().Name);
            AnimationMap.Add("Run_Down", Sprite.Animations.Values.First().Name);

            IsInitialized = true;
        }       


private int _delayAssignNewAction = 5;
        
        public override void Update(GameTime gameTime)
        {
            if(CurrentAction != null) 
            {
                CurrentAction.Update(gameTime);
                
                    _delayAssignNewAction = 5;
            } 
            else 
            {
                if(--_delayAssignNewAction <= 0)
                {
                    AssignNewActorAction();
                    _delayAssignNewAction = 5;
                }
            }

            Sprite.Update(gameTime);
        }
        public override void DrawTo(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Sprite.Render(spriteBatch);
            spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red);
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            // something collided with me -- what do I do?  
        }

        public void PlayAnimation(string name)
        {       
            var spriteAnimation = AnimationMap.GetValueOrDefault(name,  name);

            if(Sprite.CurrentAnimation.Name == spriteAnimation) return;
            
            Sprite.Play(spriteAnimation);
        }

        public void Walk(Vector2 direction)
        {
            SetAction(new WalkAction(this, Position + (direction * MoveSpeed)));
        }

        public void SetAction(BaseActorAction action)
        {
            if(CurrentAction != null) CurrentAction.Abort();
            CurrentAction = action;
            if(CurrentAction != null) CurrentAction.Start();
        }

        public abstract void AssignNewActorAction();

                /*public Vector2 GetTileFacing(int tileSize = 32)
        {
            var position = Bounds.Position + _sprite.Origin;

            switch(Orientation) {
                case Orientation.Up:
                    position.Y -= tileSize / 2;
                    break;
                case Orientation.Down:
                    position.Y += (int)(tileSize / 2);
                    break;
                case Orientation.Left:
                    position.X -= tileSize / 2;
                    break;
                case Orientation.Right:
                    position.X += (int)(tileSize / 2);
                    break;
            }

            return new Vector2(
                (int)(position.X / tileSize) * tileSize,
                (int)(position.Y / tileSize) * tileSize);
        }*/
    }
}