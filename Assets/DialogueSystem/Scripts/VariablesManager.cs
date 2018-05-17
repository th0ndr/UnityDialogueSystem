using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesManager : MonoBehaviour {

    public static IntegerFlag[] integerFlags;

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
