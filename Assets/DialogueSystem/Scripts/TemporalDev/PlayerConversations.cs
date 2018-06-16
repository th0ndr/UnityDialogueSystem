using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.DialogueSystem.Scripts.TemporalDev {
    public class PlayerConversations : MonoBehaviour {
        public List<PendingConversations> PendingConversations;
    }

    [Serializable]
    public struct PendingConversations {
        public string ConversationName;
        public List<PendingStatus> PendingStatus;
    }

    [Serializable]
    public struct PendingStatus {
        public string ConversationName;
        public int Importance;
    }
}
