using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rBody;
    private Animator animator;
    public int SpeedMultiplier = 1;
    
	void Start () {
        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	void Update () {
        Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(movement_vector != Vector2.zero) {
            animator.SetBool( "isWalking", true );
            animator.SetFloat( "input_x", movement_vector.x );
            animator.SetFloat( "input_y", movement_vector.y );
        }
        else {
            animator.SetBool( "isWalking", false );
        }

        rBody.MovePosition( rBody.position + movement_vector * Time.deltaTime * SpeedMultiplier);
	}
}
