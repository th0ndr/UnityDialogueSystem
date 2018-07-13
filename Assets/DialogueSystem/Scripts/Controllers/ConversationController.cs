namespace DialogueManager.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using DialogueManager.GameComponents;
    using DialogueManager.Models;
    using UnityEngine;

    /// <summary>
    /// Controller for the Conversation Component
    /// </summary>
    public class ConversationController
    {
        /// <summary> Model of the Conversation </summary>
        private Conversation model;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConversationController"/> class.
        /// </summary>
        /// <param name="conversation">Model of the Conversation</param>
        public ConversationController(Conversation conversation)
        {
            conversation.ActiveStatus = conversation.Status[conversation.ActiveStatusIndex];
            this.model = conversation;
        }
        
        /// <summary>
        /// Triggers a Conversation, checking if there is an unlocked Conversation Status and Triggering the correct Status
        /// </summary>
        /// <param name="dialogueManager">Dialogue Manager where the Dialogue will be displayed</param>
        public void Trigger(DialogueManager dialogueManager)
        {
            var conversations = this.model.GameConversations.PendingConversations;
            if (conversations.ContainsKey( this.model.Name ) && conversations[this.model.Name].Count > 0)
            {
                var statusList = conversations[this.model.Name];
                string statusName = statusList[0].StatusName;
                statusList.RemoveAt( 0 );

                this.model.ActiveStatus = this
                    .model
                    .Status
                    .Where( status => status.Name.Equals( statusName ) )
                    .First();

                this.model.ActiveStatusIndex = this
                    .model
                    .Status
                    .IndexOf( this.model.ActiveStatus );
            }

            if (this.model.ActiveStatus != null)
            {
                this.TriggerStatus(dialogueManager);
            }
        }

        /// <summary>
        /// Triggers the ActiveStatus and changes it to the NextStatus
        /// </summary>
        /// <param name="dialogueManager">Dialogue Manager where the Dialogue will be displayed</param>
        private void TriggerStatus(DialogueManager dialogueManager)
        {
            ConversationStatus status = this.model.ActiveStatus;
            this.model.GameConversations.ConversationsToAdd.AddRange( status.NewConversations );
            dialogueManager.DialogueToShow = status.Dialogue;
            this.model.ActiveStatus = status.NextStatus;
            this.model.ActiveStatusIndex = status.NextStatusIndex;
        }
    }
}
