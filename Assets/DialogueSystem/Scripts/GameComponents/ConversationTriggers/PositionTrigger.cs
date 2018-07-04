namespace DialogueManager.GameComponents.Triggers
{

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    // CLASE FEA
    //TEMPORAL CLASS
    //CLASE PARA PRUEBAS
    public class PositionTrigger : MonoBehaviour
    {
        //NACADAS PARA PROBAR
        public GameObject Tracked;
        private bool wasTriggered = false;

        private Transform tPosition;

        private void Start()
        {
            tPosition = Tracked.GetComponent<Transform>();
        }

        private void Update()
        {
            //MAS NACADAS PARA PROBAR
            if (tPosition.position.x < this.transform.position.x &&
                tPosition.position.y > this.transform.position.y)
            {

                if (!wasTriggered)
                {
                    wasTriggered = true;

                    ConversationComponent conversation = this.GetComponent<ConversationComponent>();
                    if (conversation != null)
                    {
                        conversation.Trigger( );
                    }
                }
            }
            else if (wasTriggered)
            {
                wasTriggered = false;
            }
        }

    }
}