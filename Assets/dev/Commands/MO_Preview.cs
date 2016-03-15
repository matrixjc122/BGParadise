using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class MO_Preview : Command
{
    
 public MO_Preview (Actor actor) : base (actor)
 {
 }

 public override bool IsExecuteable ()
 {
  return true;
 }

    
 public override void Execute ()
 {
  Debug.Log ("Executed");
    
 }

 public override void AfterExecution ()
 {
  // initialize delayed execution parameter
  this.m_Frames = 0f;
 }

    
}


