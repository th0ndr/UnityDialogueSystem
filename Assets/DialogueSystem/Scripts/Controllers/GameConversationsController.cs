namespace DialogueManager.Controllers
{
    using DialogueManager.GameComponents;
    using DialogueManager.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GameConversationsController
    {
        public GameConversations Model;

        public GameConversationsController(GameConversations gameConversations)
        {
            gameConversations.PendingConversations = new List<PendingConversation>();
            gameConversations.ConversationsToAdd = new List<NewConversation>();
            this.Model = gameConversations;
        }
        
        public void AddConversation()
        {
            // CHECAR SI YA EXISTEN FALTA
            // ESTA CLASE VA A CAMBIAR BASTANTE
            NewConversation conversation = this.Model.ConversationsToAdd[0];
            this.Model.ConversationsToAdd.RemoveAt( 0 );
            this.Model.PendingConversations.Add( new PendingConversation
            {
                ConversationName = conversation.ConversationName,
                PendingStatus = new List<PendingStatus> {
                        conversation.PendingStatus,
                    }
            } );
        }
    }
}