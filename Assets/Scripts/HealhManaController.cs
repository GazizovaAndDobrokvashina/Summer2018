using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealhManaController : MonoBehaviour
{
    public HealtAndManaBar health;
    public HealtAndManaBar mana;
    public Text countExtraLives;
    private int extraLives;
    private Fox fox;

    void Start()
    {
        fox = GameObject.FindGameObjectWithTag("Player").GetComponent<Fox>();
    }

    private void FixedUpdate()
    {
        if (fox.Health != health.current)
            health.current = fox.Health;


        if (fox.Mana != mana.current)
            mana.current = fox.Mana;

        if (extraLives == fox.ExtraLives) return;
        countExtraLives.text = "x " + fox.ExtraLives;
        extraLives = fox.ExtraLives;
    }
}