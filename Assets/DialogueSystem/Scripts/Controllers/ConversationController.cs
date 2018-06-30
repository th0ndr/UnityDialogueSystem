namespace DialogueManager.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using DialogueManager.Models;
    using DialogueManager.GameComponents;

    public class ConversationController
    {
        private Conversation Model;

        public ConversationController(Conversation conversation)
        {
            this.Model = conversation;
        }
        public void Trigger(GameConversationsComponent gameConversations, DialogueManagerComponent dialogueManager)
        {
            //ESTARIA BIEN HACER UN DICCIONARIO PARA EVITAR HACER EL FOREACH
            foreach (PendingConversation pending in gameConversations.Model.PendingConversations)
            {
                if (pending.ConversationName.Equals( this.Model.Name ))
                {

                    //POR AHORA SOLO PUEDE AGARRA UN STATUS
                    this.Model.ActiveStatus = pending.PendingStatus[0].StatusName;
                    gameConversations.Model.PendingConversations.Remove( pending );
                    //FALTA CAMBIAR METODO PARA VARIOS POSIBLES STATUS CON PRIORIDAD

                    break;
                }
            }

            if (this.Model.ActiveStatus != null)
            {

                //ESTARIA BIEN HACER UN DICCIONARIO PARA EVITAR HACER EL FOREACH
                foreach (ConversationStatus status in this.Model.Status)
                {

                    if (status.Name.Equals( this.Model.ActiveStatus ))
                    {
                        this.Model.ActiveStatus = this.TriggerStatus( status, gameConversations, dialogueManager );

                        break;
                    }
                }

            }

        }

        public string TriggerStatus(ConversationStatus status, GameConversationsComponent gameConversations, DialogueManagerComponent dialogueManager)
        {


            //DENTRO DE PLAYERCONVERSATIONS SE DEBE HACER LA LOGICA PARA QUE NO SE REPITAN
            gameConversations.AddConversations( status.NewConversations );



            //ACTIVAR DIALOGUE
            // EL OBJETO SEA UN Dialogue en vez de un Sentence, quitar variables que no se usan a Dialogue

            Dialogue dialogue = new Dialogue
            {
                sentences = status.Dialogue,
            };
            dialogueManager.StartDialogue( dialogue );

            return status.NextStatus;
        }
    }
}
