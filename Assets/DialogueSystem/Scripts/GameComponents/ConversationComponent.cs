
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            foreach (NamedStatus namedStatus in this.Model.Status)
            {
                
                if (namedStatus.Name.Equals( this.Model.ActiveStatus ))
                {
                    this.Model.ActiveStatus =  this.TriggerStatus(namedStatus);

                    break;
                }
            }

        }

    }

    public string TriggerStatus(NamedStatus namedStatus)
    {


        //DENTRO DE PLAYERCONVERSATIONS SE DEBE HACER LA LOGICA PARA QUE NO SE REPITAN
        this.playerConversations.AddConversations( namedStatus.Status.NewConversations );



        //ACTIVAR DIALOGUE
        // EL OBJETO SEA UN Dialogue en vez de un Sentence, quitar variables que no se usan a Dialogue

        Dialogue dialogue = new Dialogue
        {
            sentences = namedStatus.Status.Dialogue,
        };
        playerConversations.DialogueManager.StartDialogue( dialogue );

        return namedStatus.Status.NextStatus;
    }

}

[Serializable]
public struct NamedStatus
{
    public string Name;
    public ConversationStatus Status;
}
