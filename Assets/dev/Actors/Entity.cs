using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Entity : MonoBehaviour
{

 public enum EventType
 {
  UnregisterSender,
  RegisterSender,
  ModifyNeighborhood,
  StartEntity,
  DestroyEntity,
  UpgradeEntity
 }




 public float HealthPoints{ get; set; }

 private bool CanCallModifyHealt = true;
 private bool CanCallInvokeIndicator = true;
 private Actor Owner;

 // Use this for initialization
 void Start ()
 {
  HealthPoints = 30;
  Owner = this.gameObject.GetComponent<Actor> ();
 }
	
 // Update is called once per frame
 void Update ()
 {
  if (this.CanCallModifyHealt)
   this.StartCoroutine ("ModifyHealth");

  if (this.CanCallInvokeIndicator)
   this.StartCoroutine ("InvokeHealthIndicator");

  if (HealthPoints <= 0) {
   EventFilter (EventType.DestroyEntity, Owner); 

  }
  if (HealthPoints >= 100) {
   EventFilter (EventType.UpgradeEntity, Owner); 
   if (Owner.T == Actor.Type.BigActor) {
    HealthPoints = 100.0f;
    CanCallModifyHealt = false;
    CanCallInvokeIndicator = false;

   }

  }
    
  
 }

 IEnumerator ModifyHealth ()
 {
  CanCallModifyHealt = false;
  yield return new WaitForSeconds (0.2f);
  float scale = 0;
  foreach (var n in Owner.Neighbors) {
   scale += n.T == Actor.Type.SmallActor ? 1.0f / 8.0f : 0;
   scale += n.T == Actor.Type.MediumActor ? 3.0f / 8.0f : 0;
   scale += n.T == Actor.Type.BigActor ? 7.0f / 8.0f : 0;
  }

  float ratio = Owner.Neighbors.Count / 8.0f;
  if (ratio >= 0.75) {
   HealthPoints += ratio * scale;
  } else {
   HealthPoints -= ratio * 2 * scale;
  }

  CanCallModifyHealt = true;
 }

 IEnumerator InvokeHealthIndicator ()
 {
  CanCallInvokeIndicator = false;
  yield return new WaitForSeconds (3.0f);
  float ratio = Owner.Neighbors.Count / 8.0f;
  if (ratio >= 0.75) {
   CommandQueue.Instance.Enqueue (new InvokeParticleAnimation (Owner, "HP_up"));
  } else {
   CommandQueue.Instance.Enqueue (new InvokeParticleAnimation (Owner, "HP_down"));
  }

  CanCallInvokeIndicator = true;
 }

 public void OnGUI ()
 {

  Vector2 targetPos;
  targetPos = Camera.main.WorldToScreenPoint (this.transform.position);
  Rect rec = new Rect (targetPos.x, Screen.height - targetPos.y, 40, 20);
    
  GUI.Label (rec, (int)HealthPoints + "");

  if (Owner) {
   rec = new Rect (targetPos.x, Screen.height - targetPos.y - 20, 40, 20);
   GUI.Label (rec, Owner.Neighbors.Count + "");
  }
    
 }

 public void EventFilter (EventType type, Actor sender)
 {

  if (Owner == null)
   Owner = sender;


  if (type == EventType.StartEntity) {
   var list = Owner.SphereCast<Entity,Actor> (1.5f);
   list.Remove (Owner);
   Owner.Neighbors = list;
  }

  if (type == EventType.DestroyEntity) {
   CommandQueue.Instance.Enqueue (new NotifyNeighborhood (Owner, EventType.UnregisterSender));
   CommandQueue.Instance.Enqueue (new InvokeParticleAnimation (Owner, "Radioactive"));
   GameObject.Destroy (this.gameObject);
    
  }

  if (type == EventType.RegisterSender) {
   if (Owner.Neighbors.Find (sender) == null)
    Owner.Neighbors.AddLast (sender);
  }

  if (type == EventType.UnregisterSender) {
   if (Owner.Neighbors.Find (sender) != null)
    Owner.Neighbors.Remove (sender);
  }


  if (type == EventType.UpgradeEntity) {
   switch (Owner.T) {
   case Actor.Type.SmallActor:
    GameObject.Destroy (Owner.Appearence);
    Owner.T = Actor.Type.MediumActor;
    HealthPoints *= 0.35f;
    Owner.gameObject.name = Actor.Type.MediumActor.ToString ();
    Owner.Appearence = SpawnFactory.MakePrefab (Owner.gameObject);
    Owner.Appearence.SetActive (true);
    break;
   case Actor.Type.MediumActor:
    GameObject.Destroy (Owner.Appearence);
    Owner.T = Actor.Type.BigActor;
    Owner.gameObject.name = Actor.Type.BigActor.ToString ();
    HealthPoints *= 0.05f;
    Owner.Appearence = SpawnFactory.MakePrefab (Owner.gameObject);
    Owner.Appearence.SetActive (true);
    break;
   case Actor.Type.BigActor:
//    GameObject.Destroy(Owner.Appearence);
//    Owner.Appearence = SpawnFactory.Spawn(Actor.Type.MediumActor);
    break;
   }

   CommandQueue.Instance.Enqueue (new InvokeParticleAnimation (Owner, "Exclamation"));
    
  }

 }
}
