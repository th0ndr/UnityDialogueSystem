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
        /// Name of the Status which the Conversation will be once the Dialogue of the current Status ends.
        /// </summary>
        public string NextStatus;

        /// <summary>
        /// The complete Dialogue which will be displayed.
        /// </summary>
        public Sentence[] Dialogue;

        /// <summary>
        /// The List of the unlocked Status in other Conversations.
        /// </summary>
        public List<NewConversation> NewConversations;
    }

    // QUISIERA QUITAR ESTO
    [Serializable]
    public struct NewConversation
    {
        public string ConversationName;
        public PendingStatus PendingStatus;
    }
}