using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// we make the LifeCycle itsfelf a commandable object. So we can create Commands targeting the game LifeCycle



public class LifeCycle : MonoBehaviour
{

 public Actor ActiveActor{get;set;} 
 
 // Use this for initialization
 void Start ()
 {
  Actor empty = gameObject.AddComponent<Actor> ();
  InitializationCommand init = new InitializationCommand (empty);

  Debug.Log ("Started once");
  CommandQueue.Instance.Add (init);

  MonoBehaviour.Destroy(empty);
 }
	
 // Update is called once per frame
 void Update ()
 {
  CommandQueue.Instance.ProcessQueuedCommands ();
  CommandQueue.Instance.ProcessDelayedCommands ();
 }

}


