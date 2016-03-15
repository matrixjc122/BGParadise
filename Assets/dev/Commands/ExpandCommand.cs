using UnityEngine;
using System.Collections;

public class ExpandCommand : Command
{

 private Actor m_SpawnedActor;



 public ExpandCommand (Actor actor) : base (actor)
 {
  CommandType = "ExpandCommand";
 }

 public override bool IsExecuteable ()
 {
  if (Owner.T == Actor.Type.Tile) {

   var allChildren = Owner.gameObject.GetComponentsInChildren<Transform> ();
   if (allChildren.Length > 0) {
    foreach (Transform child in allChildren) {
     var actor = child.gameObject.GetComponent<Actor> ();
     if (actor != null) {
      if (actor.T == Actor.Type.BigActor ||
          actor.T == Actor.Type.MediumActor ||
          actor.T == Actor.Type.SmallActor) {
       return false;
      }
     }
    }
   }
   return true;
  } else
   return false;
 }


 public override void Execute ()
 {  
  
  m_SpawnedActor = SpawnFactory.Spawn (Actor.Type.SmallActor);
//  m_SpawnedActor.gameObject.transform.parent = Owner.gameObject.transform;
  m_SpawnedActor.gameObject.transform.position = Owner.gameObject.transform.position;
  m_SpawnedActor.gameObject.GetComponent<Entity>().EventFilter(Entity.EventType.StartEntity,m_SpawnedActor); 

  CommandQueue.Instance.Enqueue (new NotifyNeighborhood(m_SpawnedActor, Entity.EventType.RegisterSender));
  CommandQueue.Instance.Enqueue (new InvokeParticleAnimation (m_SpawnedActor, "HP_up"));

 }


 public override void UndoExecution ()
 {
//  CommandQueue.Instance.Enqueue (new InvokeParticleAnimation (m_SpawnedActor, "HP_down"));
  m_SpawnedActor.gameObject.SetActive (false);
  GameObject.Destroy (m_SpawnedActor);
 }

}