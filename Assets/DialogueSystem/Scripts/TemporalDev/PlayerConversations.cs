using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.DialogueSystem.Scripts.TemporalDev {
    public class PlayerConversations : MonoBehaviour {

        public DialogueManager DialogueManager;
        public List<PendingConversation> PendingConversations { get; set; }

        public void AddConversations(List<NewConversation> newConversations) {

            //CHECAR SI YA EXISTEN FALTA
            //ESTA CLASE VA A CAMBIAR BASTANTE

            foreach( NewConversation conversation in newConversations) {
                PendingConversations.Add( new PendingConversation {

                    ConversationName = conversation.ConversationName,
                    PendingStatus = new List<PendingStatus> {
                        conversation.PendingStatus,
                    },

                } );
            }



        }

    }

    [Serializable]
    public struct PendingConversation {
        public string ConversationName;
        public List<PendingStatus> PendingStatus;
    }

    [Serializable]
    public struct PendingStatus {
        public string StatusName;
        public int Importance;
    }
}
