using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell
{
	private int ID;
	private string type;
	private float cooldown;
	private float value;
	private float manaValue;
	private string nameOfSpell;
	
	public Spell(int id, string type, float cooldown, float value, float manaValue, string nameOfSpell)
	{
		this.ID = id;
		this.type = type;
		this.cooldown = cooldown;
		this.value = value;
		this.manaValue = manaValue;
		this.nameOfSpell = nameOfSpell;
	}

	public float ManaValue
	{
		get { return manaValue; }
	}

	public string Type
	{
		get { return type; }
	}

	public float Cooldown
	{
		get { return cooldown; }
	}

	public float Value
	{
		get { return value; }
	}

	public string NameOfSpell
	{
		get { return nameOfSpell; }
	}

	public int Id
	{
		get { return ID; }
	}
}
