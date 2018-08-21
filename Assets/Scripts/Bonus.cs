using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private String type;
    [SerializeField] private float value;
    [SerializeField] private float timer;

    private void UseBonus(Fox fox)
    {
        switch (type)
        {
            case "EXTRALIVE":
                fox.AddExtraLive();
                break;
            case "SPEED":
                fox.AddBonusSpeed(value, timer);
                break;
            case "JUMP":
                fox.AddBonusJump(value, timer);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger && other.gameObject.tag == "Player")
        {
            UseBonus(other.gameObject.GetComponent<Fox>());
            Level.CountNewBonus();
            gameObject.SetActive(false);
        }
    }
}