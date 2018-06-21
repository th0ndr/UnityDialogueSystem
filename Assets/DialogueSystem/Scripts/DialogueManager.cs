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


	private Queue<string> sentences;
	private Queue<Sprite> sprites;
	private Queue<AudioClip> voices;
	private AudioSource source;
    private AudioClip audioQueue;
	private bool parsing, finished;
	private string timeString, sentence;



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
		if (Input.GetKeyDown (KeyCode.Z) && finished && doubleTap) 
		{
			
			DisplayNextSentence ();
			finished = false;

		}

		if (Input.GetKeyDown (KeyCode.Z) && doubleTap == false)
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
            sprites.Enqueue( sentence.character.standardExpression );
            sentences.Enqueue( sentence.paragraph );
            voices.Enqueue( sentence.character.voice );
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
					waitTime = float.Parse (timeString) * .001f;
					timeString = "";
				} 

				if (letter != '[' && letter != ']')
				{
					timeString += letter;

				}



			}
			else
			{
				if (Input.GetKeyDown (KeyCode.Z) && finished == false)
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
