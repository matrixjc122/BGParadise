using UnityEngine;
using System.Collections;

public class ActivePlayerCommand : Command
{

 private Actor m_PreviouseActor;

 public ActivePlayerCommand (Actor actor) : base (actor)
 {
 }

 public override bool IsExecuteable ()
 {
  return m_Owner.T != Actor.Type.Tile;
 }

 public override void Execute ()
 {
  // save previouse actor for undo/action
  this.m_PreviouseActor = m_LifeCycle.ActiveActor;

  if (this.m_PreviouseActor)
   this.m_PreviouseActor.Appearence.GetComponent<MeshRenderer> ().material.color = Color.white;

  // set the new active actor
  m_LifeCycle.ActiveActor = m_Owner;
  if(m_Owner)
    m_Owner.Appearence.GetComponent<MeshRenderer> ().material.color = Color.blue;
 }

 public override void UndoExecution ()
 {
  // reset previouse actor
  var cmd = new ActivePlayerCommand (m_PreviouseActor);
  cmd.Execute ();
  // do not add to command queue
  // 
 }


}





