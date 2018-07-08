namespace DialogueManager.Controllers { 
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DialogueManager.Models;
    using UnityEngine;
    using UnityEngine.UI;
    public class DialogueManagerController
    {
        private Queue<string> sentences;
        private Queue<Sprite> sprites;
        private Queue<AudioClip> voices;
        private AudioClip audioQueue;
        private bool parsing;
        private string timeString, sentence;
        private Expression expression;

        public DialogueManager Model;
        public DialogueManagerController(DialogueManager Model)
        {
            this.Model = Model;
            sentences = new Queue<string>();
            sprites = new Queue<Sprite>();
            voices = new Queue<AudioClip>();
        }

        /// <summary>
        /// Start new dialogue, and reset all data from previous dialogues
        /// </summary>
        /// <param name="dialogue">Dialogue that will be displayed</param>
        public void StartDialogue()
        {
            Dialogue dialogue = this.Model.DialogueToShow;
            this.Model.DialogueToShow = null;
            this.Model.Animator.SetBool( "IsOpen", true );
            voices.Clear();
            sprites.Clear();
            sentences.Clear();

            foreach (Sentence sentence in dialogue.Sentences)
            {
                expression = sentence.Character.Expressions[sentence.ExpressionIndex];
                /*
                if (sentence.StandardExpression)
                {
                    expression = new Expression( sentence.character.standardExpression, "Standard" );
                }
                else
                {
                    expression = FindExpression( sentence.expression, sentence.character );
                }
                */
                sprites.Enqueue( expression.Image );
                sentences.Enqueue( sentence.Paragraph );
                voices.Enqueue( sentence.Character.Voice );
            }
        }
        
        /// <summary>
        /// Display next sentence in dialogue
        /// </summary>
        /// <returns>If there was a Sentence to be displayed or not</returns>
        public bool DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return false;
            }

            this.Model.ImageText.sprite = sprites.Dequeue();
            sentence = sentences.Dequeue();
            audioQueue = voices.Dequeue();
            this.Model.WaitTime = 0f;
            return true;
        }

        
        /// <summary>
        /// Find Expression in characcter, by expression name.
        /// </summary>
        /// <param name="name">Name of the Expression.</param>
        /// <param name="character">Character in which the Expression will be found.</param>
        /// <returns>Expression that was being looked for, returns null if there wasn't any.</returns>
        private Expression FindExpression(string name, Character character)
        {
            foreach (Expression expression in character.Expressions)
            {
                if (expression.Name.Equals( name ))
                {
                    return ( expression );
                }
            }

            return null;
        }

        /// <summary>
        /// Method that will be typing and displaying the sentence and checking for [time] indicators
        /// </summary>
        /// <returns>Necessary for the WaitForSeconds function</returns>
        public IEnumerator TypeSentence()
        {
            timeString = "";
            parsing = false;
            this.Model.DialogueText.text = "";

            foreach (char letter in sentence.ToCharArray())
            {
                if (letter == '[')
                {
                    parsing = true;
                }

                if (parsing)
                {
                    if (letter == ']')
                    {
                        parsing = false;
                        this.Model.WaitTime = float.Parse( timeString );
                        timeString = "";
                    }

                    if (letter != '[' && letter != ']')
                    {
                        timeString += letter;
                    }
                }
                else
                {
                    if (Input.GetKeyDown( this.Model.NextKey ) && this.Model.Finished == false)
                    {
                        this.Model.DialogueText.text = ParseSentence( sentence );
                        this.Model.Finished = true;
                        yield break;
                    }
                    else
                    {
                        this.Model.DialogueText.text += letter;
                        this.Model.Source.PlayOneShot( audioQueue, this.Model.VoiceVolume );
                        yield return new WaitForSeconds( this.Model.WaitTime );
                    }
                }
            }
            this.Model.Finished = true;
        }
        
        /// <summary>
        /// Hides dialogue box
        /// </summary>
        public void EndDialogue()
        {
            this.Model.Animator.SetBool( "IsOpen", false );
        }
        
        /// <summary>
        /// Parses the sentence, for fully displaying it.
        /// </summary>
        /// <param name="sentence">Sentence to be parsed.</param>
        /// <returns>Returns the complete sentence witout the [time] labels</returns>
        private string ParseSentence(string sentence)
        {
            string parsedSentence = "";
            bool normalSentence = true;
            foreach (char letter in sentence.ToCharArray())
            {
                if (letter == '[')
                {
                    normalSentence = false;
                }

                if (letter == ']')
                {
                    normalSentence = true;
                }

                if (normalSentence)
                {
                    if (letter != ']')
                    {
                        parsedSentence += letter;
                    }
                }
            }

            return parsedSentence;
        }
    }
}
