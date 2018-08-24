using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //старт новой игры
    public void StartNewGame()
    {
        //сохраняем дефолтные значения
        GameInformation.SaveGame("TimeLevel", 0, 100f, 100f, 2, "RainOfFire","FireArrow", 0, 0, 0, 2);
        
        //включаем уровень
        Continue();
    }

    //загрузка сохраненного уровня
    public void Continue()
    {
        //загружаем сцену по названию
        SceneManager.LoadScene(GameInformation.GetNameOfLevel());
    }

    //выход из игры
    public void ExitGame()
    {
        Application.Quit();
    }
}