using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add global variables, that can be changed within code
public class VariablesManager : MonoBehaviour {
    
    [SerializeField]
    public IntegerDictionary integerDictionary;
    
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
