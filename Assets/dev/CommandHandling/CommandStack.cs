using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandStack : Stack<Command>
{


 private CommandStack () : base ()
 {
 }

 private static CommandStack m_Instance;

 public static CommandStack Instance {
  get {
   if (m_Instance == null) {
    m_Instance = new CommandStack ();
   }
   return m_Instance;

  }
 }

 public void ProcessUndoStack ()
 {
  if (this.Count > 0) {
   var undo_cmd = this.Pop ();
   undo_cmd.BeforeUndoExecution();
   undo_cmd.UndoExecution ();
   undo_cmd.AfterUndoExecution();
  }
 }

      	
}
