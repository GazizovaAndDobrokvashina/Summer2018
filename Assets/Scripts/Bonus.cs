using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {

    private String type;
    private float value;

    private void UseBonus(Fox fox)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UseBonus(other.gameObject.GetComponent<Fox>());
        }
    }
}
