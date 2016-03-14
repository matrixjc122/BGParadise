using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandQueue : Queue<Command>
{



 private LinkedList<Command> m_DelayedCommands;

 private CommandQueue () : base ()
 {
  m_DelayedCommands = new LinkedList<Command> ();
 }

 private static CommandQueue m_Instance;

 public static CommandQueue Instance {
  get {
   if (m_Instance == null) {
    m_Instance = new CommandQueue ();
   }
   return m_Instance;
  }
 }


 public void ProcessQueuedCommands ()
 {
  if (this.Count > 0) {
   Command cmd = this.Dequeue ();
   if (cmd.IsExecuteable ()) {
    CommandStack.Instance.Push (cmd);
    cmd.BeforeExecution ();
    cmd.Execute ();
    cmd.AfterExecution ();
//    cmd.ExecuteChildCommmands();
    AddDelayedCommand (cmd);
   }

  }
 }

 public void AddDelayedCommand (Command cmd)
 {
  if (cmd.HasDelayedExecution ()) {
   m_DelayedCommands.AddFirst (cmd);
  }
 }

 public void ProcessDelayedCommands ()
 {
  if (m_DelayedCommands.Count == 0)
   return;

  var node = m_DelayedCommands.First;
  do {
   node.Value.ExecuteDelayed ();
   node.Value.m_Frames--;
   if (node.Value.m_Frames <= 0) {
    var previouse = node.Previous;
    m_DelayedCommands.Remove (node);
    node = previouse;
    if (node == null)
     return;
   }

   node = node.Next;
  } while(node != null);
 
 }
}