namespace DialogueManager.GameComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;
    using DialogueManager.Controllers;
    using DialogueManager.Models;

    public class GameConversationsComponent : MonoBehaviour
    {
        public GameConversations Model;
        private GameConversationsController Controller;

        private void Start()
        {
            if (this.Model == null)
            {
                this.Model = new GameConversations();
            }
            this.Controller = new GameConversationsController( Model );
        }

        public void AddConversations(List<NewConversation> newConversations)
        {
            this.Controller.AddConversations( newConversations );
        }
    }
}