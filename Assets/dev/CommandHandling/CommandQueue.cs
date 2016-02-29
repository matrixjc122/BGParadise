using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandQueue
{


 
 private Queue<Command> m_Queue;
 private LinkedList<Command> m_DelayedCommands;

 private CommandQueue ()
 {
  m_Queue = new Queue<Command> ();
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

 public void Add (Command cmd)
 {
  if (cmd.HasOwner ())
   m_Queue.Enqueue (cmd);
  else
   throw new UnityException ("Command has no owner! This is not allowed");
 }

 public void ProcessQueuedCommands ()
 {
  if (m_Queue.Count > 0) {
   Command cmd = m_Queue.Dequeue ();
   if (cmd.IsExecuteable ()) {
    cmd.BeforeExecution ();
    cmd.Execute ();
    cmd.AfterExecution ();
    if (cmd.HasDelayedExecution ())
     m_DelayedCommands.AddFirst (cmd);

   }

  }
 }

 public void ProcessDelayedCommands ()
 {
  if (m_DelayedCommands.Count == 0)
   return;

  var node = m_DelayedCommands.First;
  do {
   node.Value.ExecuteDelayed ();
   node.Value.m_Frames --;
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