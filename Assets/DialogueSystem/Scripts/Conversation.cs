using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.DialogueSystem.Scripts.TemporalDev {
    public class Conversation: MonoBehaviour {

        public string Name;
        public string ActiveStatus;
        public List<NamedStatus> Status;

        public void Trigger(PlayerConversations playerConversations) {

            //ESTARIA BIEN HACER UN DICCIONARIO PARA EVITAR HACER EL FOREACH
            foreach(PendingConversation pending in playerConversations.PendingConversations) {
                if (pending.ConversationName.Equals( this.Name )) {

                    //POR AHORA SOLO PUEDE AGARRA UN STATUS
                    ActiveStatus = pending.PendingStatus[0].StatusName;
                    playerConversations.PendingConversations.Remove( pending );
                    //FALTA CAMBIAR METODO PARA VARIOS POSIBLES STATUS CON PRIORIDAD

                    break;
                }
            }

            if (ActiveStatus != null) {

                //ESTARIA BIEN HACER UN DICCIONARIO PARA EVITAR HACER EL FOREACH
                foreach (NamedStatus namedStatus in Status) {
                    if (namedStatus.Name.Equals( ActiveStatus )) {

                        ActiveStatus = namedStatus.Status.Trigger(playerConversations);

                        break;
                    }
                }

            }

        }

    }

    [Serializable]
    public struct NamedStatus {
        public string Name;
        public ConversationStatus Status;
    }

}
