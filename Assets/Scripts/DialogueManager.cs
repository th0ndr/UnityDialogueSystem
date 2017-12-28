using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text dialogueText;
	public Image imageText;

	public Animator animator;

	private Queue<string> sentences;
	private Queue<Sprite> sprites;


	void Start () 
	{
		sentences = new Queue<string>();
		sprites = new Queue<Sprite> ();
		imageText = imageText.GetComponent<Image>();
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



		sentences.Clear();

		foreach(Sentence sentence in dialogue.sentences)
		{


			foreach (string paragraph in sentence.text)
			{
				sprites.Enqueue (sentence.character.standardExpression);
				sentences.Enqueue(paragraph);
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
	
		StopAllCoroutines();

		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		foreach(char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
	}
}
