using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;

public static class AllSpells
{
	private static List<Spell> spells;
	
	
	public static void AddSpellToUnit(Player player, Spell spell)
	{
		
	}

	public static void GenerateSpells(SpriteAtlas atlas)
	{
		if (spells == null)
		{
			spells = new List<Spell>();
			spells.Add(new Spell(0, "HEAL", 1f, 20f, 10f, "heal", "Лечение"));
			spells.Add(new Spell(1, "ALLDAMAGE", 4f, 20f, 20f, "RainOfFire", "Огненный дождь"));
			spells.Add(new Spell(2, "SINGLEDAMAGE", 3f, 15f, 15f, "FireArrow", "Огненная стрела"));
			spells.Add(new Spell(3, "SINGLEDAMAGE", 3f, 20f, 18f, "IceSpike","едяной шип"));
			spells.Add(new Spell(4, "ALLDAMAGE", 3f, 25f, 20f, "Lightning","Молнии"));
			
			GetImages(atlas);
		}
	}

	public static List<Spell> StarterSpellsForFox()
	{
		List<Spell> sp = new List<Spell>();
		sp.Add(spells[0]);
		sp.Add(spells[1]);
		sp.Add(spells[2]);
		return sp;
	}

	private static void GetImages(SpriteAtlas atlas)
	{
		foreach (Spell spell in spells)
		{
			
			spell.LargeSpell = atlas.GetSprite(spell.NameOfSpell);
			spell.SmallSprite = atlas.GetSprite(spell.NameOfSpell + "_small");
		}
	}

	public static Spell GetSpellById(int id)
	{
		return spells[id];
	}
	
}
