using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeText : MonoBehaviour {

        public float Magnitude = 2f;

        private Vector3 m_startPos;

        private void Start()
        {
            m_startPos = transform.localPosition;
        }

        private void Update()
        {
            float x = Random.Range(-Magnitude, Magnitude);
            float y = Random.Range(-Magnitude, Magnitude);
            transform.localPosition = m_startPos + new Vector3(x, y, 0.0f);
        }

}
