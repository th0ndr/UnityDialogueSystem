using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AngryEffectBuilder : ITextEffectBuilder
    {
        public TextEffect Build( GameObject gameObject )
        {
            return new AngryEffect(gameObject);
        }
    }