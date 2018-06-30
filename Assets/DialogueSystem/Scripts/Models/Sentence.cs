namespace DialogueManager.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    /// <summary>
    /// Each dialogue has several sentences, and each sentence has text, and character
    /// </summary>
    [System.Serializable]
    public class Sentence
    {
        public Character character;
        public bool StandardExpression = true;
        public string expression;
        [TextArea( 3, 10 )]
        public string paragraph;
    }
}