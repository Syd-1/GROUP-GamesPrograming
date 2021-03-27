using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public GameObject moreKeysMessage;
    public int numberOfKeys;
    //public bool byDoor;

    // Start is called before the first frame update
    void Start()
    {
        moreKeysMessage.SetActive(false);
		//byDoor = false;
    }

	void Update()
	{
		numberOfKeys = GameObject.FindGameObjectsWithTag("Key").Length;
	}

	void OnTriggerEnter(Collider other)
	{
		//Interaction With Door 
		if (other.gameObject.tag == "Player")
		{
			if (numberOfKeys == 0)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
			}
			else
			{
				moreKeysMessage.SetActive(true);
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		moreKeysMessage.SetActive(false);
	}

}
