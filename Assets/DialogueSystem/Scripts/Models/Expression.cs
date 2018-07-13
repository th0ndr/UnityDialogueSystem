namespace DialogueManager.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    
    /// <summary>
    /// Different expression faces for the characters
    /// </summary>
    [System.Serializable]
    public class Expression
    {
        /// <summary> Name of the Character Expression </summary>
        [Header( "Expression" )]
        public string Name;

        /// <summary> Image that will be displayed in the Expression </summary>
        public Sprite Image;

        /// <summary> Initializes a new instance of the <see cref="Expression"/> class. </summary>
        /// <param name="name">Name of the Expression</param>
        /// <param name="image">Image showed in the Dialogue</param>
        public Expression( string name, Sprite image )
        {
            this.Image = image;
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Expression"/> class.
        /// </summary>
        public Expression()
        {
            this.Name = string.Empty;
        }
    }
}