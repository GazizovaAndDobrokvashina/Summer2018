using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    //Количество врагов на уровне
    private int countOfEnemies;
    //количество смертей игрока
    private int countOfDeaths;
    //количество бонусов на уровне
    private int countOfBonuses;
    //массив чекпоинтов на уровне
    private Checkpoint[] checkpoints;
    //какой чекпоинт был посещен последним
    private static int indexOfLastCheckPoint;
    //подготовка данных при старте уровня
    private void Start()
    {
        //сбрасываем посещенные чекпоинты
        indexOfLastCheckPoint = -1;
        
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
    }

    public static void ChangeLastCheckPoint(int index)
    {
        indexOfLastCheckPoint = index;
        Debug.Log("Player Saved at checkpoint " + indexOfLastCheckPoint);
    }

    public static int GetIndexOfLastCheckPoint()
    {
        return indexOfLastCheckPoint;
    }

    void ShowInformation()
    {
    }
}