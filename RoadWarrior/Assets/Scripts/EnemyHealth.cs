using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] private int health = 50;
    [SerializeField] private int impactDamageDealt = 50;
	
	// Update is called once per frame
	void Update () 
    {
		if (health <= 0)
        {
            Destroy(gameObject);
        }
	}

    public void ApplyDamage(int damage)
    {
        health -= damage;
        print(gameObject.name + " took damage: " + damage);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
            collision.gameObject.GetComponent<PlayerController>().ApplyDamage(impactDamageDealt);
            health = 0;
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().ApplyDamage(impactDamageDealt);
        }
    }
}
