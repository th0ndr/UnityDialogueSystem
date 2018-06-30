namespace DialogueManager.Models { 

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;

    [Serializable]
    public class DialogueManager
    {
        public Text DialogueText;
        public Image ImageText;
        public Animator Animator;
        public float WaitTime = .01f;
        public float VoiceVolume = 1f;
        public bool DoubleTap = true;
        public string NextKey = "z";

        public AudioSource source { get; set; }
        public bool finished { get; set; }
    }
}
