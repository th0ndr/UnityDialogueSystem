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
        /// List of all the Pending Conversations in case they are Triggered.
        /// </summary>
        public List<PendingConversation> PendingConversations { get; set; }
    }
}