using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{

    bool attacking = false;
    public GameObject target;
    public float attackTimer;
    public float atkCoolDown;

    void Start()
    {
        attackTimer = 0;
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
                animation.Player("Idle");
            }
            idleTimer = idleTime + Time.time;
        }
        if (attacking)
        {
            animation.Stop();
            animation.Player("Attacking");
        }
        if (jumping)
        {
            animation.Stop();
            animation.Player("Jumping");
        }
        if (wallSliding)
        {
            animation.Stop();
            animation.Player("Wallsliding");
        }
    }
}