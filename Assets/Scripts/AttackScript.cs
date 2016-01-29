using UnityEngine;
using System.Collections;

public class AttackScript : MonoBehaviour {
    public float damage = 50.0f;
    public float attackDuration = 0.3f;
    [HideInInspector]
    public bool attacking = false;

    //USE THIS SCRIPT ON A CREATED GAME OBJECT (I.E. SWORD)
    void Start() { 
    }


    void Update(){
        if(Input.GetKeyDown("j")){
            attacking = true;
        }
    }

    //TO USE THIS ENEMY CLASS MUST ALL HAVE RIGID BODIES ATTACHED
    //MAKE SURE ENEMIES ARE TAGGED PROPERLY TOO
    void OnTriggerEnter(Collider col) {
        if (col.tag == "Enemy"){
            if (attacking){
                col.SendMessage("receiveDamage", damage, SendMessageOptions.DontRequireReceiver); //test of damage
            }
        }
    }

    void EnableDamage(){
        if (attacking == true) return;
        attacking = true;
        StartCoroutine("DisableDamage");
    }

    IEnumerator DisableDamage(){
        yield return new WaitForSeconds(attackDuration);
        attacking = false;
    }

}