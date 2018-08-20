using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
    [SerializeField] protected int ID;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float health;
    [SerializeField] protected float maxMana;
    [SerializeField] protected float mana;
    [SerializeField] protected float speed;
    [SerializeField] protected float forceForJump;
    [SerializeField] protected float currentCoolDownHeal;
    [SerializeField] protected float currentCoolDownFirstDamage;
    [SerializeField] protected float currentCoolDownSecondDamage;
    
    
    protected Rigidbody _rigidbody;
    protected Spell[] spells;
    protected Spell currentHealSpell;
    protected Spell currentFirstDamageSpell;
    protected Spell currentSecondDamageSpell;

    protected abstract void UseSpell(Spell spell, int numberOfAttackSpell);
    protected abstract void Move();
    protected abstract void Jump();
    protected abstract void Death();
    
    
}