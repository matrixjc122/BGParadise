using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//using System.Linq;

public class CommandQueue
{
 private LinkedList<Command> m_CurrentCommands;

 private CommandQueue () : base ()
 {
  m_CurrentCommands = new LinkedList<Command> ();
 }

 private static CommandQueue m_Instance;


 public void Enqueue (Command cmd)
 {
  if (cmd.IsExecuteable ()) {
   this.m_CurrentCommands.AddLast (cmd);
   }
 }

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


  if (m_CurrentCommands.Count == 0) {
   
   return;
  } 

  var node = m_CurrentCommands.First;
  while (node != null) {

   if(!node.Value.OnceExecuted){
    node.Value.BeforeExecution ();
    node.Value.Execute ();
    node.Value.OnceExecuted = true;
   }
    
   node.Value.m_Frames--;

   if (node.Value.m_Frames <= 0) {
    node.Value.AfterExecution ();
    var previouse = node.Previous;
    m_CurrentCommands.Remove (node);
    node = previouse;
    if (node == null)
     return;
   } else if(node.Value.m_Frames >0){
    node.Value.Execute ();
   }

   node = node.Next;
  } 
 }
}