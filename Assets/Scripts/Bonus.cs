using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bonus : MonoBehaviour
{
    //тип бонуса
    [SerializeField] private String type;

    //значение бонуса
    [SerializeField] private float value;

    //время действия эффекта
    [SerializeField] private float timer;

    //событие подъема бонуса
    [SerializeField] private UnityEvent _bonusEvent;

    //событие завершения уровня
    [SerializeField] private UnityEvent _endOfLevel;
    
    //событие подъема бонуса жизни
    [SerializeField] private UnityEvent _bonusLiveEvent;
    
    //событие подъема бонуса скорости
    [SerializeField] private UnityEvent _bonusSpeedEvent;
    
    //событие подъема бонуса прыжка
    [SerializeField] private UnityEvent _bonusJumpEvent;

    private void UseBonus(Fox fox)
    {
        switch (type)
        {
            case "EXTRALIVE":
                fox.AddExtraLive();
                _bonusLiveEvent.Invoke();
                break;
            case "SPEED":
                fox.AddBonusSpeed(value, timer);
                _bonusSpeedEvent.Invoke();
                break;
            case "JUMP":
                fox.AddBonusJump(value, timer);
                _bonusJumpEvent.Invoke();
                break;
            case "CLAW":
                _endOfLevel.Invoke();
                break;
        }

        if (type != "CLAW")
            _bonusEvent.Invoke();
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