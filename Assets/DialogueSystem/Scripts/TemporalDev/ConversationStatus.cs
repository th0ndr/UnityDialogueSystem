using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.DialogueSystem.Scripts.TemporalDev {

    [System.Serializable]
    public class ConversationStatus {

        public Sentence[] Dialogue;
        public List<string> NextStatus;
    }
}
