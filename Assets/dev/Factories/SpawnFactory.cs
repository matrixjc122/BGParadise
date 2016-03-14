using UnityEngine;
using System.Collections;

public static class SpawnFactory
{
 public static Actor Spawn (Actor.Type type)
 {
  GameObject empty = new GameObject ();

  Actor actor;
  switch (type) {
  case Actor.Type.MediumActor:
   actor = SpawnJumper (empty);
   actor.Appearence.SetActive(true);
   break;
  case Actor.Type.SmallActor:
   actor = SpawnRunner (empty);
   actor.Appearence.SetActive(true);
   break;
  case Actor.Type.BigActor:
   actor = SpawnKing (empty);
   actor.Appearence.SetActive(true);
   break;
  case Actor.Type.Tile:
   actor = SpawnTile (empty);
   actor.Appearence.SetActive(true);
   break;
  default:
   actor = null;  
   break;
  }
  return actor;
 }

 private static Actor SpawnJumper (GameObject o)
 {
  Debug.Log ("Spawn Jumper");
  o.name = "Jumper";
  Actor a = o.AddComponent<Actor> ();
  a.T = Actor.Type.MediumActor;
  a.Appearence = MakePrefab(o);
  BoxCollider collider = o.AddComponent<BoxCollider>();
  collider.size = new Vector3(1.0f,0.01f,1.0f);

  return a;
 }

 private static Actor SpawnRunner (GameObject o)
 {
  o.name = "Runner";
  Actor a = o.AddComponent<Actor> ();
  a.T = Actor.Type.SmallActor;
  a.Appearence = MakePrefab(o);
  BoxCollider collider = o.AddComponent<BoxCollider>();
  collider.size = new Vector3(1.0f,0.01f,1.0f);
  return a;
 }

 private static Actor SpawnKing (GameObject o)
 {
  o.name = "King";
  Actor a = o.AddComponent<Actor> ();
  a.T = Actor.Type.BigActor;
  a.Appearence = MakePrefab(o);
  BoxCollider collider = o.AddComponent<BoxCollider>();
  collider.size = new Vector3(1.0f,0.01f,1.0f);
  return a;
 }
  
 public static GameObject MakePrefab (GameObject o)
 {
  GameObject prefab = GameObject.Instantiate (Resources.Load (o.name) as GameObject);
  prefab.transform.parent = o.transform;
  prefab.transform.position = o.transform.position;
  return prefab;

 }

 private static Actor SpawnTile (GameObject o)
 {
  o.name = "Tile";
  Actor a = o.AddComponent<Actor> ();
  a.T = Actor.Type.Tile;
  a.Appearence = MakePrefab(o);
  return a;
 }

}


