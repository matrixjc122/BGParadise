using UnityEngine;
using System.Collections;

public class MoveCommand : Command
{

 public Vector3 m_Direction;
 private Vector3 m_DelayedMoveDistance;
 private Actor m_DisplacedActor;


 public MoveCommand (Actor actor) : base (actor)
 {
  m_Direction = new Vector3 ();
  m_Frames = 5.0f;
 }

 public override bool IsExecuteable ()
 {
  return true;
 }

 public override void Execute ()
 {
  // the animation for a movement takes 3 frames, so each frame the actor 
  // moves one third of the distance. Define this move distance here
  m_DelayedMoveDistance = new Vector3 (m_Direction.x / m_Frames,
   m_Direction.y / m_Frames,
   m_Direction.z / m_Frames);

  // If a actor moves in a direction we have to check if another actor
  // is in front of this actor. This can be done by raycasting. For this we
  // have to define the current position and the direction of the cast.
  var owner_pos = m_Owner.gameObject.transform.position;
  var origin = new Vector3 (owner_pos.x, owner_pos.y, owner_pos.z);

  // if the ray hit, access the game object for further investigations.
  RaycastHit hit;
  if (Physics.Raycast (origin, m_Direction, out hit, 1.25f)) {
   var hit_o = hit.transform.gameObject;
   m_DisplacedActor = hit_o.GetComponent<Actor> ();
   // if we hit a gameObject with an component Actor
   // we force the hit_o to move
   if (m_DisplacedActor) {
    var cmd = new MoveCommand (m_DisplacedActor);
    cmd.m_Direction = this.m_Direction;
    cmd.m_Frames = 3.0f; // speedup animation
    // because its an child command of the current command
    // do not add it to the global command queue
    this.ChildCommands.Add(cmd);
   }


  }
  
 }

 public override void ExecuteDelayed ()
 {
  // actor displacement per frame
  m_Owner.gameObject.transform.position += m_DelayedMoveDistance;  
 }

 public override void UndoExecution ()
 {
  // reset previouse actor
  m_Owner.gameObject.transform.position -= m_Direction;
//  if(m_DisplacedActor)
//  {
//    m_DisplacedActor.gameObject.transform.position -= m_Direction;
//  }
 }

 public void Up (int disp = 1)
 {
  m_Direction.z = disp;
 }

 public void Right (int disp = 1)
 {
  m_Direction.x = disp;
 }

 public void Down (int disp = 1)
 {
  m_Direction.z = -disp;
 }

 public void Left (int disp = 1)
 {
  m_Direction.x = -disp;
 }

}
