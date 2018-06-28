namespace DialogueManager.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    public class Conversation
    {

        public string Name;
        public string ActiveStatus;
        public List<ConversationStatus> Status;

    }

    [System.Serializable]
    public struct PendingConversation
    {
        public string ConversationName;
        public List<PendingStatus> PendingStatus;
    }

    [System.Serializable]
    public struct PendingStatus
    {
        public string StatusName;
        public int Importance;
    }
}