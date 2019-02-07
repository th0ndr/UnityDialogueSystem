using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class LetterComponent : MonoBehaviour
{
    public Letter Model;

    private void Update()
    {
        if (this.Model.Effect != null)
        {
            this.Model.Effect.Update();
        }
    }
}
