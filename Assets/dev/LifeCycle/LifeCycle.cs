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
  
  //var init = new BordGameInitializationCommand (this);
  var init = new ParadiseInitializationCommand (this);

  Debug.Log ("Started once");
  CommandQueue.Instance.Enqueue (init);

//  this.CurrentPlayerID = 0;

 }
	
 // Update is called once per frame
 void Update ()
 {
  CommandQueue.Instance.ProcessQueuedCommands ();

  if (Input.GetKeyDown ("z")) {
    CommandStack.Instance.ProcessUndoStack();
  }
  
 }

}