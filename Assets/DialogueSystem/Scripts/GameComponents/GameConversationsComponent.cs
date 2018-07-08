namespace DialogueManager.GameComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DialogueManager.Controllers;
    using DialogueManager.Models;
    using UnityEngine;

    /// <summary>
    /// Component of all the Pending Conversations in the Game
    /// </summary>
    public class GameConversationsComponent : MonoBehaviour
    {
        /// <summary> Model of the GameConversations </summary>
        public GameConversations Model;

        /// <summary> Controller of the GameConversations </summary>
        private GameConversationsController controller;

        /// <summary> Creation of Controller and Model </summary>
        private void Awake()
        {
            if (this.Model == null)
            {
                this.Model = new GameConversations();
            }

            this.controller = new GameConversationsController( this.Model );
        }

        /// <summary> Is called once per frame. </summary>
        private void Update()
        {
            if (this.Model.ConversationsToAdd.Count > 0)
            {
                this.AddConversation();
            }
        }

        /// <summary> Adds Pending Conversations to the List </summary>
        private void AddConversation()
        {
            this.controller.AddConversation();
        }
    }
}