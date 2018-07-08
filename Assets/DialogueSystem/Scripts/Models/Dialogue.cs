namespace DialogueManager.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;
    
    /// <summary>
    /// Dialogue class, with all the sentences that will be displayed in the Status
    /// </summary>
    [System.Serializable]
    public class Dialogue
    {
        /// <summary>
        /// A List in which each <see cref="Sentence"/> contains a game dialogue line.
        /// </summary>
        public List<Sentence> Sentences;
    }
}