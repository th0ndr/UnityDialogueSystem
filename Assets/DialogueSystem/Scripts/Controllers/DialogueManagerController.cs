namespace DialogueManager.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using DialogueManager.Models;
    using UnityEngine;
    using UnityEngine.UI;

    public class DialogueManagerController : MonoBehaviour
    {
        private Queue<string> sentences;
        private Queue<Sprite> sprites;
        private Queue<AudioClip> voices;
        private AudioClip audioQueue;
        private bool parsing;
        private string timeString, sentence;
        private Expression expression;

        private List<LetterComponent> letters;
        private List<float> speeds;
        private List<ITextEffectBuilder> effects;
        private int fontSize = 30;
        private int boxSize = 380;
        private int currentX = 0;
        private int currentY = 0;

        private float currentSpeed = 0.01f;
        private ITextEffectBuilder currentEffect = null;

        public DialogueManager Model;
        public DialogueManagerController( DialogueManager Model )
        {
            this.Model = Model;
            this.sentences = new Queue<string>();
            this.sprites = new Queue<Sprite>();
            this.voices = new Queue<AudioClip>();
            this.letters = new List<LetterComponent>();
            this.speeds = new List<float>();
            this.effects = new List<ITextEffectBuilder>();
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
            foreach (LetterComponent letter in this.letters)
            {
                GameObject.Destroy( letter.gameObject );
            }

            this.currentSpeed = this.Model.WaitTime;
            this.currentEffect = null;
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
            int currentIndexEffects = 0;
            int currentIndexSpeeds = 0;
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

                    LetterComponent letterComponent = letterObject.AddComponent<LetterComponent>();
                    
                    Letter newLetter = new Letter
                    {
                        Character = word[i],
                        Speed = this.speeds[currentIndexSpeeds],
                        isActive = false
                    };
                    if (this.effects[currentIndexEffects] != null)
                    {
                        newLetter.Effect = this.effects[currentIndexEffects].Build( letterObject );
                    }
                    letterComponent.Model = newLetter;
                    this.letters.Add( letterComponent );
                    currentIndexEffects++;
                    currentIndexSpeeds++;
                }
                currentX += wordSize + letterSpacing;
                currentIndexEffects++;
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

            if (TextEffect.effects.ContainsKey( effect ))
            {
                this.currentEffect = TextEffect.effects[effect];
            }
            else
            {
                this.currentEffect = null;
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
            foreach (LetterComponent letter in this.letters)
            {
                if (letter == null)
                {
                    break;
                }
                Text text = letter.GetComponent<Text>();
                text.color = new Color( 0.0f, 0.0f, 0.0f, 1.0f );
                this.Model.Source.PlayOneShot( audioQueue, this.Model.VoiceVolume );
                yield return new WaitForSeconds( letter.Model.Speed );
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
