using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public EscapeMenu _escapeMenu;
	
	public void ToMainMenu()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("MainMenu");
	}

	public void ToGame()
	{
		_escapeMenu.ToGame();
	}
}
