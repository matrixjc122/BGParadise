using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// we make the LifeCycle itsfelf a commandable object. So we can create Commands targeting the game LifeCycle



public class LifeCycle : Actor
{

 public Actor ActiveActor{ get; set; }

 
 // Use this for initialization
 void Start ()
 {
  BordGameInitializationCommand init = new BordGameInitializationCommand (this);

  Debug.Log ("Started once");
  CommandQueue.Instance.Enqueue (init);

//  this.CurrentPlayerID = 0;

 }
	
 // Update is called once per frame
 void Update ()
 {
  CommandQueue.Instance.ProcessQueuedCommands ();
  CommandQueue.Instance.ProcessDelayedCommands ();

  if (Input.GetKeyDown ("z")) {
    CommandStack.Instance.ProcessUndoStack();
  }
  
 }

}