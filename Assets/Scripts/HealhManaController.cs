using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealhManaController : MonoBehaviour
{
    public HealtAndManaBar health;
    public HealtAndManaBar mana;
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
    }
}