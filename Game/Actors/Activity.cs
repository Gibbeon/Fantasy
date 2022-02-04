using Microsoft.Xna.Framework;

namespace Fantasy.Game.Actors
{   
    
   
    public abstract class Activity
    {      
        public enum ActivityState
        {
            Stopped,
            Playing,
            Aborted
        }  
        public ActivityState State
        {
            get; 
            protected set;
        }

        public Actor Actor
        {
            get; 
            protected set;
        }
        public Activity(Actor actor)
        {
            Actor = actor;
        }
        public virtual void Initialize()
        {

        }

        public void Start()
        {
            State = ActivityState.Playing; 
            OnStarted();
        }

        public void Abort()
        {        
            if(State == ActivityState.Playing)
            {    
                State = ActivityState.Aborted;
                Actor.SetActivity(null);
                OnAborted();
            }
        }

        public void Finish(bool succeeded = true)
        {
            State = ActivityState.Stopped;
            Actor.SetActivity(null);
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
}