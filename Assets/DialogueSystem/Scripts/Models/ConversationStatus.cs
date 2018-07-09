namespace DialogueManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Status of a conversation, each status contains the Dialogue that will be displayed one after another.
    /// </summary>
    [System.Serializable]
    public class ConversationStatus
    {
        /// <summary>
        /// Name of the Status.
        /// </summary>
        public string Name;

        /// <summary>
        /// Index of the the NextStatus in the Conversation Status List
        /// </summary>
        public int NextStatusIndex;

        /// <summary>
        /// The complete Dialogue which will be displayed.
        /// </summary>
        public Dialogue Dialogue;

        /// <summary>
        /// The List of the unlocked <see cref="ConversationStatus"/> in other Conversations.
        /// </summary>
        public List<PendingStatus> NewConversations;

        /// <summary>
        /// Gets or sets the <see cref="ConversationStatus"/> in which the Conversation will be once the Dialogue of the current Status ends.
        /// </summary>
        public ConversationStatus NextStatus { get; set; }
    }
}