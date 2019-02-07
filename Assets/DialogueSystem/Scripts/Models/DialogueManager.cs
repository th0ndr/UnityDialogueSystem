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
        /// <summary> Prefabs of the Dialogue Box, text and image. </summary>
        public GameObject CanvasObjectsPrefab;

        /// <summary> Prefab of the GameConversations. </summary>
        public GameObject GameConversationsPrefab;

        /// <summary> Time between each letter. </summary>
        public float WaitTime = .01f;

        /// <summary> Volume of the Voice of the characters. </summary>
        public float VoiceVolume = 1f;

        /// <summary> Is double tap. </summary>
        public bool DoubleTap = true;

        /// <summary> Key which must be pressed to continue to the next Sentence. </summary>
        public string NextKey = "z";

        /// <summary> Font </summary>
        public Font Font;

        /// <summary> FontMaterial </summary>
        public Material Material;

        /// <summary> Gets or sets the Text that is being displayed on the Scene. </summary>
        public Transform DialogueStartPoint { get; set; }

        /// <summary> Gets or sets the Image that is being displayed on the Scene. </summary>
        public Image ImageText { get; set; }

        /// <summary> Gets or sets the Animation that causes the Dialogue box to go up or down. </summary>
        public Animator Animator { get; set; }

        /// <summary> Gets or sets the Audio that the current dialogue is showing. </summary>
        public AudioSource Source { get; set; }

        /// <summary> Gets or sets a value indicating whether the Dialogue has finished or not. </summary>
        public bool Finished { get; set; }

        /// <summary> Gets or sets the <see cref="Dialogue"/> that will be displayed. </summary>
        public Dialogue DialogueToShow { get; set; }
    }
}
