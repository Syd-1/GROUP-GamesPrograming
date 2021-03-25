using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	public int maxHealth = 90;
	public int currentHealth;
	public bool heal = false;

	public HealthBar healthBar;

	// Start is called before the first frame update
	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}

    // On collision with enemy sword take damage
    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Enemy")
		{
			TakeDamage(30);
		}

		if (other.gameObject.tag == "Health")
		{
			AddHealth(30);
			heal = true;
            if (heal == true)
            {
				Destroy(GameObject.FindWithTag("Health"));
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