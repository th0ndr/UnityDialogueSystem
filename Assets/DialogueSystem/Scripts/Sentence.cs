using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Each dialogue has several sentences, and each sentence has text, and character
[System.Serializable]
public class Sentence
{
	public Character character;
    public bool StandardExpression;
    public string expression;
	[TextArea(3, 10)]
	public string[] text;
}
