using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class Command : Object
{

 protected LifeCycle m_LifeCycle = null;
 protected Actor m_Owner = null;

 protected List<Command> ChildCommands{ get; set; }


 public Command (Actor obj)
 {
  m_Frames = 0f;
  m_Owner = obj;
  ChildCommands = new List<Command> ();
  m_LifeCycle = GameObject.Find ("LifeCycle").GetComponent<LifeCycle> ();
 }



 /// <summary>
 /// Determines whether this instance has owner. An command is always related to one specific
 /// commandable object in the world.
 /// </summary>
 /// <returns><c>true</c> if this instance has owner; otherwise, <c>false</c>.</returns>
 public virtual bool HasOwner ()
 {
  return m_Owner != null;
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
 public void BeforeExecution ()
 {
 }

 /// <summary>
 /// Execute the command for the specific actor.
 /// </summary>
 /// <param name="actor">Actor.</param>
 public abstract void Execute ();

 /// <summary>
 /// Executes the child commmands. This function is called during AfterExecution is processed.
 /// </summary>
 public virtual void ExecuteChildCommmands ()
 {
  foreach (Command cmd in ChildCommands) {
   if (cmd.IsExecuteable ()) {
    cmd.BeforeExecution ();
    cmd.Execute ();
    cmd.AfterExecution ();
    CommandQueue.Instance.AddDelayedCommand (cmd);
   }else
   {
   // if command is not executabel remove it from the childlist
    ChildCommands.Remove(cmd);
   }
  }
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
  this.UndoExecuteChildCommmands ();
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
//    CommandQueue.Instance.AddDelayedCommand(cmd);
   
  }
 }

 /// <summary>
 /// Process cleanup operations and mark command as handled. Otherwise the 
 /// command queue will throw an exception for unhandled commands;
 /// </summary>
 /// <param name="actor">Actor.</param>
 public virtual void AfterExecution ()
 {
  this.ExecuteChildCommmands ();
 }


 public float m_Frames{ get; set; }


 public virtual bool HasDelayedExecution ()
 {
  return m_Frames > 0;
 }

 /// <summary>
 /// This method is called each frame until time is over.
 /// </summary>
 public virtual void ExecuteDelayed ()
 {
 }

	
}
