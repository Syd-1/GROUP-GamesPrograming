using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

	public int maxHealth = 90;
	public int currentHealth;
	public bool canHeal = false;

	public HealthBar healthBar;
	public GameObject deathPanel;
	private bool pauseGame = false;

	// Start is called before the first frame update
	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
		deathPanel.SetActive(false);
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
	}

	void TakeDamage(int damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);

		if (currentHealth <= 0f)
		{
			Die();
		}
	}

	void AddHealth(int health)
    {
		currentHealth += health;

		healthBar.SetHealth(currentHealth);
    }

	public void Die()
	{
		deathPanel.SetActive(true);
		ToggleTime();
	}

	public void Restart()
	{
		ToggleTime();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	//https://www.youtube.com/watch?v=f6BvWzAEews
	private void ToggleTime()
	{
		pauseGame = !pauseGame;

		if (pauseGame)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}

}