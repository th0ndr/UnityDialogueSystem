namespace DialogueManager.GameComponents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using DialogueManager.Controllers;
    using DialogueManager.Models;
    
    /// <summary>
    /// Conversation Component, must be added for every single NPC or Situation that has a Conversation
    /// </summary>
    public class ConversationComponent : MonoBehaviour
    {
        public Conversation Model;
        private ConversationController Controller;

        private void Awake()
        {
            this.Controller = new ConversationController( Model );
        }

        /// <summary>
        /// Triggers the Conversation, displaying it, if it has an active Status.
        /// </summary>
        /// <param name="gameConversationsComponent"></param>
        public void Trigger(GameConversationsComponent gameConversationsComponent)
        {
            DialogueManagerComponent dialogueManager = GameObject
                .Find( "DialogueManager" )
                .GetComponent<DialogueManagerComponent>();
            Controller.Trigger( gameConversationsComponent, dialogueManager );
        }
    }
}