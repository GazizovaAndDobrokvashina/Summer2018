using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class GameCanvas : MonoBehaviour
{
    //окно со способностями игрока
    public GameObject spellsGameObject;

    //текстовое поле кнопки добавления способности
    public Text addSpell1;

    //текстовое поле кнопки добавления способности
    public Text addSpell2;

    //картинка хилки
    public Image HealImage;

    //объект с кнопками добавления способностей
    public GameObject addSpells;

    //ссылка на игрока
    private Fox fox;

    //какой навык выбран
    private int whichSpellChoosen;

    //массив кнопок спелов
    [SerializeField] private GameObject[] buttons;

    //массив картинок спелов
    [SerializeField] private Sprite[] images;

    //картинка первой способности
    public Image firstSkill;

    //картинка второй способности
    public Image secondSkill;
    
    private void Start()
    {
        //находим игрока на сцене
        fox = GameObject.FindWithTag("Player").GetComponent<Fox>();
    }

    //открыть меню смены активных спеллов
    public void ChangeChoosenSpell(int value, string nameOfSpell)
    {
        //запоминаем, на какой хотим сменить
        whichSpellChoosen = value;
        //выводим на кнопках какую способность на какую позицию назначить
        addSpell1.text = "Установить на первую способность: " + nameOfSpell;
        addSpell2.text = "Установить на вторую способность: " + nameOfSpell;
        //включаем меню смены спеллов
        addSpells.SetActive(true);
    }

    private void FixedUpdate()
    {
        //если нажали колесико мыши, то открываем или закрываем меню способностей
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            MenuSpells();
        }

        if(fox.CurrentCoolDownHeal > 0)
        HealImage.fillAmount =
            (fox.CurrentHealSpell.Cooldown - fox.CurrentCoolDownHeal) / fox.CurrentHealSpell.Cooldown;
        
        
        if(fox.CurrentCoolDownFirstDamage > 0)
            firstSkill.fillAmount =
                (fox.CurrentFirstDamageSpell.Cooldown - fox.CurrentCoolDownFirstDamage) / fox.CurrentFirstDamageSpell.Cooldown;
        
        
        if(fox.CurrentCoolDownSecondDamage > 0)
            secondSkill.fillAmount =
                (fox.CurrentSecondDamageSpell.Cooldown - fox.CurrentCoolDownSecondDamage) / fox.CurrentSecondDamageSpell.Cooldown;
    }

    //открыть/закрыть окно способностей
    private void MenuSpells()
    {
        //открываем или закрываем меню способностей
        spellsGameObject.SetActive(!spellsGameObject.activeInHierarchy);
        //если меню способностей открыто
        if (spellsGameObject.activeInHierarchy)
        {
            //узнаем у игрока, какие у него есть способности
            List<Spell> spells = fox.Spells;
            //создаем индекс для кнопок
            int index = 0;
            //проходим по всем кнопкам
            foreach (Spell spell in spells)
            {
                //если способность наносит урон
                if (spell.Type != "HEAL")
                {
                    //выводим название спелла
                    buttons[index].GetComponentInChildren<Text>().text = spell.NameForGame;
                    //добавляем листенер на клик
                    buttons[index].GetComponent<Button>().onClick
                        .AddListener(() => ChangeChoosenSpell(spell.Id, spell.NameForGame));

                    buttons[index].GetComponent<Image>().sprite = spell.LargeSpell;

                    //включаем кнопку
                    buttons[index].SetActive(true);
                    //увеличиваем счетчик кнопки
                    index++;
                }
            }
        }
        else
        {
            //выключаем меню добавления спеллов
            addSpells.SetActive(false);
        }
    }

    //сохраням новый спелл
    public void SaveNewSpell(int value)
    {
        //value - это первая или вторая атакующая способность, whichSpellChoosen передается фоксу и заменяет активный на новый 
        fox.SetDamageSpell(value, whichSpellChoosen);
        ChangeSpriteOnToolBars(value, AllSpells.GetSpellById(whichSpellChoosen).LargeSpell);
        //выключаем менюшки спеллов
        addSpells.SetActive(false);
        spellsGameObject.SetActive(false);
        // Debug.Log("Saved");
    }

    //изменить спрайт в тулбаре, если изменили способность
    private void ChangeSpriteOnToolBars(int number, Sprite sprite)
    {
        if (number == 1)
        {
            firstSkill.sprite = sprite;
        }
        else
        {
            secondSkill.sprite = sprite;
        }
    }
}