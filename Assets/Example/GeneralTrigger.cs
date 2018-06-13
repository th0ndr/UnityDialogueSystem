using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralTrigger : MonoBehaviour {

    private DialogueTrigger dialogueTrigger;
    public Transform Player;
    public Transform Focus;
    public CameraFollow cameraFollow;
    public Dialogue[] dialogues;
    private void Start() {
        dialogueTrigger = new DialogueTrigger();
        dialogueTrigger.dialogues = dialogues;
    }

    public void Trigger() {

        dialogueTrigger.TriggerDialogue();

    }
    private void Update() {
        switch (VariablesManager.instance.integerDictionary["status"]) {
            case 0:
                if (Player.position.y > 0.62) {
                    Trigger();
                    VariablesManager.instance.integerDictionary["status"] = 1;
                }
                if(Player.position.x < -1) {
                    VariablesManager.instance.integerDictionary["status"] = 1;
                }
                break;
            case 1:
                if (Player.position.x < -1) {
                    Trigger();
                    VariablesManager.instance.integerDictionary["status"] = 2;
                }
                break;
            case 2:
                if (Player.position.y > 0.62) {
                    cameraFollow.Speed = 0.05f;
                    cameraFollow.target = Focus;
                    Trigger();
                    VariablesManager.instance.integerDictionary["status"] = 3;
                    Invoke( "ReturnCamera", 3 );
                }
                break;
        }
    }
    private void ReturnCamera() {
        cameraFollow.target = Player;
        cameraFollow.Speed = 0.1f;
    }

}
