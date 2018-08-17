using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell 
{
	private String type;
	private float cooldown;
	private float value;
	private float manaValue;

	public Spell(string type, float cooldown, float value, float manaValue)
	{
		this.type = type;
		this.cooldown = cooldown;
		this.value = value;
		this.manaValue = manaValue;
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
}
