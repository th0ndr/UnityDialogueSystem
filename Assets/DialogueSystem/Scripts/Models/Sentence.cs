namespace DialogueManager.Models
{
    using UnityEngine;

    /// <summary>
    /// Each dialogue has several sentences, and each sentence has text, and character
    /// </summary>
    [System.Serializable]
    public class Sentence
    {
        /// <summary> Character who will be talking </summary>
        public Character Character;

        /// <summary> Index of the used Expression in the list of the character expressions </summary>
        public int ExpressionIndex;

        /// <summary> The text that will be displayed. </summary>
        [TextArea( 3, 10 )]
        public string Paragraph;
    }
}