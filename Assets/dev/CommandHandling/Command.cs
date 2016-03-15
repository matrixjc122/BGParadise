using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class Command : Object
{

 protected LifeCycle m_LifeCycle = null;
 protected Actor m_Owner = null;
 public bool OnceExecuted{get;set;}

 public Actor Owner{ get { return m_Owner; } }

 public string CommandType{ get; set; }

 protected List<Command> ChildCommands{ get; set; }


 public Command (Actor obj)
 {
  m_Frames = 0f;
  m_Owner = obj;
  ChildCommands = new List<Command> ();
  m_LifeCycle = GameObject.Find ("LifeCycle").GetComponent<LifeCycle> ();
  CommandType = "BaseCommand";
  OnceExecuted = false;
 }



 /// <summary>
 /// Determines whether this instance has owner. An command is always related to one specific
 /// commandable object in the world.
 /// </summary>
 /// <returns><c>true</c> if this instance has owner; otherwise, <c>false</c>.</returns>
 public virtual bool HasOwner ()
 {
  return Owner != null;
 }


 /// <summary>
 /// Check if Actor can handle the command.
 /// </summary>
 /// <returns><c>true</c>, if applyable was ised, <c>false</c> otherwise.</returns>
 /// <param name="actor">Actor.</param>
 public virtual bool IsExecuteable ()
 {
  return true;
 }

 /// <summary>
 /// Is triggered befores the execution. Can be used to start 
 /// animations or other commands e.g. for neighboring objects.
 /// </summary>
 /// <param name="actor">Actor.</param>
 public virtual void BeforeExecution ()
 {
 }

 /// <summary>
 /// Execute the command for the specific actor.
 /// </summary>
 /// <param name="actor">Actor.</param>
 public virtual void Execute (){}

 /// <summary>
 /// Is once executed during the enqueue process. Normal case calls the standard Execute method.
 /// </summary>
// public virtual void ExecuteOnce ()
// {
//  // if no frames specified, and ExecuteOnce was not overlaoded, we will call
//  // ExecuteFrame to be sure that it was executed once.
//  if(m_Frames == 0)
//    this.ExecuteFrame();
// }

 /// <summary>
 /// Process cleanup operations and mark command as handled. Otherwise the 
 /// command queue will throw an exception for unhandled commands;
 /// </summary>
 /// <param name="actor">Actor.</param>
 public virtual void AfterExecution ()
 {
 }

 /// <summary>
 /// Befores the undo execution.
 /// </summary>
 public virtual void BeforeUndoExecution ()
 {
 }

 /// <summary>
 /// Undos the execution.
 /// </summary>
 public virtual void UndoExecution ()
 {
 }

 /// <summary>
 /// Afters the undo execution.
 /// </summary>
 public virtual void AfterUndoExecution ()
 {
 }

 /// <summary>
 /// Undos the execute child commmands.
 /// </summary>
 public virtual void UndoExecuteChildCommmands ()
 {
  foreach (Command cmd in ChildCommands) {
   
   cmd.BeforeUndoExecution ();
   cmd.UndoExecution ();
   cmd.AfterUndoExecution ();

  
    // if command is not executabel remove it from the childlist
//   ChildCommands.Remove (cmd);
  

//    CommandQueue.Instance.AddDelayedCommand(cmd);
   
  }
 }



 public float m_Frames{ get; set; }


 public virtual bool HasDelayedExecution ()
 {
  return m_Frames > 0;
 }

 public LinkedList<OutputType> CastTo<InputType, OutputType>(LinkedList<InputType> input) where InputType: Component
 {
    var list = new LinkedList<OutputType>();
    foreach(var i in input)
    {
      list.AddLast(i.gameObject.GetComponent<OutputType>());
    }
    return list;
 }
	
}
