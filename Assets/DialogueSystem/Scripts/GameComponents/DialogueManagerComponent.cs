namespace DialogueManager.GameComponents
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using DialogueManager.Controllers;
    using DialogueManager.Models;
    // This class manages the text in the dialogues, the transition between sentences, animations, and such
    public class DialogueManagerComponent : MonoBehaviour
    {
        public DialogueManager Model;
        private DialogueManagerController Controller;
        void Start()
        {
            Model.ImageText = Model.ImageText.GetComponent<Image>();
            Model.source = GetComponent<AudioSource>();
            Controller = new DialogueManagerController( this.Model );
        }

        void Update()
        {
            if (Input.GetKeyDown( Model.NextKey ) && Model.finished && Model.DoubleTap)
            {
                DisplayNextSentence();
                Model.finished = false;
            }

            if (Input.GetKeyDown( Model.NextKey ) && Model.DoubleTap == false)
            {
                Model.finished = true;
                DisplayNextSentence();
            }
        }

        public void StartDialogue(Dialogue dialogue)
        {
            Controller.StartDialogue( dialogue );
            DisplayNextSentence();
        }

        // Display next sentence in dialogue
        private void DisplayNextSentence()
        {
            StopAllCoroutines();
            if (Controller.DisplayNextSentence())
            {
                StartCoroutine( Controller.TypeSentence() );
            }
        }
    }
}