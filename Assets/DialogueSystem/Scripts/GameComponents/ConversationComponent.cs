namespace DialogueManager.GameComponents
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using DialogueManager.Controllers;
    using DialogueManager.Models;
    
    public class ConversationComponent : MonoBehaviour
    {
        public Conversation Model = new Conversation();

        private PlayerConversationsComponent playerConversations;

        public void Trigger(PlayerConversationsComponent playerConversations)
        {
            this.playerConversations = playerConversations;
            //ESTARIA BIEN HACER UN DICCIONARIO PARA EVITAR HACER EL FOREACH
            foreach (PendingConversation pending in playerConversations.PendingConversations)
            {
                if (pending.ConversationName.Equals( this.Model.Name ))
                {

                    //POR AHORA SOLO PUEDE AGARRA UN STATUS
                    this.Model.ActiveStatus = pending.PendingStatus[0].StatusName;
                    playerConversations.PendingConversations.Remove( pending );
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
                        this.Model.ActiveStatus = this.TriggerStatus( status );

                        break;
                    }
                }

            }

        }

        public string TriggerStatus(ConversationStatus status)
        {


            //DENTRO DE PLAYERCONVERSATIONS SE DEBE HACER LA LOGICA PARA QUE NO SE REPITAN
            this.playerConversations.AddConversations( status.NewConversations );



            //ACTIVAR DIALOGUE
            // EL OBJETO SEA UN Dialogue en vez de un Sentence, quitar variables que no se usan a Dialogue

            Dialogue dialogue = new Dialogue
            {
                sentences = status.Dialogue,
            };
            playerConversations.DialogueManager.StartDialogue( dialogue );

            return status.NextStatus;
        }

    }
}