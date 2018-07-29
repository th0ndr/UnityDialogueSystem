namespace DialogueManager.GameComponents
{
    using System.Collections;
    using System.Collections.Generic;
    using DialogueManager.Controllers;
    using DialogueManager.Models;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// This class manages the text in the dialogues, the transition between sentences, animations, and such
    /// </summary>
    public class DialogueManagerComponent : MonoBehaviour
    {
        /// <summary> Model of the Dialogue Manager </summary>
        public DialogueManager Model;

        /// <summary> Controller of the Dialogue Manager </summary>
        private DialogueManagerController controller;

        /// <summary>
        /// Is excecuted when the object is instantiated
        /// </summary>
        private void Awake()
        {
            GameObject gameConversations = Instantiate( this.Model.GameConversationsPrefab );
            gameConversations.name = "GameConversations";

            /*
            GameObject canvasObject = new GameObject( "DialogueCanvas", typeof(RectTransform) );
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            */
            Transform canvasObject = GameObject.Find( "DialogueCanvas" ).GetComponent<Transform>();
            GameObject dialogueBox = Instantiate( this.Model.CanvasObjectsPrefab );
            dialogueBox.transform.position = new Vector3( -250, 0, 0 );
            dialogueBox.name = "DialogueBox";
            dialogueBox.transform.SetParent( canvasObject.transform );
            dialogueBox.GetComponent<RectTransform>().localPosition = new Vector3( 0, -500, 0 );


            this.Model.DialogueStartPoint = GameObject.Find( "/DialogueCanvas/DialogueBox/DialogueStartPoint" ).GetComponent<Transform>();
            this.Model.ImageText = GameObject.Find( "/DialogueCanvas/DialogueBox/CharacterImage" ).GetComponent<Image>();
            this.Model.Animator = GameObject.Find( "/DialogueCanvas/DialogueBox" ).GetComponent<Animator>();
            this.Model.Source = this.GetComponent<AudioSource>();

            this.controller = new DialogueManagerController( this.Model );
        }

        /// <summary>
        /// Checks if there is something in the model to display and if there was an input
        /// </summary>
        private void Update()
        {
            if ( this.Model.DialogueToShow != null )
            {
                this.StartDialogue();
            }

            if ( Input.GetKeyDown( this.Model.NextKey ) && this.Model.Finished && this.Model.DoubleTap )
            {
                this.DisplayNextSentence();
                this.Model.Finished = false;
            }

            if ( Input.GetKeyDown( this.Model.NextKey ) && this.Model.DoubleTap == false )
            {
                this.Model.Finished = true;
                this.DisplayNextSentence();
            }
        }

        /// <summary>
        /// Start new dialogue, and reset all data from previous dialogues
        /// </summary>
        private void StartDialogue()
        {
            this.controller.StartDialogue();
            this.DisplayNextSentence();
        }

        /// <summary>
        /// Display next sentence in dialogue
        /// </summary>
        private void DisplayNextSentence()
        {
            this.StopAllCoroutines();
            if ( this.controller.DisplayNextSentence() )
            {
                this.StartCoroutine( this.controller.TypeSentence() );
            }
        }
    }
}