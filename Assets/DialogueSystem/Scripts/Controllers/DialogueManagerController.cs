namespace DialogueManager.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
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

        private List<GameObject> letters;
        private int fontSize = 30;
        private int boxSize = 380;
        private int currentX = 0;
        private int currentY = 0;

        public DialogueManager Model;
        public DialogueManagerController( DialogueManager Model )
        {
            this.Model = Model;
            this.sentences = new Queue<string>();
            this.sprites = new Queue<Sprite>();
            this.voices = new Queue<AudioClip>();
            this.letters = new List<GameObject>();
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
            foreach (GameObject character in this.letters) {
                GameObject.Destroy( character );
            }
            this.letters.Clear();
            this.currentX = 0;
            this.currentY = 0;

            if (sentences.Count == 0)
            {
                EndDialogue();
                return false;
            }

            this.Model.ImageText.sprite = sprites.Dequeue();
            this.sentence = sentences.Dequeue();
            this.audioQueue = voices.Dequeue();
            this.Model.WaitTime = 0f;

            string[] words = this.sentence.Split( ' ' );
            int letterSpacing = ( int )( this.fontSize * 0.5 );

            foreach (string word in words)
            {
                GameObject wordObject = new GameObject( word, typeof( RectTransform ) );
                wordObject.transform.SetParent( this.Model.DialogueStartPoint );
                int wordSize = word.Length * letterSpacing;
                if (this.currentX + wordSize > this.boxSize)
                {
                    this.currentX = 0;
                    this.currentY -= (int) (this.fontSize * 0.9);
                }
                wordObject.GetComponent<RectTransform>().localPosition = new Vector3( currentX, currentY, 0 );

                for (int i = 0; i < word.Length; i++)
                {
                    GameObject letterObject = new GameObject( word[i].ToString() );
                    letterObject.transform.SetParent( wordObject.transform );

                    Text myText = letterObject.AddComponent<Text>();

                    RectTransform parentTransform = this.Model.DialogueStartPoint.GetComponentInParent<RectTransform>();
                    myText.text = word[i].ToString();
                    myText.alignment = TextAnchor.LowerCenter;
                    myText.fontSize = this.fontSize;
                    myText.font = this.Model.Font;
                    myText.material = this.Model.Material;
                    myText.GetComponent<RectTransform>().localPosition = new Vector3( i * letterSpacing, 0, 0 );

                    myText.color = new Color( 0.0f, 0.0f, 0.0f, 0.0f );
                    RectTransform rt = letterObject.GetComponentInParent<RectTransform>();
                    rt.sizeDelta = new Vector2( this.fontSize, this.fontSize );
                    rt.pivot = new Vector2( 0, 1 );
                    letterObject.AddComponent<ShakeText>();
                    //letterObject.AddComponent<WaveText>();
                    //letterObject.GetComponent<WaveText>().Offset = .15f * i;

                    this.letters.Add( letterObject );
                }
                currentX += wordSize + letterSpacing;
            }
            return true;
        }

        /// <summary>
        /// Method that will be typing and displaying the sentence and checking for [time] indicators
        /// </summary>
        /// <returns>Necessary for the WaitForSeconds function</returns>
        public IEnumerator TypeSentence()
        {
            timeString = "";
            parsing = false;
            // this.Model.DialogueText.text = "";

            foreach (GameObject letter in this.letters)
            {
                if (letter == null) {
                    break;
                }
                Text text = letter.GetComponent<Text>();
                text.color = new Color( 0.0f, 0.0f, 0.0f, 1.0f );
                this.Model.Source.PlayOneShot( audioQueue, this.Model.VoiceVolume );
                yield return new WaitForSeconds( this.Model.WaitTime );
                /*
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
                        // this.Model.DialogueText.text = ParseSentence( sentence );
                        this.Model.Finished = true;
                        yield break;
                    }
                    else
                    {
                        // this.Model.DialogueText.text += letter;
                        this.Model.Source.PlayOneShot( audioQueue, this.Model.VoiceVolume );
                        yield return new WaitForSeconds( this.Model.WaitTime );
                    }
                }
                */
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
        private string ParseSentence( string sentence )
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
