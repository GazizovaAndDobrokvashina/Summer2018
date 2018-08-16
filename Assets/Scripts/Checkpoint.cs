using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	//идентификатор чекпоинта
	private int ID;
	//был ли уже здесь игрок
	private bool playerWasAlreadyThere = false;
	//последняя ли это точка сохранения
	private bool thisPlaceIsTheLastWherePlayerWas = false;

	//сохранить эту точку как последнюю, которую посетил игрок
	void SaveplayerHere()
	{
		playerWasAlreadyThere = true;
		thisPlaceIsTheLastWherePlayerWas = true;
	}

	//если сталкивается с каким-то триггером, то проверяет игрок ли это и не был ли он уже тут
	//если не бл, то сохраняет чекпоинт
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" && !playerWasAlreadyThere)
		{
			SaveplayerHere();
		}
	}

	//если игрок достиг следующего чекпоинта, то этот перестает быть последним
	public void PlayerOnNextCheckPoint()
	{
		thisPlaceIsTheLastWherePlayerWas = false;
	}
}
