using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour
{
 public enum Type
 {
  SmallActor,
  MediumActor,
  BigActor,
  Tile
 }

 public virtual int PlayerID{ set; get; }

 public virtual Type T{ get; set; }

 public virtual GameObject Appearence{ get; set; }


 public virtual void OnMouseDown ()
 {
    
  var cmd = new ActiveActorCommand (this);
  CommandQueue.Instance.Enqueue (cmd);
    
 }

 public virtual void OnKeyDown ()
 {

  

  if (Input.GetKeyDown ("left")) {
   var cmd = new MoveCommand (this);
   cmd.Left ();
   CommandQueue.Instance.Enqueue (cmd);
  }
  if (Input.GetKeyDown ("up")) {
   var cmd = new MoveCommand (this);
   cmd.Up ();
   CommandQueue.Instance.Enqueue (cmd);
  }
  if (Input.GetKeyDown ("right")) {
   var cmd = new MoveCommand (this);
   cmd.Right ();
   CommandQueue.Instance.Enqueue (cmd);
  }
  if (Input.GetKeyDown ("down")) {
   var cmd = new MoveCommand (this);
   cmd.Down ();
   CommandQueue.Instance.Enqueue (cmd);
  }

 }

 void OnMouseDrag ()
 {
//  Debug.Log (Input.mousePosition);

 }

 protected LifeCycle m_LifeCycle;

 void Start ()
 {
  m_LifeCycle = GameObject.Find ("LifeCycle").GetComponent<LifeCycle> ();
 }

 void Update ()
 {
  if (m_LifeCycle.ActiveActor == this)
   OnKeyDown ();
 }
}

