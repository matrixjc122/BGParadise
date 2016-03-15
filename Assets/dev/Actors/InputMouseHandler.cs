using UnityEngine;
using System.Collections;

public class InputMouseHandler : MonoBehaviour {

 public virtual void OnMouseDown ()
 {

  {
   ActiveActorCommand cmd = new ActiveActorCommand (this.gameObject.GetComponentInParent<Actor>());
   if (cmd.IsExecuteable ()) {
    CommandQueue.Instance.Enqueue (cmd);
    CommandStack.Instance.Push (cmd);

    return;
   }
  }

  {
   ExpandCommand cmd = new ExpandCommand (this.gameObject.GetComponentInParent<Actor>());
   if (cmd.IsExecuteable ()) {
    CommandQueue.Instance.Enqueue (cmd);
    CommandStack.Instance.Push (cmd);
    return;
   }
  } 
 }

}
