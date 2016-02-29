using UnityEngine;
using System.Collections;

public class InitializationCommand : Command
{

 public InitializationCommand (Actor actor) : base (actor)
 {
 }

 public int[] pat = new int[] {
  0,  0,  -1, 0,  -2, 0,  -2, 0,  -3,
  0,  0,  0,  0,  0,  0,  0,  0,  0,
  1,  0,  9,  0,  -1, 0,  9,  0,  -2,
  0,  0,  0,  0,  0,  0,  0,  0,  0,
  2,  0,  1,  0,  9,  0,  -1, 0,  -2,
  0,  0,  0,  0,  0,  0,  0,  0,  0,
  2,  0,  9,  0,  1,  0,  9,  0,  -1,
  0,  0,  0,  0,  0,  0,  0,  0,  0,
  3,  0,  2,  0,  2,  0,  1,  0,  0
 };

 public override void Execute ()
 {
  
  int sizeX = 10;
  int sizeY = 10;
  for (int i = 0; i < sizeX; i++) {
   for (int j = 0; j < sizeY; j++) {
    
    var tile = SpawnFactory.Spawn (Actor.Type.Tile);
    tile.gameObject.transform.position += new Vector3 (-5f + i, 0f, -5f + j);


   }
  }

  for (int i = 0; i < sizeX - 1; i++) {
   for (int j = 0; j < sizeY - 1; j++) {
    if (pat [i + j * (sizeX - 1)] == 0)
     continue;

    var offset = new Vector3 (-5f + i + 0.5f, 0f, -5f + j + 0.5f);
    switch (System.Math.Abs(pat [i + j * (sizeX - 1)])) {
    case 1:
     {
      var runner = SpawnFactory.Spawn (Actor.Type.Runner);
      runner.gameObject.transform.position += offset;
      runner.PlayerID = 1;
     }
     break;
    case 2:
     {
      var jumper = SpawnFactory.Spawn (Actor.Type.Jumper);
      jumper.gameObject.transform.position += offset;
      jumper.PlayerID = 1;
     }
     break;
    case 3:
     {
      var king = SpawnFactory.Spawn (Actor.Type.King);
      king.gameObject.transform.position += offset;
      king.PlayerID = 1;
     }
     break;
    }



   }
  }



  Debug.Log ("CommandExecuted");
 }

	
}
