namespace DialogueManager.Models
{ 
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Model of the main Dialogue Manager
    /// </summary>
    [Serializable]
    public class DialogueManager
    {
        public GameObject CanvasObjectsPrefab;
        public GameObject GameConversationsPrefab;
        //public GameConversations GameConversations;

        /// <summary>
        /// Text that is being displayed on the Scene.
        /// </summary>
        public Text DialogueText { get; set; }

        /// <summary>
        /// Image that is being displayed on the Scene.
        /// </summary>
        public Image ImageText { get; set; }

        /// <summary>
        /// Animation that causes the Dialogue box to go up or down.
        /// </summary>
        public Animator Animator { get; set; }

        /// <summary>
        /// Time between each letter.
        /// </summary>
        public float WaitTime = .01f;

        /// <summary>
        /// Volume of the Voice of the characters.
        /// </summary>
        public float VoiceVolume = 1f;
        
        public bool DoubleTap = true;

        /// <summary>
        /// Key which must be pressed to continue to the next Sentence
        /// </summary>
        public string NextKey = "z";

        public AudioSource source { get; set; }
        public bool finished { get; set; }
        public Dialogue DialogueToShow { get; set; }
    }
}
