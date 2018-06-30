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

        // Start new dialogue, and reset all data from previous dialogues
        public void StartDialogue(Dialogue dialogue)
        {
            this.Model.Animator.SetBool( "IsOpen", true );

            voices.Clear();
            sprites.Clear();
            sentences.Clear();


            foreach (Sentence sentence in dialogue.sentences)
            {
                if (sentence.StandardExpression)
                {
                    expression = new Expression( sentence.character.standardExpression, "Standard" );
                }
                else
                {
                    expression = FindExpression( sentence.expression, sentence.character );
                }

                sprites.Enqueue( expression.Image );
                sentences.Enqueue( sentence.paragraph );
                voices.Enqueue( sentence.character.voice );
            }
        }

        // Display next sentence in dialogue
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


        //Find Expression in characcter, by expression name
        public Expression FindExpression(string name, Character character)
        {
            foreach (Expression expression in character.expressions)
            {
                if (expression.Name.Equals( name ))
                {
                    return ( expression );
                }
            }

            return null;

        }
        // Type sentence letter by letter, and parse the dialogue speed
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
                    if (Input.GetKeyDown( this.Model.NextKey ) && this.Model.finished == false)
                    {
                        this.Model.DialogueText.text = ParseSentence( sentence );
                        this.Model.finished = true;
                        yield break;
                    }
                    else
                    {
                        this.Model.DialogueText.text += letter;
                        this.Model.source.PlayOneShot( audioQueue, this.Model.VoiceVolume );
                        yield return new WaitForSeconds( this.Model.WaitTime );
                    }
                }
            }
            this.Model.finished = true;
        }


        // Hides dialogue box
        public void EndDialogue()
        {
            this.Model.Animator.SetBool( "IsOpen", false );
        }

        // Parses the sentence
        string ParseSentence(string sentence)
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
