using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{

	public GameObject pauseMenu;

	public GameObject gameMenu;

	public GameObject deathMenu;
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (Input.GetKeyDown(KeyCode.Escape) && !deathMenu.active)
		{
			if (!pauseMenu.active)
			{
				Time.timeScale = 0;
				pauseMenu.SetActive(true);
				gameMenu.SetActive(false);
			}
				
			else
			{
				Time.timeScale = 1;
				pauseMenu.SetActive(false);
				gameMenu.SetActive(true);
			}
		}
	}

	public void ToGame()
	{
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
		gameMenu.SetActive(true);
	}
}
