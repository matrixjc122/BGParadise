using UnityEngine;
using System.Collections;

public class InputKeyHandler : MonoBehaviour {

 void Update ()
 {
  var currentActor = this.gameObject.GetComponentInParent<Actor>();
  if (GameObject.Find("LifeCycle").GetComponent<LifeCycle>().ActiveActor == currentActor)
   OnKeyDown ();
 }

 public virtual void OnKeyDown ()
 {

  MoveCommand cmd = null;
  var currentActor = this.gameObject.GetComponentInParent<Actor>();
  if (Input.GetKeyDown ("left")) {
  Debug.Log("left");
   cmd = new MoveCommand (currentActor);
   cmd.Left ();
   CommandQueue.Instance.Enqueue (cmd);
   CommandStack.Instance.Push (cmd);
  } else if (Input.GetKeyDown ("up")) {
   cmd = new MoveCommand (currentActor);
   cmd.Up ();
   CommandQueue.Instance.Enqueue (cmd);
   CommandStack.Instance.Push (cmd);
  } else if (Input.GetKeyDown ("right")) {
   cmd = new MoveCommand (currentActor);
   cmd.Right ();
   CommandQueue.Instance.Enqueue (cmd);
   CommandStack.Instance.Push (cmd);
  } else if (Input.GetKeyDown ("down")) {
   cmd = new MoveCommand (currentActor);
   cmd.Down ();
   CommandQueue.Instance.Enqueue (cmd);
   CommandStack.Instance.Push (cmd);
  }

  if (cmd != null) {
   Debug.Log ("cmd active");
   

  }

 }
}
