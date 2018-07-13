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
        /// <summary> Gets or sets the dictionary of the Pending Status, Key = Name of the Conversation</summary>
        public Dictionary<string, List<PendingStatus>> PendingConversations { get; set; }

        /// <summary> Gets or sets the list of conversations that haven't been added to PendingConversations </summary>
        public List<PendingStatus> ConversationsToAdd { get; set; }
    }

    /// <summary>
    /// Name of the Conversation and Status that is pending to be triggered
    /// </summary>
    [System.Serializable]
    public class PendingStatus
    {
        /// <summary> Name of the <see cref="Conversation"/> </summary>
        public string ConversationName;

        /// <summary> Name of the <see cref="ConversationStatus"/> </summary>
        public string StatusName;

        /// <summary> Importante of the <see cref="ConversationStatus"/>, higher means more important </summary>
        public int Importance;
    }
}