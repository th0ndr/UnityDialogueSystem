namespace DialogueManager.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;
    
    /// <summary>
    /// Dialogue class, with a variable that triggers it
    /// </summary>
    [System.Serializable]
    public class Dialogue
    {
        public Sentence[] sentences;
    }
}