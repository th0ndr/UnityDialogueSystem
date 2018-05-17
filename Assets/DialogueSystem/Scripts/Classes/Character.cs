using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
	public string characterName;
	public Sprite standardExpression;
	public Expression[] expressions;
	public AudioClip voice;

}
