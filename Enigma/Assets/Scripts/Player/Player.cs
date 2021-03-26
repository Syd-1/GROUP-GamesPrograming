using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	public int maxHealth = 90;
	public int currentHealth;
	public bool canHeal = false;
	public bool hasAllKeys = false;
	public int numberOfKeys;

	public HealthBar healthBar;

	// Start is called before the first frame update
	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
		numberOfKeys = GameObject.FindGameObjectsWithTag("Key").Length;
	}

    // On collision with enemy sword take damage

    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Enemy")
		{
			TakeDamage(30);
		}

		//health kit - check if the player collided with a heart
		if (other.gameObject.tag == "Health")
		{
			if(currentHealth < maxHealth)
            {
				canHeal = true;
				if (canHeal == true)
                {
					AddHealth(30);
					Destroy(other.gameObject);
				}
            }
		}

		if (other.gameObject.tag == "Key")
		{
			Destroy(other.gameObject);
			numberOfKeys--;
			if (numberOfKeys == 0)
            {
				hasAllKeys = true;
			}
		}
	}


    void TakeDamage(int damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);
	}

	void AddHealth(int health)
    {
		currentHealth += health;

		healthBar.SetHealth(currentHealth);
    }
}