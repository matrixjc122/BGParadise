using UnityEngine;
using System.Collections;

public class ActiveActorCommand : Command
{

 private Actor m_PreviouseActor = null;

 public ActiveActorCommand (Actor actor) : base (actor)
 {
  // catch and store the current active actor if available
  if (m_LifeCycle.ActiveActor) {
   m_PreviouseActor = m_LifeCycle.ActiveActor;
  }
 }

 public override bool IsExecuteable ()
 {
  bool isExecuteable = true;
  // check if the wanted actor is one actor of the current player
//  isExecuteable &= m_Owner.PlayerID == m_LifeCycle.CurrentPlayerID;
  // exclude tiles
  if(Owner)
    isExecuteable &= Owner.T != Actor.Type.Tile;
  return isExecuteable;
 }

 public override void Execute ()
 {

  
  // now set the new actor
  m_LifeCycle.ActiveActor = Owner; 

  // m_Owner can be null for undoing the first ActivePlayerCommand
  if (m_LifeCycle.ActiveActor) {
   m_LifeCycle.ActiveActor.Appearence.GetComponent<MeshRenderer> ().material.color = Color.cyan;
  }

  // reset properties of the previously active actor
  if (m_PreviouseActor) {
   this.m_PreviouseActor.Appearence.GetComponent<MeshRenderer> ().material.color 
   = m_PreviouseActor.PlayerID == 1 ? Color.yellow : Color.green;
  }

 }

 public override void AfterExecution ()
 {
  var cmd = new InvokeParticleAnimation (Owner, "Exclamation");
  CommandQueue.Instance.Enqueue( cmd );
 }

 public override void UndoExecution ()
 {
  // reset previouse actor
  var cmd = new ActiveActorCommand (m_PreviouseActor);
  CommandQueue.Instance.Enqueue( cmd );
//  CommandStack.Instance.Push( cmd );

  // do not add to command queue
  // 
 }


}





