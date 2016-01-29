using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {
	
	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;
	
	public Vector2 wallJumpClimb; //recommend 7.5, 16
	public Vector2 wallJumpOff; // recommend 8.5, 7
	public Vector2 wallLeap; // recommend 18, 17
	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;
	
	public float doubleJumpHeightPercent = 1;
	float doubleJumpVelocity;
	public int airDashLimit = 5;
	public int groundDashLimit = 5;
	public float dashSpeed = 30;
	public float airDashSpeed = 50;
	public float staminaRegenTime = 2f;
	float regenTime = 0f;
	int consecutiveDash = 0;
	int consecutiveAirDash = 0;
	bool isJumping;
	bool doubleJumpUsed = false;
	
	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;
	
	Controller2D controller;
	
	void Start() {
		controller = GetComponent<Controller2D> ();
		// d = v*t + at^2/2 where a = g at highest point in arc, and v = 0, negative since down
		gravity = -2 * maxJumpHeight/Mathf.Pow(timeToJumpApex, 2);
		// vFinal = vInit + a*t where a = g and vInit = 0 at start of jump, .4 =t at apex
		maxJumpVelocity = Mathf.Abs (gravity) * timeToJumpApex;
		//debug: print("Gravity: " + gravity + " Jump Velocity: " + maxJumpVelocity);
		
		//Vf^2 = Vi^2 + 2ad
		//minJumpForce = sqrt (2*gravity*minJumpHeight)
		//solve for minJumpHeight to enable variable height jump
		minJumpVelocity = Mathf.Sqrt (2*Mathf.Abs (gravity)*minJumpHeight);
		
		doubleJumpVelocity = maxJumpVelocity * doubleJumpHeightPercent;

	}
	
	void Update(){
		
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		int wallDirX = (controller.collisions.left)? -1: 1; //-1 for wall to left, 1 to right
		
		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		
		bool isAirDashing = false;
		bool wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0){
			wallSliding = true;
			
			if(velocity.y < -wallSlideSpeedMax){
				velocity.y = -wallSlideSpeedMax;
			}
			
			if (timeToWallUnstick > 0){
				velocityXSmoothing = 0;
				velocity.x = 0;
				if (input.x != wallDirX && input.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				}
				else {
					timeToWallUnstick = wallStickTime;
				}
			}
			else {
				timeToWallUnstick = wallStickTime;
			}
		}
		
		//DOUBLE JUMPING IMPLEMENTATION
		if (!controller.collisions.below && !controller.collisions.left && !controller.collisions.right){
			isJumping = true;
		}
		else{
			isJumping = false;
		}
		if (controller.collisions.below || controller.collisions.left || controller.collisions.right){
				doubleJumpUsed = false;
		}
		//
		
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (wallSliding){
				if (wallDirX == input.x){
					velocity.x = -wallDirX * wallJumpClimb.x;
					velocity.y = wallJumpClimb.y;
				}
				else if (input.x == 0){
					velocity.x = -wallDirX * wallJumpOff.x;
					velocity.y = wallJumpOff.y;
				}
				else {
					velocity.x = -wallDirX * wallLeap.x;
					velocity.y = wallLeap.y;
				}
			}
			if (controller.collisions.below){
				velocity.y = maxJumpVelocity;
			}
			//actual double jump movement definition
			if (isJumping && !doubleJumpUsed){
				velocity.y = doubleJumpVelocity;
				doubleJumpUsed = true;
			}
		}
		
		if (Input.GetKeyUp (KeyCode.Space)){
			if(velocity.y > minJumpVelocity){
				velocity.y = minJumpVelocity;
			}
		}

		//dash stamina implementation  ***SEEMS TO CAUSE INFINITE DASHES AFTER ONE COOLDOWN CYCLE****
		if (consecutiveAirDash == airDashLimit){
			if (Time.time > regenTime + staminaRegenTime){
				consecutiveAirDash -= consecutiveAirDash;
			}
		}
		if (consecutiveDash == groundDashLimit){
			if (Time.time > regenTime + staminaRegenTime){
				consecutiveDash -= consecutiveDash;
			}
		}
		//

		//air dash + ground dash implementation
		if(Input.GetKeyDown(KeyCode.LeftShift)||Input.GetKeyDown(KeyCode.RightShift)){
			if(!controller.collisions.below){
				if (consecutiveAirDash < airDashLimit){
					isAirDashing = true;
					if (input.x == 1){
						velocity.x = airDashSpeed;
					}
					else if (input.x == -1){
						velocity.x = -airDashSpeed;
					}
					else{
						velocity.y = -airDashSpeed;
					}
					consecutiveAirDash++;
				}
			}
			if (controller.collisions.below){
				if(consecutiveDash < groundDashLimit){
					if (input.x == 1){
						velocity.x = dashSpeed;
					}
					if (input.x == -1){
						velocity.x = -dashSpeed;
					}
					else{
						velocity.x = dashSpeed;
					}
					consecutiveDash++;
				}
			}
	}
		
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime, input);
		
		if (controller.collisions.above || controller.collisions.below){
			velocity.y = 0;
		}
	}
	
}