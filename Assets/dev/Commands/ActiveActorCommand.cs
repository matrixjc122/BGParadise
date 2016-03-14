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

  var o = GameObject.Instantiate (Resources.Load("Sprite_Particle System") as GameObject);

  o.transform.parent = Owner.gameObject.transform;
  o.transform.position = Owner.gameObject.transform.position;
  var ps = o.GetComponent<ParticleSystem>();
  ps.playOnAwake = true;
  ps.Play();
  ps.loop = false;
  o.SetActive(true);



  Owner.gameObject.name = "TEST";


 }

 public override void UndoExecution ()
 {
  // reset previouse actor
  var cmd = new ActiveActorCommand (m_PreviouseActor);
  cmd.Execute ();
  // do not add to command queue
  // 
 }


}





