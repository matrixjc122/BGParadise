using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InvokeParticleAnimation : Command
{

 private string m_MaterialName;

 private GameObject EmitterObject;
 private static Dictionary<string,PSMaterialProperties> m_ProperyMap;

 protected class PSMaterialProperties
 {
  public PSMaterialProperties (int min, int max, int numParticles)
  {
   this.min = min;
   this.max = max;
   this.numParticles = numParticles;
  }

  public int min;
  public int max;
  public int numParticles;

 }


 public InvokeParticleAnimation (Actor actor, string name) : base (actor)
 {
  m_MaterialName = name;

  if (m_ProperyMap == null) {
   m_ProperyMap = new Dictionary<string,PSMaterialProperties> ();
   m_ProperyMap.Add ("HP_down", new PSMaterialProperties (1, 10, 10));
   m_ProperyMap.Add ("HP_up", new PSMaterialProperties (1, 10, 10));
   m_ProperyMap.Add ("Exclamation", new PSMaterialProperties (1, 1, 1));
   m_ProperyMap.Add ("Impossible", new PSMaterialProperties (10, 1, 100));
   m_ProperyMap.Add ("Nuclear", new PSMaterialProperties (10, 1, 100));
   m_ProperyMap.Add ("Radioactive", new PSMaterialProperties (10, 1, 100));
  }
  

 }

 public override bool IsExecuteable ()
  {
  if (Owner == null || Owner.gameObject == null) return false;
  else return true;
  }


 public override void Execute ()
 {
  if (Owner == null || Owner.gameObject == null) return;
   EmitterObject = GameObject.Instantiate (Resources.Load ("Sprite_Particle System") as GameObject);
   EmitterObject.transform.position = Owner.gameObject.transform.position;
   var psprops = m_ProperyMap [m_MaterialName];
  // edit particle system properties
  var ps = EmitterObject.GetComponent<ParticleSystem> ();
  // handle
  // - psprops.duration
  // - psprops.ratio
//  ps.Emit (psprops.particles);
//  ps.loop = false;
  var emission = ps.emission;
  emission.rate = new ParticleSystem.MinMaxCurve(psprops.min,psprops.max);
  ps.Emit(psprops.numParticles);


  GameObject.Destroy (EmitterObject, ps.duration);


  // update particle material
  var renderer = EmitterObject.GetComponent<ParticleSystemRenderer> ();
  renderer.material = Resources.Load ("Materials/ParticleSystem/" + this.m_MaterialName) as Material;
  }
 
}