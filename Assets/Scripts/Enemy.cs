using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Player
{
    private void Start()
    {
        //здоровье
        maxHealth = 100f;
        health = 100f;
        //мана
        mana = 100f;
        maxMana = 100f;
        //скорость передвижения
        speed = 15f;
        //сила прыжка
        forceForJump = 6f;
        //rigidbody игрока
        _rigidbody = GetComponent<Rigidbody>();

        //ТЕСТОВЫЕ ДАННЫЕ
        spells = new List<Spell>();
//        spells[0] = new Spell("HEAL", 1f, 20f, 10f);
//        spells[1] = new Spell("ALLDAMAGE", 4f, 20f, 20f);
//        spells[2] = new Spell("SINGLEDAMAGE", 3f, 15f, 15f);

//        currentHealSpell = spells[0];
//        currentFirstDamageSpell = spells[1];
//        currentSecondDamageSpell = spells[2];
    }

    private void FixedUpdate()
    {
        if (mana < maxMana)
            mana += Time.deltaTime;
        if (mana > maxMana)
            mana = maxMana;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }

    protected override void UseSpell(Spell spell, int numberOfAttackSpell)
    {
        throw new System.NotImplementedException();
    }

    protected override void Move()
    {
        throw new System.NotImplementedException();
    }

    protected override void Jump()
    {
        throw new System.NotImplementedException();
    }

    protected override void Death()
    {
        gameObject.SetActive(false);
        Level.CountNewEnemy();
    }

    public void RestartEnemy()
    {
        health = maxHealth;
        mana = maxMana;
        
        //дописать рестарт кулдаунов способностей, когда пропишем способности для ботов
    }

    public float Health
    {
        get { return health; }
    }
}