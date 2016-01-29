using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {
    public float maxHP = 100.0f;
    public float currentHP;
    public GameObject Enemy;

    void Start(){
        currentHP = maxHP;
    }

    void Update(){
        checkStatus();
    }

    public void checkStatus(){
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        if (currentHP < 0)
        {
            currentHP = 0;
        }

        if (currentHP == 0)
        {
            death();
        }
    }

    public void receiveDamage(float damage){
        currentHP -= damage;
        Debug.Log("Damage Applied");
    }

    public void death(){
        Destroy(this.gameObject);
    }

}