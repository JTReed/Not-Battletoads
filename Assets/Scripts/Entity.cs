using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    public int health;

	public void TakeDamage(int dmg) 
    {
        health -= dmg;
        if (health <= 0) {
            Die();
        }
        Debug.Log(gameObject.name + " has " + health + " health");
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " is dead");
        Destroy(gameObject);
    }
}
