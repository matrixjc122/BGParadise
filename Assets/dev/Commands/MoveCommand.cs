using UnityEngine;
using System.Collections;

public class MoveCommand : Command
{

 public Vector3 m_Displacement;
 private Vector3 m_DelayedMoveDistance;


 public MoveCommand (Actor actor) : base (actor)
 {
  m_Displacement = new Vector3 ();
  m_Frames = 3.0f;
 }

 public override bool IsExecuteable ()
 {
  return m_LifeCycle.ActiveActor == m_Owner;
 }

 public override void Execute ()
 {
  // start some animations
  m_DelayedMoveDistance = new Vector3 (m_Displacement.x / m_Frames,
   m_Displacement.y / m_Frames,
   m_Displacement.z / m_Frames);
 }

 public override void ExecuteDelayed ()
 {
  m_Owner.gameObject.transform.position += m_DelayedMoveDistance;  
 }

 public override void UndoExecution ()
 {
  // reset previouse actor
  m_Owner.gameObject.transform.position -= m_Displacement;
 }

 public void Up (int disp = 1)
 {
  m_Displacement.z = disp;
 }

 public void Right (int disp = 1)
 {
  m_Displacement.x = disp;
 }

 public void Down (int disp = 1)
 {
  m_Displacement.z = -disp;
 }

 public void Left (int disp = 1)
 {
  m_Displacement.x = -disp;
 }

}
