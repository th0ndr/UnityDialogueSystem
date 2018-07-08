namespace DialogueManager.GameComponents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using DialogueManager.Controllers;
    using DialogueManager.Models;
    using UnityEngine;

    /// <summary>
    /// Conversation Component, must be added for every single NPC or Situation that has a Conversation
    /// </summary>
    public class ConversationComponent : MonoBehaviour
    {
        /// <summary> Model of the Conversation </summary>
        public Conversation Model;

        /// <summary> Controller of the Conversation </summary>
        private ConversationController controller;

        /// <summary>
        /// Triggers the Conversation, displaying the Active Status.
        /// </summary>
        public void Trigger()
        {
            this.Model.GameConversations = GameObject
                .Find( "GameConversations" )
                .GetComponent<GameConversationsComponent>()
                .Model;
            DialogueManager dialogueManager = GameObject
                .Find( "DialogueManager" )
                .GetComponent<DialogueManagerComponent>()
                .Model;
            this.controller.Trigger( dialogueManager );
        }

        /// <summary> Creation of the Controller </summary>
        private void Awake()
        {
            this.controller = new ConversationController( this.Model );
        }
    }
}