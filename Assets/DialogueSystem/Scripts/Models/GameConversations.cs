namespace DialogueManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    /// <summary>
    /// Model of the Game Conversations, contains al the Pending Conversations with every NPC or situation.
    /// If a Conversation is Triggered and it has a Pending Conversation, the Status will be changed to the one specified.
    /// </summary>
    public class GameConversations
    {
        /// <summary>
        /// Gets or sets the list of all the Pending Conversations in case they are Triggered.
        /// </summary>
        public List<PendingConversation> PendingConversations { get; set; }

        /// <summary>
        /// Gets or sets the list of conversations that haven't been added to PendingConversations
        /// </summary>
        public List<PendingStatus> ConversationsToAdd { get; set; }
    }
}