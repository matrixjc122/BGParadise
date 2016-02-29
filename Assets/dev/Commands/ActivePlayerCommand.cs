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
  // set the new active actor
  m_LifeCycle.ActiveActor = m_Owner;
 }

 public override void UndoExecution ()
 {
 // reset previouse actor
  m_LifeCycle.ActiveActor = m_PreviouseActor;
 }


}





