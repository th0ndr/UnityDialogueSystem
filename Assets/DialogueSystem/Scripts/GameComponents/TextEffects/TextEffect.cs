using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class TextEffect
{
    public static Dictionary<string, ITextEffectBuilder> effects = new Dictionary<string, ITextEffectBuilder>
    {
        { "normal",  null },
        { "angry",   new AngryEffectBuilder()},
        { "wave",    new WaveEffectBuilder()}
    };

    public Vector3 m_startPos;
    public GameObject gameObject;

    public TextEffect(GameObject gameObject )
    {
        this.gameObject = gameObject;
        this.m_startPos = gameObject.transform.localPosition;
    }

    public abstract void Update();
}