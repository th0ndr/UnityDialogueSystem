namespace DialogueManager.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DialogueManager.GameComponents;
    using DialogueManager.Models;

    /// <summary>
    /// Controller for the GameConversations Component
    /// </summary>
    public class GameConversationsController
    {
        /// <summary>
        /// Model of the GameConversation
        /// </summary>
        private GameConversations model;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameConversationsController"/> class.
        /// </summary>
        /// <param name="gameConversations">Model of the GameConversations</param>
        public GameConversationsController( GameConversations gameConversations )
        {
            gameConversations.PendingConversations = new Dictionary<string, List<PendingStatus>>();
            gameConversations.ConversationsToAdd = new List<PendingStatus>();
            this.model = gameConversations;
        }

        /// <summary>
        /// Creates a Key on the PendingConversations with the name of the Conversation if it doesn't exists already.
        /// Adds the first element in ConversationsToAdd to the Value PendingConversations with the correct key and sorts the list.
        /// </summary>
        public void AddConversation()
        {
            PendingStatus unlockedStatus = this.model.ConversationsToAdd[0];
            this.model.ConversationsToAdd.RemoveAt( 0 );
            Dictionary<string, List<PendingStatus>> conversations = this.model.PendingConversations;
            if (!conversations.ContainsKey( unlockedStatus.ConversationName ))
            {
                conversations[unlockedStatus.ConversationName] = new List<PendingStatus>();
            }

            List<PendingStatus> pending = conversations[unlockedStatus.ConversationName];
            PendingStatus match = pending.Where( status => status.ConversationName == unlockedStatus.StatusName ).FirstOrDefault();
            if (match == null)
            {
                pending.Add( unlockedStatus );
                pending.OrderBy( status => status.Importance );
            }
        }
    }
}