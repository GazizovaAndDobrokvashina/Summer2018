using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    //идентификатор
    [SerializeField] protected int ID;

    //максимальное здоровье
    [SerializeField] protected float maxHealth;

    //текущее здоровье
    [SerializeField] protected float health;

    //максимальная мана
    [SerializeField] protected float maxMana;

    //текущая мана
    [SerializeField] protected float mana;

    //скорость передвижения
    [SerializeField] protected float speed;

    //сила прыжка
    [SerializeField] protected float forceForJump;

    //кулдаун хилящей способности
    [SerializeField] protected float currentCoolDownHeal;

    //кулдаун первой атакующей способности
    [SerializeField] protected float currentCoolDownFirstDamage;

    //кулдаун второй атакующей способности
    [SerializeField] protected float currentCoolDownSecondDamage;

    //рига персонажа
    protected Rigidbody _rigidbody;

    //список доступных заклинаний
    protected List<Spell> spells;

    //текущая хилящая способность
    protected Spell currentHealSpell;

    //первая атакующая способность
    protected Spell currentFirstDamageSpell;

    //вторая атакующя способность
    protected Spell currentSecondDamageSpell;

    //использование спелла
    protected abstract void UseSpell(Spell spell, int numberOfAttackSpell);

    //прыжок
    protected abstract void Jump();

    //смерть
    protected abstract void Death();
}