using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //сохранена ли игра
    private bool gameSaved;

    //событие сохранения игры
    [SerializeField] private UnityEvent _SaveGameEvent;

    //выйти в главное меню
    public void ToMainMenu()
    {
        //игра ещё не сохранена
        gameSaved = false;
        
        //выставляем нормальное течение времени
        Time.timeScale = 1;
        
        //вызываем у уровня метод сохранения уровня
        _SaveGameEvent.Invoke();
        
        //ждем, пока игра сохранится, и выходим в главное меню
        StartCoroutine(WaitSaves());
    }

    //метод для скрипта уровня, чтобы отметь, что игра сохранена
    public void GameSaved()
    {
        gameSaved = true;
    }

    //корутина ожидания сохранения и по его завершению выход в главное меню
    private IEnumerator WaitSaves()
    {
        yield return new WaitUntil(() => gameSaved);
        SceneManager.LoadScene("MainMenu");
    }
}