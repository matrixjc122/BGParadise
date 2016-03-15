using UnityEngine;
using System.Collections;




public class MoveCommand : Command
{

 static Vector3 Clone (Vector3 vec)
 {
  return new Vector3 (vec.x, vec.y, vec.z);
 }

 public Vector3 m_Direction;
 private Vector3 m_DelayedMoveDistance;


 public MoveCommand (Actor actor) : base (actor)
 {
  m_Direction = new Vector3 ();
  CommandType = "MoveCommand";
  this.m_Frames = 5.0f;
 }

 public override void BeforeExecution ()
 {

  CommandQueue.Instance.Enqueue (new InvokeParticleAnimation (Owner, "Exclamation"));

  // the animation for a movement takes 3 frames, so each frame the actor 
  // moves one third of the distance. Define this move distance here
  this.m_DelayedMoveDistance = new Vector3 (m_Direction.x / m_Frames,
   m_Direction.y / m_Frames,
   m_Direction.z / m_Frames);
     
  // if the ray hit, access the game object for further investigations.
  RaycastHit hit;
  // If a actor moves in a direction we have to check if another actor
  // is in front of this actor. This can be done by raycasting. For this we
  // have to define the current position and the direction of the cast.
  var owner_pos = Clone (Owner.gameObject.transform.position);
  owner_pos.y += 0.05f;

  if (Physics.Raycast (owner_pos, m_Direction, out hit, 1.25f)) {

   var hit_o = hit.transform.gameObject;
   var actor = hit_o.GetComponentInParent<Actor> ();
   // if we hit a gameObject with an component Actor
   // we force the hit_o to move
   if (actor) {
    var cmd = new MoveCommand (actor);
    cmd.m_Direction = this.m_Direction;
    // because its an child command of the current command
    // do not add it to the global command queue
    CommandQueue.Instance.Enqueue (cmd);
    CommandStack.Instance.Push (cmd);


   }
  }
 }



 public override void Execute ()
 {
  Owner.gameObject.transform.position += m_DelayedMoveDistance;  
 }

 public override void UndoExecution ()
 {
//  Debug.Log ("UndoExecution");
  Owner.gameObject.transform.position -= m_Direction;
 }

 public void Up (int disp = 1)
 {
  m_Direction.z = disp;
  CommandType = "Up";
 }

 public void Right (int disp = 1)
 {
  m_Direction.x = disp;
  CommandType = "Right";
 }

 public void Down (int disp = 1)
 {
  m_Direction.z = -disp;
  CommandType = "Down";
 }

 public void Left (int disp = 1)
 {
  m_Direction.x = -disp;
  CommandType = "Left";
 }

}
