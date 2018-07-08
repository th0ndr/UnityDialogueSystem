
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueManager.Models;

// A character scriptable object, can be created in Unity Editor

[CreateAssetMenu( fileName = "New Character", menuName = "Character" )]
public class Character : ScriptableObject
{
    public string characterName;
    public Sprite standardExpression;
    public List<Expression> expressions;
    public AudioClip voice;
}