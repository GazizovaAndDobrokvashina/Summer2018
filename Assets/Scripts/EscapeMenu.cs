using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{

	public GameObject pauseMenu;

	public GameObject gameMenu;
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown(KeyCode.Escape))
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
