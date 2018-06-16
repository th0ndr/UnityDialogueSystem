using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.DialogueSystem.Scripts.TemporalDev {
    public class Conversation: MonoBehaviour {

        public string Name;
        public string ActiveStauts;
        public List<NamedStatus> Status;

    }

    [Serializable]
    public struct NamedStatus {
        public string Name;
        public ConversationStatus Status;
    }

}
