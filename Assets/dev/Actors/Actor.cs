using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actor : MonoBehaviour
{

 public enum Type
 {
  SmallActor,
  MediumActor,
  BigActor,
  Tile
 }

 public LinkedList<Actor> Neighbors = new LinkedList<Actor> ();

 public virtual int PlayerID{ set; get; }

 public virtual Type T{ get; set; }

 public virtual GameObject Appearence{ get; set; }


 protected LifeCycle m_LifeCycle;

 void Start ()
 {
  m_LifeCycle = GameObject.Find ("LifeCycle").GetComponent<LifeCycle> ();
 }


 public LinkedList<TargetType> SphereCast<CastType, TargetType> (float radius) where CastType: Component
 {

  LinkedList<TargetType> list = new LinkedList<TargetType> ();

  // if this is gameobject has TargetType-Component
  var Ownder_Entity = this.gameObject.GetComponent<TargetType> ();
  if (Ownder_Entity != null) {
   var allObjects = GameObject.FindObjectsOfType<CastType> ();
   foreach (CastType other in allObjects) {
    var dist = (this.gameObject.transform.position - other.gameObject.transform.position).magnitude;
    if (dist < radius) {
     if(other.gameObject.GetComponent<TargetType>() != null)
     {
      list.AddLast (other.gameObject.GetComponent<TargetType>());
     }
    }
   }
  }
  return list;
 }








}

