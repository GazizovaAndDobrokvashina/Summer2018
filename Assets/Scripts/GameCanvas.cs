using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    public GameObject spellsGameObject;

    public GameObject addSpells;
    private Fox fox;

    private int whichSpellChoosen;

    private void Start()
    {
        fox = GameObject.FindWithTag("Player").GetComponent<Fox>();
    }

    public void ChangeChoosenSpell(int value)
    {
        whichSpellChoosen = value;
        addSpells.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            spellsGameObject.SetActive(!spellsGameObject.active);
        }
    }

    public void SaveNewSpell(int value)
    {
        //value - это первая или вторая атакующая способность, whichSpellChoosen передается фоксу и заменяет активный на новый 
        addSpells.SetActive(false);
        spellsGameObject.SetActive(false);
        Debug.Log("Saved");
    }
}