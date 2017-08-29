using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject container;
    public Camera plauyerCamera;
    public Animator animator;

    private bool canMove = true;
	
	// Update is called once per frame
	void Update () {
        if (canMove) {
            if (Input.GetKeyDown(KeyCode.W)) {
                animator.SetBool("Running", true);
            } else if (Input.GetKeyUp(KeyCode.W)) {
                animator.SetBool("Running", false);
            } else if (Input.GetKeyDown(KeyCode.A)) {
                animator.SetBool("WalkLeft", true);
            }else if(Input.GetKeyUp(KeyCode.A)){
                animator.SetBool("WalkLeft", false);
			} else if (Input.GetKeyDown(KeyCode.D)) {
				animator.SetBool("WalkRight", true);
            } else if (Input.GetKeyUp(KeyCode.D)) {
                animator.SetBool("WalkRight", false);
			} else if (Input.GetKeyDown(KeyCode.S)) {
				animator.SetBool("WalkBack", true);
            }else if (Input.GetKeyUp(KeyCode.S)) {
                animator.SetBool("WalkBack", false);
    		}

		if(Input.GetKey(KeyCode.RightArrow)){
                container.transform.Rotate(new Vector3(0, 2.5f,0));
            }else if(Input.GetKey(KeyCode.LeftArrow)){
                container.transform.Rotate(new Vector3(0, -2.5f,0));
            }
        }
	}

    public void setCanMove(bool flag){
        canMove = flag;
    }

    public Camera getCamera(){
        return plauyerCamera;
    }
}
