using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class EndOfLevelCanvas : MonoBehaviour
{
    public Text text;


    public void OpenThisMenu()
    {
        Time.timeScale = 0;
        text.text = "Врагов убито: " + Level.CountOfEnemies + "\nБонусов собрано: " + Level.CountOfBonuses +
                    "\nСмертей игрока: " + Level.CountOfDeaths;
        gameObject.SetActive(true);
    }
}