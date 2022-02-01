// using Microsoft.Xna.Framework;
// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;

// public class Behavior { // pack animal, determines what activies to undertake and why
//     public void Do() {

//     }
// }

// public class Activity { // hunt, flee, roam, birth
//     public void Initialize() {

//     }

//     public abstract void Execute(Creature creature);
// }

// public class Need {
//     public enum Priority {
//         LifeThreatening,
//         HighPriority,
//         Normal,
//         LowPriority,
//         Disliked
//     }

//     public struct Entry {
//         public Priority Priority;
//         public float Threshold;
//     }

//     Need.Entry[] _needDefn = { 
//         new Need.Entry() {Priority = Need.Priority.LifeThreatening, Threshold = .2f },
//         new Need.Entry() {Priority = Need.Priority.LowPriority, Threshold = 1f }
//     };

//     public Need.Priority GetPriority(Creature creature) {

//         var attribute = creature.GetAttribute<Health>();

//         for(var i = 0; i < _needDefn.Length; i++) {
//             if(_needDefn[i].Threshold >= attribute.Value) {
//                 return _needDefn[i].Priority;
//             }
//         }
//         return Need.Priority.LowPriority;
//     }   
// }

// public class Roam : Activity {
//     public void Do(Creature creature) {
//         // if elapsed time & no current activity, then iscomplete
//         // if not elapsed, find random spot within creature.evnironment
//         // _next = Walk (random spot);
//     }
// }

// public class Walk : Activity {

// }

// public class Stalk : Activity {

// }

// public class Run : Activity {

// }

// public class Escape : Activity {
//     // find distance away from creature[]
//     // run to place
// }

// public class Hunt : Activity {
//     // find acceptable animal within creature.environment
//     // activity to stalk
//     // if creature target starts running, run after (but only for a while)
//     // activity when close enough to fight
// }

// public class Think : Activity {
//     public void Do(Creature creature) {
//         // if interupted stop pauseing, else wait until thinking time is over

//         var lifethreatening = creature.Needs.Where((Need.Priority n) => { return n == Need.Priority.LifeThreatening; });

//         //lifethreatening.OrderBy() {
//         //    EstimateTimeToDeath();
//         //}.FirstOrDefault();

//         // etc, etc.


//         Need needToAddress;

//         creature.NextActivity(needToAddress);
            

        
//         // determine next activity
//         // do I have life threatening needs
//         // do I have serious needs
//         // do I have strong desires
//         // do I have things I need to get done, including things like work (for npc)
//         // what are the other things I like to do
//     }
// }

// public class Attribute {

// }

// public class Health : Attribute {   
//     public float Value { get; set; }
// }

// public class Creature {
//     public IReadOnlyList<Behavior> Behaviors {
//         get => _behaviors;
//     }

//     public IReadOnlyList<Attribute> Attributes {
//         get => _attributes;
//     }

//     public Activity Current { 
//         get => _activities.Count > 0 ? _activities.Peek() : null;
//     }

//     public Activity Next { 
//         get;
//         set;
//     }
//     protected List<Behavior> _behaviors;
//     protected List<Attribute> _attributes;
//     protected Stack<Activity> _activities;
//     public List<Need> Needs { get; set; }
    
//     public void Update(GameTime gameTime) {
//         // something has been determined to be next
//         if(Next != null) {
//             _activities.Push(Next);
//             Next.Initialize();
//             Next = null;
//         }

//         // there is nothing to do, select a default activity
//         if(Current is null) {
//             Current = GetNextActivity();
//         }
        
//         Current.Execute(this);

//         while(Current != null && Current.Complete) {
//             _activities.Pop();
//         }
//     }

//     public void ResolveCollision(Thing collidedWith) {
//         // is what I am doing now enough to resolve collision
//         // if(moving).routeAround(collidedWith)
//         // can I do the activity I was tasked? recheck pathfinding
//         // if I can't, get a new activity
//     }

//     public void GetNextActivity() {
//         Behaviors.
//     }

//     public void GetNextActivity(Need.Priority priority, Need need = null) {
        

//         need.Activities; // the list of all things I can do to fill a need

//         foreach(var activity in activities) {
//             foreach(var behavior in activity) {
//                 behavior.RelevancyScore(activity); // how much I like to do this thing, or how much it fits with my behaviors

//                 environment.RelevancyScore(activity); // how likely I can do the thing within my sphere (no fishing if away from water, etc.)
//             }
//         }

//         // put relevancy score within a probability table, increase scale based on priority

//         _next = table.GetRandom();

//         if(_current != null) { 
//             _current.Abort(); // stop what you are doing, may need to investigate if there are reasons not to stop
//         }
//     }
// }

// public class World {
//     Creature[] _creatures;

//     public void Update(GameTime gameTime) {
//         foreach(var creature in _creatures) {
//             Update(gameTime);
//         }
//     }
// }