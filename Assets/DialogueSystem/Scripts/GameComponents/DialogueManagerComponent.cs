namespace DialogueManager.GameComponents
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using DialogueManager.Controllers;
    using DialogueManager.Models;

    /// <summary>
    /// This class manages the text in the dialogues, the transition between sentences, animations, and such
    /// </summary>
    public class DialogueManagerComponent : MonoBehaviour
    {
        public DialogueManager Model;
        private DialogueManagerController Controller;

        /// <summary>
        /// Is excecuted when the object is instantiated
        /// </summary>
        private void Awake()
        {
            GameObject gameConversations = Instantiate(Model.GameConversationsPrefab);
            gameConversations.name = "GameConversations";
            GameObject canvasObjects = Instantiate( Model.CanvasObjectsPrefab );
            canvasObjects.name = "DialogueCanvas";
            
            Model.DialogueText = GameObject.Find("/DialogueCanvas/DialogueBox/DialogueText").GetComponent<Text>();
            Model.ImageText = GameObject.Find("/DialogueCanvas/DialogueBox/Image").GetComponent<Image>();
            Model.Animator = GameObject.Find("/DialogueCanvas/DialogueBox").GetComponent<Animator>();
            Model.Source = this.GetComponent<AudioSource>();

            Controller = new DialogueManagerController( this.Model );
        }

        /// <summary>
        /// Checks if there is something in the model to display and if there was an input
        /// </summary>
        private void Update()
        {
            if(Model.DialogueToShow != null)
            {
                this.StartDialogue();
            }
            if (Input.GetKeyDown( Model.NextKey ) && Model.Finished && Model.DoubleTap)
            {
                this.DisplayNextSentence();
                Model.Finished = false;
            }

            if (Input.GetKeyDown( Model.NextKey ) && Model.DoubleTap == false)
            {
                Model.Finished = true;
                this.DisplayNextSentence();
            }
        }

        /// <summary>
        /// Start new dialogue, and reset all data from previous dialogues
        /// </summary>
        /// <param name="dialogue">Dialogue that will be displayed</param>
        public void StartDialogue()
        {
            Controller.StartDialogue();
            this.DisplayNextSentence();
        }

        /// <summary>
        /// Display next sentence in dialogue
        /// </summary>
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