using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Player {

	public void TakeDamage(float damage)
	{
		health -= damage;
	}
	
	protected override void UseFirstDamgeSpell()
	{
		throw new System.NotImplementedException();
	}

	protected override void UseSecondDamgeSpell()
	{
		throw new System.NotImplementedException();
	}

	protected override void UseHealSpell()
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
		throw new System.NotImplementedException();
	}
}
