using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerTurn
{

 /// <summary>
 /// Gets or sets the current player ID.
 /// </summary>
 /// <value>The current player I.</value>
 public int CurrentPlayerID{ get; set; }

 private Actor BigActor{ get; set; }

 private Dictionary<string,int> BA_CMDList{ get; set; }

 private Actor MediumActor{ get; set; }

 private Dictionary<string,int> MA_CMDList{ get; set; }

 private Actor SmallActor{ get; set; }

 private Dictionary<string,int> SA_CMDList{ get; set; }

 public PlayerTurn (int PlayerID)
 {
  this.CurrentPlayerID = PlayerID;
  this.BA_CMDList = new Dictionary<string,int> ();
  this.MA_CMDList = new Dictionary<string,int> ();
  this.SA_CMDList = new Dictionary<string,int> ();
 }


 public bool IsCommandRequired (Command cmd)
 {
  switch (cmd.Owner.T) {
  case Actor.Type.BigActor:
   this.IsBigActorCommandRequired (cmd);
   break;
  case Actor.Type.MediumActor:
   this.IsMediumActorCommandRequired (cmd);
   break;
  case Actor.Type.SmallActor:
   this.IsSmallActorCommandRequired (cmd);
   break;
  }
  return true;
 }

 private bool IsSmallActorCommandRequired (Command cmd)
 {
  if (SA_CMDList == null)
   return false;

  if (cmd.Type.Contains ("Push")) {
   SA_CMDList = null;
   return true;
  }

  if (!SA_CMDList.ContainsKey (cmd.Type))
   SA_CMDList.Add (cmd.Type, 1);
  else
   SA_CMDList [cmd.Type]++;

  return true;
 }

 private bool IsMediumActorCommandRequired (Command cmd)
 {
  if (MA_CMDList == null)
   return false;

  if (cmd.Type.Contains ("Push")) {
   MA_CMDList = null;
   return true;
  }
  return true;
 }

 private bool IsBigActorCommandRequired (Command cmd)
 {
  if (BA_CMDList == null)
   return false;

  if (cmd.Type.Contains ("Push")) {
   BA_CMDList = null;
   return true;
  }
  return true;
 }

}