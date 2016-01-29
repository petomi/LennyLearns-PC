using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
    bool attacking = false;
    public GameObject target;
    public float attackTimer;
    public float atkCoolDown;
	float idleTimer = 4.0f;


    void Start(){
		attackTimer = 0f;
		atkCoolDown = 2.0f;
	}

    void Update()
    {
        //animation selection using public bools

        //idle animation check
        if (Input.anyKey == false)
        {
            if (Time.time >= idleTimer)
            {
                GetComponent<Animation>().Player("Idle");
            }
            idleTimer = idleTime + Time.time;
        }
        if (attacking)
        {
            GetComponent<Animation>().Stop();
            GetComponent<Animation>().Player("Attacking");
        }
        if (jumping)
        {
            GetComponent<Animation>().Stop();
            GetComponent<Animation>().Player("Jumping");
        }
        if (wallSliding)
        {
            GetComponent<Animation>().Stop();
            GetComponent<Animation>().Player("Wallsliding");
        }
    }
}