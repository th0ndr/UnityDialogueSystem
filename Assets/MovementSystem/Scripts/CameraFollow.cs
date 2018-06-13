using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float Speed = 0.1f;
    private Camera myCam;
	void Start () {
        myCam = GetComponent<Camera>();
	}
	void FixedUpdate () {
        myCam.orthographicSize = (Screen.height / 1000f) ;
        if (target) {
            transform.position = Vector3.Lerp(transform.position, target.position, Speed)
                + new Vector3(0, 0, -10);
        }
	}
}
