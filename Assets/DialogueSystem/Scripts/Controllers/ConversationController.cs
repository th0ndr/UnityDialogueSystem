namespace DialogueManager.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using DialogueManager.GameComponents;
    using DialogueManager.Models;
    using UnityEngine;

    public class ConversationController
    {
        private Conversation Model;

        public ConversationController(Conversation conversation)
        {
            this.Model = conversation;
        }
        
        public void Trigger(DialogueManager dialogueManager)
        {
            // ESTARIA BIEN HACER UN DICCIONARIO PARA EVITAR HACER EL FOREACH
            foreach (PendingConversation pending in this.Model.GameConversations.PendingConversations)
            {
                if (pending.ConversationName.Equals( this.Model.Name ))
                {
                    // POR AHORA SOLO PUEDE AGARRA UN STATUS
                    this.Model.ActiveStatus = pending.PendingStatus[0].StatusName;
                    this.Model.GameConversations.PendingConversations.Remove( pending );
                    // FALTA CAMBIAR METODO PARA VARIOS POSIBLES STATUS CON PRIORIDAD

                    break;
                }
            }

            if (this.Model.ActiveStatus != null)
            {
                // ESTARIA BIEN HACER UN DICCIONARIO PARA EVITAR HACER EL FOREACH
                foreach (ConversationStatus status in this.Model.Status)
                {
                    if (status.Name.Equals( this.Model.ActiveStatus ))
                    {
                        this.Model.ActiveStatus = this.TriggerStatus( status, dialogueManager );
                        break;
                    }
                }
            }
        }

        public string TriggerStatus(ConversationStatus status, DialogueManager dialogueManager)
        {
            // DENTRO DE PLAYERCONVERSATIONS SE DEBE HACER LA LOGICA PARA QUE NO SE REPITAN
            this.Model.GameConversations.ConversationsToAdd.AddRange( status.NewConversations );
            
            // ACTIVAR DIALOGUE
            // EL OBJETO SEA UN Dialogue en vez de un Sentence, quitar variables que no se usan a Dialogue

            Dialogue dialogue = new Dialogue
            {
                sentences = status.Dialogue,
            };

            dialogueManager.DialogueToShow = dialogue;
            return status.NextStatus;
        }
    }
}
