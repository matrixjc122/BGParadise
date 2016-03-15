using UnityEngine;
using System.Collections;

public class ParadiseInitializationCommand : Command
{

 public ParadiseInitializationCommand (Actor actor) : base (actor)
 {
 }


 public override void Execute ()
 {
  
  int sizeX = 50;
  int sizeY = 50;
  for (int i = 0; i < sizeX; i++) {
   for (int j = 0; j < sizeY; j++) {
    var tile = SpawnFactory.Spawn (Actor.Type.Tile);
    tile.gameObject.transform.position += new Vector3 (-5f + i, 0f, -5f + j);


   }
  }



  Debug.Log ("CommandExecuted");
 }

	
}
