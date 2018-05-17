using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializableDictionaryExample : MonoBehaviour {
	// The dictionaries can be accessed throught a property
	[SerializeField]
	IntegerDictionary m_integerDictionary;
	public IDictionary<string, int> IntegerDictionary
	{
		get { return m_integerDictionary; }
		set { m_integerDictionary.CopyFrom (value); }
	}

}
