using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    public class PlayerConversationsComponent : MonoBehaviour {

        public DialogueComponent DialogueManager;
        public List<PendingConversation> PendingConversations { get; set; }

        private void Start() {
            PendingConversations = new List<PendingConversation>();
        }

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
