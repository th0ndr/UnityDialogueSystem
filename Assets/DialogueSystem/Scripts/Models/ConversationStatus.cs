namespace DialogueManager.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;



    [System.Serializable]
    public class ConversationStatus

    {
        public string Name;
        public string NextStatus;
        public Sentence[] Dialogue;
        public List<NewConversation> NewConversations;
    }

    [Serializable]
    public struct NewConversation
    {
        public string ConversationName;
        public PendingStatus PendingStatus;
    }
}