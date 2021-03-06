﻿using UnityEngine;
using System.Collections;
using System;

public enum SkillID
{
	Bomb,
	SuperBomb,
	Shield,
	//Seekers,
	Dodge,
	//Slowdown,
	Rapidfire,
	Shotgun,
	Trap,
	Lazor,
	//Pet,
	//Melee,
	_AmountSkills,
	None,
	DestroySkill,
	_AmountAll,
}

public class SkillzDatabase : MonoBehaviour
{
	public static SkillzDatabase I;

	private Type[] types;
	private Sprite[] icons;

	void Awake()
	{
		I = this;

		//init database
		int amount = (int) SkillID._AmountAll;

		types = new Type[amount];
		icons = new Sprite[amount];

		AddSkill(SkillID.Bomb, typeof(BombSkill), "BombIcon", -1);
		AddSkill(SkillID.SuperBomb, typeof(SuperBombSkill), "SuperBombIcon", -1);
		AddSkill(SkillID.Shield, typeof(ShieldSkill), "Icons", 4);
		AddSkill(SkillID.Dodge, typeof(DodgeSkill), "Icons", 0);
		AddSkill(SkillID.Rapidfire, typeof(RapidFireSkill), "Icons", 3);
		AddSkill(SkillID.Shotgun, typeof(ShotgunSkill), "Icons",6);
		AddSkill(SkillID.Lazor, typeof(LazorSkill), "Icons", 2);
		AddSkill(SkillID.Trap, typeof(TrapSkill), "Icons", 7);

		/*
		AddSkill(SkillID.Seekers, typeof(BombSkill), "SeekersIcon");
		AddSkill(SkillID.Slowdown, typeof(BombSkill), "SlowdownIcon");
		
		AddSkill(SkillID.Pet, typeof(BombSkill), "PetIcon");
		AddSkill(SkillID.Melee, typeof(BombSkill), "MeleeIcon");
		*/

		AddSkill(SkillID.DestroySkill, typeof(LazorSkill), "Icons", 5);
	}

	private void AddSkill(SkillID id, Type type, string icon, int index)
	{
		types [(int)id] = type;

		if (index == -1) {
			icons [(int)id] = Resources.Load<Sprite> ("SkillIcons/" + icon);
		} else 
		{
			var sprites = Resources.LoadAll<Sprite> ("SkillIcons/" + icon);
			icons [(int)id] = sprites [index];
		}
	}

	public Sprite GetIcon(SkillID id)
	{
		return icons [(int)id];
	}

	public Type GetSkill(SkillID id)
	{
		return types[(int)id];
	}

	public SkillID GetRandomSkillID()
	{
		return (SkillID)UnityEngine.Random.Range(0, (int)SkillID._AmountSkills);
	}

    public int GetAmountOfSkill()
    {
        return icons.Length;
    }
}
