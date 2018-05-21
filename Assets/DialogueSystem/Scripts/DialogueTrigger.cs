using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour 
{

    public Dialogue[] dialogues;


	public void TriggerDialogue()
	{
        foreach (Dialogue dialogue in dialogues)
        {

            if(VariablesManager.instance.integerDictionary[dialogue.variableName] == dialogue.variableValue)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                break;
            }

                
        }
		
	}
	

   

}
     

