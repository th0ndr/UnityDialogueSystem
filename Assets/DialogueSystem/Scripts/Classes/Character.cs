using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueManager.Models;

/// <summary>
/// A character scriptable object, can be created in Unity Editor
/// </summary>
[CreateAssetMenu( fileName = "New Character", menuName = "Character" )]
public class Character : ScriptableObject
{
    /// <summary> Name of the <see cref="Character"/> </summary>
    public string Name;

    /// <summary> List of <see cref="Expression"/> of the <see cref="Character"/>. </summary>
    public List<Expression> Expressions;

    /// <summary> Sound that will be played each time a letter or character is added to the dialogue display </summary>
    public AudioClip Voice;
}