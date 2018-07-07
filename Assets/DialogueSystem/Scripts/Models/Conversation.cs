namespace DialogueManager.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// The Conversation Class contains all the posible Dialogues the NPC or situation can have.
    /// </summary>
    [System.Serializable]
    public class Conversation
    {
        /// <summary>
        /// Name of the conversation.
        /// </summary>
        public string Name;

        /// <summary>
        /// Status of the conversation if a Dialogue was Triggered.
        /// </summary>
        public ConversationStatus ActiveStatus;
        public int ActiveStatusIndex;
        /// <summary>
        /// List containing all the possible Status each with it's Dialogues.
        /// </summary>
        public List<ConversationStatus> Status;

        /// <summary>
        /// Pending Status of Game Conversations
        /// </summary>
        public GameConversations GameConversations { get; set; }
    }

    // QUIERO CAMBIAR COMO FUNCIONAN PENDINGCONVERSATION Y PENDINGSTATUS
    [System.Serializable]
    public struct PendingConversation
    {
        public string ConversationName;
        public List<PendingStatus> PendingStatus;
    }

    [System.Serializable]
    public class PendingStatus
    {
        public string ConversationName;
        public string StatusName;
        public int Importance;
    }
}