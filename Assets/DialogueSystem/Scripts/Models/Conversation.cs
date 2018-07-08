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
        /// <summary> Name of the conversation. </summary>
        public string Name;

        /// <summary> <see cref="ConversationStatus"/> of the conversation if a Dialogue was Triggered. </summary>
        public ConversationStatus ActiveStatus;

        /// <summary> Index in the Status List of the Active Status </summary>
        public int ActiveStatusIndex;

        /// <summary> List containing all the possible <see cref="ConversationStatus"/> each with it's Dialogues. </summary>
        public List<ConversationStatus> Status;

        /// <summary> Gets or sets the pending GameConversations of the scene </summary>
        public GameConversations GameConversations { get; set; }
    }
}