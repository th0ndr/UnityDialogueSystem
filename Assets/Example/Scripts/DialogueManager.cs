using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	[HideInInspector]
	public Text dialogueText;

	[HideInInspector]
	public Image imageText;

	[HideInInspector]
	public Animator animator;

	[HideInInspector]
	public float waitTime = .01f;

	public float voiceVolume = 1f;


	private Queue<string> sentences;
	private Queue<Sprite> sprites;
	private Queue<AudioClip> voices;
	private AudioSource source;
	private bool betweenChars = true;


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
		if (Input.GetKeyDown (KeyCode.Z)) 
		{
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
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		imageText.sprite = sprites.Dequeue ();
		string sentence = sentences.Dequeue();
		AudioClip audio = voices.Dequeue ();

		StopAllCoroutines();

		StartCoroutine(TypeSentence(sentence, audio));
	}

	IEnumerator TypeSentence(string sentence, AudioClip audio)
	{
		string time = "";
		bool parsing = false;
		dialogueText.text = "";
		foreach(char letter in sentence.ToCharArray())
		{

			if (letter == '[') 
			{
				parsing = true;
			}

			if (parsing) 
			{
				parsing = true;
				if (letter == ']') {
					parsing = false;
					waitTime = float.Parse (time) * .001f;
					time = "";
				} 

				if (letter != '[' && letter != ']') {
					time += letter;
				}

			}
			else
			{
				dialogueText.text += letter;
				source.PlayOneShot (audio, voiceVolume);
				yield return new WaitForSeconds(waitTime);
			}

		}
	}



	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
	}
}
