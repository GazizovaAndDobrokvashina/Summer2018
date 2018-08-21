using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class GameCanvas : MonoBehaviour
{
    public GameObject spellsGameObject;

    public GameObject addSpells;
    private Fox fox;

    private int whichSpellChoosen;

    [SerializeField] private GameObject[] buttons;
    
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
            OpenSpells();
        }
    }

    private void OpenSpells()
    {
        spellsGameObject.SetActive(!spellsGameObject.activeInHierarchy);
        List<Spell> spells = fox.Spells;
        int index = 0;
        foreach (Spell spell in spells)
        {
            if (spell.Type != "HEAL")
            {
                buttons[index].GetComponentInChildren<Text>().text = spell.NameOfSpell;
                buttons[index].GetComponent<Button>().onClick
                    .AddListener(() => ChangeChoosenSpell(spell.Id));
                buttons[index].SetActive(true);
                index++;
            }
        }
    }

    public void SaveNewSpell(int value)
    {
        //value - это первая или вторая атакующая способность, whichSpellChoosen передается фоксу и заменяет активный на новый 
        fox.SetDamageSpell(value, whichSpellChoosen);
        addSpells.SetActive(false);
        spellsGameObject.SetActive(false);
       // Debug.Log("Saved");
    }
}