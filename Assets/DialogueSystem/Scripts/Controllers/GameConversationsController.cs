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
            this.Model = gameConversations;
        }
        
        public void AddConversations(List<NewConversation> newConversations)
        {
            // CHECAR SI YA EXISTEN FALTA
            // ESTA CLASE VA A CAMBIAR BASTANTE

            foreach (NewConversation conversation in newConversations)
            {
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
}