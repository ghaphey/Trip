using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] private int health = 50;
    [SerializeField] private int impactDamageDealt = 50;
    [SerializeField] private GameObject destroyedPrefab;

    private bool notDead = true;

    // Update is called once per frame
    void Update () 
    {
		if ((health <= 0 || Vector3.Dot(transform.up, Vector3.down ) > 0) && notDead)
        {
            GameObject destroyed = Instantiate(destroyedPrefab, transform.parent);
            destroyed.transform.position = transform.position;
            destroyed.transform.localScale = transform.localScale;
            Destroy(gameObject);
            notDead = false;
        }
	}

    public void ApplyDamage(int damage)
    {
        health -= damage;
        //print(gameObject.name + " took damage: " + damage);
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
            if (gameObject.tag != "Enemy")
                collision.gameObject.GetComponent<EnemyHealth>().ApplyDamage(impactDamageDealt);
        }
        else if (collision.gameObject.tag == "Obstacle")
        {
            collision.gameObject.GetComponent<EnemyHealth>().ApplyDamage(impactDamageDealt);
        }
    }

}
