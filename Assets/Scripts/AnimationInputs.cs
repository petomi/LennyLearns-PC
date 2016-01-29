using UnityEngine;
using System.Collections;

public class AnimationInputs : MonoBehaviour {
    public Animator anim;
    private float previousTime;
    bool plsFlip = false;
    bool alreadyFlipped = false;
    private bool isIdleReady = false;
    private bool isWalkingRight = false;
    private bool isWalkingLeft = false;

    //set idle timer
    public const float IDLE_TIME = 7.0f;

	// Update is called once per frame
	void Update () {
        
        
        //walking animations based on key inputs (one for each direction)
        //split into directions because otherwise stopping walking in one direction would stop the animation for good

        if (isWalkingRight) {
            anim.SetBool("isWalking", true);
            //reset idletimer
            anim.SetBool("isIdle", false);
            isIdleReady = false;
            previousTime = Time.timeSinceLevelLoad;
        }

        if (isWalkingLeft) {
            anim.SetBool("isWalking", true);
            //reset idletimer
            anim.SetBool("isIdle", false);
            isIdleReady = false;
            previousTime = Time.timeSinceLevelLoad;
        }
        
        
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            isWalkingLeft = true;
            isWalkingRight = false;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            isWalkingRight = true;
            isWalkingLeft = false;
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) {
            isWalkingLeft = false;
            anim.SetBool("isWalking", false);
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) {
            isWalkingRight = false;
            anim.SetBool("isWalking", false);
        }

        
        //flipping orientation of animation
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            plsFlip = true;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (alreadyFlipped == true) {
                Flip();
                alreadyFlipped = false;
                plsFlip = false;
            }
        }

        if (plsFlip == true && alreadyFlipped == false) {
            Flip();
            plsFlip = false;
            alreadyFlipped = true;
        }

        //Idle animation manager
        GameObject player = GameObject.Find("Player");
        Player playerScript = player.GetComponent<Player>();

        if (!isIdleReady && playerScript.isGrounded && !playerScript.isMoving) 
        {
            isIdleReady = true;
            previousTime = Time.timeSinceLevelLoad;
        }

        else if (isIdleReady && Time.timeSinceLevelLoad - previousTime > IDLE_TIME){
            anim.SetBool("isIdle", true);
            previousTime = Time.timeSinceLevelLoad;
        }


    }
                

    void Flip() { 
    
        //Multiply player x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }
    
  
	}
