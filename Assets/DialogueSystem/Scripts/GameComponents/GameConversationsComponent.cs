namespace DialogueManager.GameComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;
    using DialogueManager.Controllers;
    using DialogueManager.Models;

    /// <summary>
    /// Component of all the Pending Conversations in the Game
    /// </summary>
    public class GameConversationsComponent : MonoBehaviour
    {
        public GameConversations Model;
        private GameConversationsController Controller;

        /// <summary>
        /// Creation of Controller and Model
        /// </summary>
        private void Awake()
        {
            if (this.Model == null)
            {
                this.Model = new GameConversations();
            }
            this.Controller = new GameConversationsController( Model );
        }

        /// <summary>
        /// Is called once per frame.
        /// </summary>
        private void Update()
        {
            
            if(this.Model.ConversationsToAdd.Count > 0)
            {
                AddConversation();
            }
        }

        /// <summary>
        /// Adds Pending Conversations to the List
        /// </summary>
        /// <param name="newConversations">List of the new Pending Conversations to be added</param>
        public void AddConversation()
        {
            this.Controller.AddConversation();
        }
    }
}