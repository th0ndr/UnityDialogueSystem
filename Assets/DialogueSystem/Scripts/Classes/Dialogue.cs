using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

// Dialogue class, with a variable that triggers it

[System.Serializable]
public class Dialogue
{

    public string variableName;
    public int variableValue;
	public Sentence[] sentences;

}
