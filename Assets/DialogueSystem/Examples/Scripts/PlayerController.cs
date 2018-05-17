using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public DialogueTrigger dialogue;
	
	void Update () 
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            dialogue.TriggerDialogue();
        }
	}

}
