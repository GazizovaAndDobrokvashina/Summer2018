using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //идентификатор чекпоинта
    [SerializeField] private int ID;

    //если сталкивается с каким-то триггером, то проверяет игрок ли это и какой чекпоинт был до этого, есливсё подходит, то сохраняет чекпоинт
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && ID > Level.GetIndexOfLastCheckPoint() && !other.isTrigger)
        {
            Level.ChangeLastCheckPoint(ID);
            other.gameObject.GetComponent<Fox>().SaveLastCheckPoint(transform.position);
            gameObject.SetActive(false);
        }
    }

    //геттер айдишника
    public int Id
    {
        get { return ID; }
    }
}