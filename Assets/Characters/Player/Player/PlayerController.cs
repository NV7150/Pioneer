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
                animator.SetBool("Running",true);
            }else if(Input.GetKeyUp(KeyCode.W)){
                animator.SetBool("Running", false);
            }
            /* else if (Input.GetKey(KeyCode.A)) {
                container.transform.position += container.transform.right * -1.0f;
            } else if (Input.GetKey(KeyCode.D)) {
                container.transform.position += container.transform.right * 1.0f;
            } else if (Input.GetKey(KeyCode.S)) {
                container.transform.position += container.transform.forward * -1.0f;
            }*/

            if(Input.GetKey(KeyCode.RightArrow)){
                container.transform.Rotate(new Vector3(0,1,0));
            }else if(Input.GetKey(KeyCode.LeftArrow)){
                container.transform.Rotate(new Vector3(0,-1,0));
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
