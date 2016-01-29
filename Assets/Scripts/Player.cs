using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	//audio stuff, check inspector
	public AudioSource[] AudioClips = null;
	//check very bottom for walking noise

	//jump + basic movement variables
    public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;
    public float idleTime = 10f;
	
    //walljump variables
//	public Vector2 wallJumpClimb; //recommend 7.5, 16
//	public Vector2 wallJumpOff; // recommend 8.5, 7
//	public Vector2 wallLeap; // recommend 18, 17
//	public float wallSlideSpeedMax = 3;
//	public float wallStickTime = .25f;
//	float timeToWallUnstick;
	
	//double jump + dash variables
//    public float doubleJumpHeightPercent = 1;
//	float doubleJumpVelocity;
//	public int airDashLimit = 5;
//	public int groundDashLimit = 5;
//	public float dashSpeed = 30;
//	public float airDashSpeed = 50;
//	public float staminaRegenTime = 2f;
//    float nextDash = 0f;
//    float nextAirDash = 0f;
//	int consecutiveDash = 3;
//	int consecutiveAirDash = 3;
	bool isJumping;
//	bool doubleJumpUsed = false;
    public bool isGrounded = false;

	//basic jump velocity and gravity variables
    float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;

    //shared hooks for animation
    public bool isMoving = false;
	
    //creates object which can be used for hit detection.
	Controller2D controller;
	
	void Start() {
      

        controller = GetComponent<Controller2D> ();
		
        //JUMP VELOCITY EQUATIONS
        // d = v*t + at^2/2 where a = g at highest point in arc, and v = 0, negative since down
		gravity = -2 * maxJumpHeight/Mathf.Pow(timeToJumpApex, 2);
		// vFinal = vInit + a*t where a = g and vInit = 0 at start of jump, .4 =t at apex
		maxJumpVelocity = Mathf.Abs (gravity) * timeToJumpApex;
		//debug: //print("Gravity: " + gravity + " Jump Velocity: " + maxJumpVelocity);
		
		//Vf^2 = Vi^2 + 2ad -> minJumpForce = sqrt (2*gravity*minJumpHeight)
		//solve for minJumpHeight to enable variable height jump
		minJumpVelocity = Mathf.Sqrt (2*Mathf.Abs (gravity)*minJumpHeight);
		
//        doubleJumpVelocity = maxJumpVelocity * doubleJumpHeightPercent;

	}
	
	void Update(){


        //variable hooks into idle animation and other animations
        if (controller.collisions.below) {
            isGrounded = true;    
        }
        
        //reads which way the controller stick is moving
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		//sets var for side wall is on when colliding horizontally
        int wallDirX = (controller.collisions.left)? -1: 1; //-1 for wall to left, 1 to right
		
		//basic horizontal movement implementation
        float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		
//        //wallsliding implementation
//		bool isAirDashing = false;
//		bool wallSliding = false;
//		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0){
//			wallSliding = true;
//			
//			if(velocity.y < -wallSlideSpeedMax){
//				velocity.y = -wallSlideSpeedMax;
//			}
//			if (timeToWallUnstick > 0){
//				velocityXSmoothing = 0;
//				velocity.x = 0;
//				if (input.x != wallDirX && input.x != 0) {
//					timeToWallUnstick -= Time.deltaTime;
//				}
//				else {
//					timeToWallUnstick = wallStickTime;
//				}
//			}
//			else {
//				timeToWallUnstick = wallStickTime;
//			}
//		}
//		
//		//DOUBLE JUMPING IMPLEMENTATION
//		if (!controller.collisions.below && !controller.collisions.left && !controller.collisions.right){
//			isJumping = true;
//		}
//		else{
//			isJumping = false;
//		}
//		if (controller.collisions.below || controller.collisions.left || controller.collisions.right){
//				doubleJumpUsed = false;
//		}
//		//
		
		
		//walljump + jump + double jump implementation
        if (Input.GetKeyDown(KeyCode.Space)) {
//			if (wallSliding){
//				if (wallDirX == input.x){
//					velocity.x = -wallDirX * wallJumpClimb.x;
//					velocity.y = wallJumpClimb.y;
//					AudioClips[1].Play ();
//
//				}
//				else if (input.x == 0){
//					velocity.x = -wallDirX * wallJumpOff.x;
//					velocity.y = wallJumpOff.y;
//					AudioClips[1].Play ();
//				}
//				else {
//					velocity.x = -wallDirX * wallLeap.x;
//					velocity.y = wallLeap.y;
//					AudioClips[1].Play ();
//				}
//			}
			if (controller.collisions.below){
				velocity.y = maxJumpVelocity;
				AudioClips[1].Play ();
			}
//			//actual double jump movement definition
//			if (isJumping && !doubleJumpUsed){
//				velocity.y = doubleJumpVelocity;
//				doubleJumpUsed = true;
//				AudioClips[1].Play ();
//			}
		}
		if (Input.GetKeyUp (KeyCode.Space)){
			if(velocity.y > minJumpVelocity){
				velocity.y = minJumpVelocity;
				AudioClips[1].Stop ();
			}
		}


		//***dash stamina implementation  ***TEST THIS***
//		if (Time.time >= nextAirDash){
//            nextAirDash = Time.time + staminaRegenTime;
//            consecutiveAirDash--;
//            if (consecutiveAirDash == 0) {
//                nextAirDash = 0;
//            }    
//		}
//        if (Time.time >= nextDash){
//            nextDash = Time.time + staminaRegenTime;
//            consecutiveDash--;
//            if (consecutiveDash == 0){
//                nextDash = 0;
//            }
//        }
		//

		//air dash + ground dash implementation ***TEST STAMINA/COOLDOWN***
//		if(Input.GetKeyDown(KeyCode.LeftShift)||Input.GetKeyDown(KeyCode.RightShift)){
//			if(!controller.collisions.below){
//				if (consecutiveAirDash < airDashLimit){
//					isAirDashing = true;
//					if (input.x == 1){
//						velocity.x = airDashSpeed;
//					}
//					else if (input.x == -1){
//						velocity.x = -airDashSpeed;
//					}
//					else{
//						velocity.y = -airDashSpeed;
//					}
//					consecutiveAirDash++;
//                    if (consecutiveAirDash == airDashLimit){
//                        nextAirDash = Time.time + staminaRegenTime;
//                    }
//				}
//			}
//			if (controller.collisions.below){
//				if(consecutiveDash < groundDashLimit){
//					if (input.x == 1){
//						velocity.x = dashSpeed;
//					}
//					if (input.x == -1){
//						velocity.x = -dashSpeed;
//					}
//					else{
//						velocity.x = dashSpeed;
//					}
//					consecutiveDash++;
//                    if (consecutiveDash == groundDashLimit){
//                        nextDash = Time.time + staminaRegenTime;
//                    }
//				}
//			}
//	}
	 
		//falling implementation
	    velocity.y += gravity * Time.deltaTime;
	    controller.Move (velocity * Time.deltaTime, input);
	    //keeps from vibrating if stuck
	    if (controller.collisions.above || controller.collisions.below){
		    velocity.y = 0;
		}

        //hooks for animation script
        if (Input.anyKey) {
            isMoving = true;
        }
        if (!Input.anyKey) {
            isMoving = false;
       }

	//	if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey (KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && !isJumping) {
	//		AudioClips[0].Play (); //walking noise
	//	}
	//	if (!isMoving){
	//		AudioClips[0].Stop ();
	//	}

        
    }



}