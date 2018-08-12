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
        private List<float> speeds;
        private List<int> effects;
        private int fontSize = 30;
        private int boxSize = 380;
        private int currentX = 0;
        private int currentY = 0;

        private float currentSpeed = 0.01f;
        private int currentEffect = 0;

        public DialogueManager Model;
        public DialogueManagerController( DialogueManager Model )
        {
            this.Model = Model;
            this.sentences = new Queue<string>();
            this.sprites = new Queue<Sprite>();
            this.voices = new Queue<AudioClip>();
            this.letters = new List<GameObject>();
            this.speeds = new List<float>();
            this.effects = new List<int>();
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
            foreach (GameObject character in this.letters)
            {
                GameObject.Destroy( character );
            }

            this.currentSpeed = this.Model.WaitTime;
            this.currentEffect = 0;
            this.effects.Clear();
            this.speeds.Clear();
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
            string onlyWords = string.Empty;

            for (int i = 0; i < this.sentence.Length; i++)
            {
                if (this.sentence[i] == '[')
                {
                    i = this.changeSpeed( i );
                }
                else if (this.sentence[i] == '<')
                {
                    i = this.changeEffect( i );
                }
                else
                {
                    this.effects.Add( this.currentEffect );
                    if (this.sentence[i] != ' ')
                    {
                        this.speeds.Add( ( float )this.currentSpeed );
                    }
                    onlyWords += this.sentence[i];
                }
            }

            string[] words = onlyWords.Split( ' ' );
            int letterSpacing = ( int )( this.fontSize * 0.5 );
            int currentIndex = 0;
            foreach (string word in words)
            {
                GameObject wordObject = new GameObject( word, typeof( RectTransform ) );
                wordObject.transform.SetParent( this.Model.DialogueStartPoint );
                int wordSize = word.Length * letterSpacing;
                if (this.currentX + wordSize > this.boxSize)
                {
                    this.currentX = 0;
                    this.currentY -= ( int )( this.fontSize * 0.9 );
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
                    
                    if (this.effects[currentIndex] == 1)
                    {
                        letterObject.AddComponent<AngryEffect>();
                    }
                    this.letters.Add( letterObject );
                    currentIndex++;
                }
                currentX += wordSize + letterSpacing;
                currentIndex++;
            }
            return true;
        }

        public int changeSpeed( int i )
        {
            i++;
            string speed = string.Empty;
            while (this.sentence[i] != ']')
            {
                speed += this.sentence[i];
                i++;
            }
            this.currentSpeed = float.Parse( speed );
            return i;
        }

        public int changeEffect( int i )
        {
            i++;
            string effect = string.Empty;
            while (this.sentence[i] != '>')
            {
                effect += this.sentence[i];
                i++;
            }

            if (effect.Equals( "angry" ))
            {
                this.currentEffect = 1;
            }
            else
            {
                this.currentEffect = 0;
            }

            return i;
        }

        /// <summary>
        /// Method that will be typing and displaying the sentence and checking for [time] indicators
        /// </summary>
        /// <returns>Necessary for the WaitForSeconds function</returns>
        public IEnumerator TypeSentence()
        {
            timeString = "";
            parsing = false;
            int currentIndex = 0;
            foreach (GameObject letter in this.letters)
            {
                if (letter == null)
                {
                    break;
                }
                Text text = letter.GetComponent<Text>();
                text.color = new Color( 0.0f, 0.0f, 0.0f, 1.0f );
                this.Model.Source.PlayOneShot( audioQueue, this.Model.VoiceVolume );
                //yield return new WaitForSeconds( this.Model.WaitTime );
                yield return new WaitForSeconds( this.speeds[currentIndex] );
                currentIndex++;
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
