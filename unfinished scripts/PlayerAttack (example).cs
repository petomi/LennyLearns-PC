using UnityEngine;
using System.Collections;

/// <summary>
/// //////THIS WHOLE FILE IS AN EXAMPLE/PLACEHOLDER!! MUST BE CONVERTED BEFORE USE
/// </summary>


public class PlayerAttack : MonoBehaviour {
    
    bool attacking = false;
    public GameObject target;
    public float attackTimer;
    public float atkCoolDown;    

    void Start() {
        attackTimer = 0;
        atkCoolDown = 2.0f;
    }

    void Update(){
        if(attackTimer > 0){
            attackTimer -= Time.deltaTime;
        }
        else
        {
            attackTimer = 0;
            if (Input.GetKeyDown(KeyCode.J) && !GetComponent<Animation>().isPlaying)
            {
                Attack();
                if (attackTimer == 0)
                {
                    Attack();
                    attackTimer = atkCoolDown;
                }
            }
        }
    }

    private void Attack(){
        GetComponent<Animation>().Play("Attack");
        float distance = Vector3.Distance(target.transform.position, transform.position);
        Vector3 dir = (target.transform.position - trasnform.position).normalized;
        float direction = Vector3.Dot(dir, transform.forward);
        Debug.Log(direction);
        if(distance < 2.5f){
            if (direction >0){
                EnemyHealth eh = (EnemyHealth)target.GetComponent("EnemyHealth");
                eh.AddJustCurrentHealth(-5);
            }
        }
	}






    
}