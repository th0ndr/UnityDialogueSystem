using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesManager : MonoBehaviour {
    
    [SerializeField]
    IntegerDictionary m_integerDictionary;
    public IDictionary<string, int> IntegerDictionary
    {
        get { return m_integerDictionary; }
        set { m_integerDictionary.CopyFrom(value); }
    }


    private static VariablesManager instance = null;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
