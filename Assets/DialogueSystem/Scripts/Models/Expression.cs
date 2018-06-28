namespace DialogueManager.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    // Different expression faces for the characters
    [System.Serializable]
    public class Expression
    {
        [Header( "Expression" )]
        public string Name;
        public Sprite Image;

        public Expression(Sprite Image, string Name)
        {
            this.Image = Image;
            this.Name = Name;
        }
    }
}