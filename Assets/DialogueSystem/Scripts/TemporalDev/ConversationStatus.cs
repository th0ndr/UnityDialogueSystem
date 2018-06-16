using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.DialogueSystem.Scripts.TemporalDev {

    [System.Serializable]
    public class ConversationStatus {

        public Sentence[] Dialogue;
        public List<string> NextStatus;
        public List<NewConversation> NewConversations;

        public string Trigger(PlayerConversations playerConversations) {


            //DENTRO DE PLAYERCONVERSATIONS SE DEBE HACER LA LOGICA PARA QUE NO SE REPITAN
            playerConversations.AddConversations( NewConversations );



            //ACTIVAR DIALOGUE
            // EL OBJETO SEA UN Dialogue en vez de un Sentence, quitar variables que no se usan a Dialogue

            Dialogue dialogue = new Dialogue {
                sentences = this.Dialogue,
            };
            playerConversations.DialogueManager.StartDialogue( dialogue );




            string newStatus = null;
            //POR AHORA SOLO AGARRA EL PRIMER NEXT STATUS
            if (NextStatus.Count > 0) {
                newStatus = NextStatus[0];
            }
            
            
            

            return newStatus;
        }

    }

    [Serializable]
    public struct NewConversation {
        public string ConversationName;
        public PendingStatus PendingStatus;
    }
}
