﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameInformation
{
    //получить название уровня, на котором игрок был в последний раз
    public static string GetNameOfLevel()
    {
        return PlayerPrefs.GetString("NameOfLevel");
    }

    //сохранить игру
    public static void SaveGame(string NameOfLevel, int IdCheckPoint, float HealOfPlayer, float ManaofPlayer,
        int ExtralivesOfPlayer, string NameOfFirstSpell, string NameOfSecondSpell, int countOfEnemies,
        int countOfDeaths, int countOfBonuses, int IDOfLastSpell)
    {
        //название уровня
        PlayerPrefs.SetString("NameOfLevel", NameOfLevel);

        //номер чекпоинта
        PlayerPrefs.SetInt("IdCheckPoint", IdCheckPoint);

        //количество дополнительных жизней игрока
        PlayerPrefs.SetInt("ExtralivesOfPlayer", ExtralivesOfPlayer);

        //текущий уровень здоровья игрока
        PlayerPrefs.SetFloat("HealOfPlayer", HealOfPlayer);

        //текущий уровень маны игрока
        PlayerPrefs.SetFloat("ManaofPlayer", ManaofPlayer);

        //название первого активного спелла игрока
        PlayerPrefs.SetString("NameOfFirstSpell", NameOfFirstSpell);

        //название второго активного спелла игрока
        PlayerPrefs.SetString("NameOfSecondSpell", NameOfSecondSpell);

        //количество убитых игроком врагов на уровне
        PlayerPrefs.SetInt("countOfEnemies", countOfEnemies);

        //количество смертей игрока на уровне
        PlayerPrefs.SetInt("countOfDeaths", countOfDeaths);

        //количество собранных игроком бонусов на уровне
        PlayerPrefs.SetInt("countOfBonuses", countOfBonuses);

        //ID полседнего скилла, который игрок подобрал
        PlayerPrefs.SetInt("IDOfLastSpell", IDOfLastSpell);
    }
}