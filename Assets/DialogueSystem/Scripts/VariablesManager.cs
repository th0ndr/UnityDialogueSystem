using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesManager : MonoBehaviour {
    
    [SerializeField]
    public IntegerDictionary integerDictionary;
    public IDictionary<string, int> IntegerDictionary
    {
        get { return integerDictionary; }
        set { integerDictionary.CopyFrom(value); }
    }


    public static VariablesManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
