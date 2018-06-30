namespace DialogueManager.GameComponents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using DialogueManager.Controllers;
    using DialogueManager.Models;
    
    public class ConversationComponent : MonoBehaviour
    {
        public Conversation Model;
        private ConversationController Controller;

        private void Awake()
        {
            this.Controller = new ConversationController( Model );
        }
        public void Trigger(GameConversationsComponent gameConversationsComponent)
        {
            DialogueManagerComponent dialogueManager = GameObject.Find( "DialogueManager" )
                .GetComponent<DialogueManagerComponent>();
            Controller.Trigger( gameConversationsComponent, dialogueManager );
        }
    }
}