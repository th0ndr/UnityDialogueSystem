using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WaveEffectBuilder : ITextEffectBuilder
{
    private float currentOffset = 0;
    public TextEffect Build( GameObject gameObject )
    {
        WaveEffect effect = new WaveEffect( gameObject );
        effect.Offset = currentOffset;
        currentOffset += 0.08f;
        return effect;
    }
}
