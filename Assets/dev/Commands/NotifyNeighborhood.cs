using UnityEngine;
using System.Collections;

public class NotifyNeighborhood : Command
{

 private Entity.EventType EventType_;

 public NotifyNeighborhood (Actor actor, Entity.EventType type) : base (actor)
 {
  this.EventType_ = type;
  this.CommandType = "UpdateNeigborhoodCommand";
 }


 public override void Execute ()
 {  
  foreach (var a in Owner.Neighbors) {
   if(a && a.gameObject && a.gameObject.GetComponent<Entity>())
      a.gameObject.GetComponent<Entity>().EventFilter(EventType_,Owner);
//   CommandQueue.Instance.Enqueue(new InvokeEntityEvent(a,EventType_)); 
  }
 }

}