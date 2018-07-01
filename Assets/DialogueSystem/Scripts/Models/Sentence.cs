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
        /// <summary>
        /// Character that will be talking
        /// </summary>
        public Character character;

        // IGUAL Y PODEMOS QUITAR ESTA
        public bool StandardExpression = true;

        /// <summary>
        /// Expression of the Character when talking
        /// </summary>
        public string expression;

        /// <summary>
        /// The text that will be displayed.
        /// </summary>
        [TextArea( 3, 10 )]
        public string paragraph;
    }
}