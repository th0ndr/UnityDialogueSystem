using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sentence
{
	public Character character;
	[TextArea(3, 10)]
	public string[] text;

}
