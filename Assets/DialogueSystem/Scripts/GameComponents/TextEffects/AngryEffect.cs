using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class AngryEffect : TextEffect
{
    public float Magnitude = 2f;

    public AngryEffect( GameObject gameObject ) : base( gameObject )
    {
    }
    public override void Update()
    {
        float x = Random.Range( -Magnitude, Magnitude );
        float y = Random.Range( -Magnitude, Magnitude );
        this.gameObject.transform.localPosition = m_startPos + new Vector3( x, y, 0.0f );
    }
}
