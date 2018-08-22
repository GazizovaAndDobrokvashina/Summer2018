using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Level : MonoBehaviour
{
    //Количество врагов на уровне
    private static int countOfEnemies;

    //количество смертей игрока
    private static int countOfDeaths;

    //количество бонусов на уровне
    private static int countOfBonuses;

    //массив чекпоинтов на уровне
    private Checkpoint[] checkpoints;

    //массив врагов на уровне
    private GameObject[] enemyes;

    private GameObject[] bonuses;

    //какой чекпоинт был посещен последним
    private static int indexOfLastCheckPoint;

    public SpriteAtlas atlas;
    
    //ссылка на игрока
    private Fox fox;
    
    //подготовка данных при старте уровня
    private void Start()
    {
        AllSpells.GenerateSpells(atlas);
        
        //находим игрока на уровне
        fox = GameObject.FindGameObjectWithTag("Player").GetComponent<Fox>();
        
        //сбрасываем параметры уровня к стандартным
        ResetIndexOfLastCheckPointAndCounts();

        //находим все чекпоинты на уровне
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Checkpoint");

        //объявляем массив чекпоинтов размером равным количеству найденных чекпоинтов
        checkpoints = new Checkpoint[objects.Length];

        //записываем все скрипты чекпоинтов в массив на места равные их ID
        for (var index = 0; index < objects.Length; index++)
        {
            Checkpoint checkpoint = objects[index].GetComponent<Checkpoint>();
            checkpoints[checkpoint.Id] = checkpoint;
        }

        //находим всех врагов на уровне
        enemyes = GameObject.FindGameObjectsWithTag("Enemy");
        
        //находим все бонусы на уровне
        bonuses = GameObject.FindGameObjectsWithTag("Bonus");
    }

    //запись ID посещенного игроком чекпоинта
    public static void ChangeLastCheckPoint(int index)
    {
        indexOfLastCheckPoint = index;
        Debug.Log("Player Saved at checkpoint " + indexOfLastCheckPoint);
    }

    //получить ID последднего посещенного игроком чекпоинта
    public static int GetIndexOfLastCheckPoint()
    {
        return indexOfLastCheckPoint;
    }

    //добавить убитого врага в счетчик
    public static void CountNewEnemy()
    {
        countOfEnemies++;
    }

    //добавить найденный предмет в счетчик
    public static void CountNewBonus()
    {
        countOfBonuses++;
    }
    
    //отобразить информацию об уровне
    void ShowInformation()
    {
    }

    //начать уровнеь заново
    public void RestartLevel()
    {
        //Сбрасываем параметры лисы
        fox.RestartFox(checkpoints[0].transform.position);
        
        //включаем всех врагов на уровне, сбрасываем их параметры к дефолтным
        foreach (GameObject o in enemyes)
        {
            o.SetActive(true);
            o.GetComponent<Enemy>().RestartEnemy();
        }
        
        //включаем все бонусы на уровне
        foreach (GameObject o in bonuses)
        {
            o.SetActive(true);
        }

        //сбрасываем посещенные чекпоинты
        ResetIndexOfLastCheckPointAndCounts();
    }

    //сброс посещенных чекпоинтов
    void ResetIndexOfLastCheckPointAndCounts()
    {
        indexOfLastCheckPoint = -1;
        countOfEnemies = 0;
        countOfDeaths = 0;
        countOfDeaths = 0;
    }
}