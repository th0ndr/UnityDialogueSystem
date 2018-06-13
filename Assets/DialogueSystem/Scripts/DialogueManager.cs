using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	public void StartDialogue(Dialogue dialogue)
	{
		animator.SetBool("IsOpen", true);

		voices.Clear ();
		sprites.Clear ();
		sentences.Clear();

		foreach(Sentence sentence in dialogue.sentences)
		{


			foreach (string paragraph in sentence.text)
			{
				sprites.Enqueue (sentence.character.standardExpression);
				sentences.Enqueue(paragraph);
				voices.Enqueue (sentence.character.voice);
			}

		}

		DisplayNextSentence();
	}

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



	public void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
	}

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
