using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player : MonoBehaviour
{
	protected int ID;
	protected float health;
	protected float mana;
	protected float speed;
	protected float forceForJump;
	protected Spell[] spells;
	protected Spell currentHealSpell;
	protected Spell currentFirstDamageSpell;
	protected Spell currentSecondDamageSpell;

	protected abstract void UseSpell(Spell spell);
	protected abstract void Move();
	protected abstract void Jump();
	protected abstract void Death();
}
