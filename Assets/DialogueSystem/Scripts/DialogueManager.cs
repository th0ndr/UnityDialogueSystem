using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class manages the text in the dialogues, the transition between sentences, animations, and such
public class DialogueManager : MonoBehaviour {
	
	public Text dialogueText;
	public Image imageText;
	public Animator animator;
	public float waitTime = .01f;
	public float voiceVolume = 1f;
	public bool doubleTap = true;
    public string nextKey = "z"

	private Queue<string> sentences;
	private Queue<Sprite> sprites;
	private Queue<AudioClip> voices;
	private AudioSource source;
    private AudioClip audioQueue;
	private bool parsing, finished;
	private string timeString, sentence;
    private Expression expression;



	void Start () 
	{
		sentences = new Queue<string>();
		sprites = new Queue<Sprite> ();
		voices = new Queue<AudioClip> ();
		imageText = imageText.GetComponent<Image>();
		source = GetComponent<AudioSource>();
	}

	void Update ()
	{
		if (Input.GetKeyDown ("z") && finished && doubleTap) 
		{
			
			DisplayNextSentence ();
			finished = false;

		}

        if (Input.GetKeyDown ("z") && doubleTap == false)
		{
			finished = true;
			DisplayNextSentence ();
		}
	}

    // Start new dialogue, and reset all data from previous dialogues
	public void StartDialogue(Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);

		voices.Clear ();
		sprites.Clear ();
		sentences.Clear();

		foreach(Sentence sentence in dialogue.sentences)
		{
            if(sentence.StandardExpression){
                expression = new Expression(sentence.character.standardExpression);
            }
            else
            {
                expression = FindExpression(sentence.expression, sentence.character);
            }


			foreach (string paragraph in sentence.text)
			{
				sprites.Enqueue (expression.Image);
				sentences.Enqueue(paragraph);
				voices.Enqueue (sentence.character.voice);
			}

		}

		DisplayNextSentence();
	}

    // Display next sentence in dialogue
	public void DisplayNextSentence()
	{
        print( sentences.Count );
		if (sentences.Count == 0)
		{
			EndDialogue();
			StopAllCoroutines();
			return;
		}

		imageText.sprite = sprites.Dequeue ();
		sentence = sentences.Dequeue();
		audioQueue = voices.Dequeue ();

		StopAllCoroutines();
		waitTime = 0f;
		StartCoroutine(TypeSentence(sentence, audioQueue));
	}

    //Find Expression in characcter, by expression name
    public Expression FindExpression(string name, Character character)
    {
        foreach(Expression expression in character.expressions)
        {
            if(expression.Name == name)
            {
                return (expression);
            } 
        }

        return null;
 
    }
    // Type sentence letter by letter, and parse the dialogue speed
	IEnumerator TypeSentence(string sentence, AudioClip audioQueue)
	{
		timeString = "";
		parsing = false;
		dialogueText.text = "";

		foreach(char letter in sentence.ToCharArray())
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
					waitTime = float.Parse (timeString);
					timeString = "";
				} 

				if (letter != '[' && letter != ']')
				{
					timeString += letter;

				}



			}
			else
			{
				if (Input.GetKeyDown ("z") && finished == false)
				{
					dialogueText.text = ParseSentence (sentence);
					finished = true;
					yield break;
				} 
				else 
				{
					dialogueText.text += letter;
					source.PlayOneShot (audioQueue, voiceVolume);
					yield return new WaitForSeconds (waitTime);
				}
			}

		}
		finished = true;
	}


    // Hides dialogue box
	public void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
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
