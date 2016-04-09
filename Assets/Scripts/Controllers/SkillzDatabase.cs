using UnityEngine;
using System.Collections;
using System;

public enum SkillID
{
	Bomb,
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
	_Amount,
	None,
	DestroySkill,
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
		int amount = (int) SkillID._Amount;

		types = new Type[amount];
		icons = new Sprite[amount];

		AddSkill(SkillID.Bomb, typeof(BombSkill), "BombIcon");
		AddSkill(SkillID.Shield, typeof(ShieldSkill), "ShieldIcon");
		AddSkill(SkillID.Dodge, typeof(DodgeSkill), "DodgeIcon");
		AddSkill(SkillID.Rapidfire, typeof(RapidFireSkill), "RapidfireIcon");
		AddSkill(SkillID.Shotgun, typeof(ShotgunSkill), "ShotgunIcon");
		AddSkill(SkillID.Lazor, typeof(LazorSkill), "LazorIcon");
        AddSkill(SkillID.Trap, typeof(TrapSkill), "TrapIcon");
		/*
		AddSkill(SkillID.Seekers, typeof(BombSkill), "SeekersIcon");
		AddSkill(SkillID.Slowdown, typeof(BombSkill), "SlowdownIcon");
		
		AddSkill(SkillID.Pet, typeof(BombSkill), "PetIcon");
		AddSkill(SkillID.Melee, typeof(BombSkill), "MeleeIcon");
		*/
	}

	private void AddSkill(SkillID id, Type type, string icon)
	{
		types [(int)id] = type;
		icons [(int)id] = Resources.Load<Sprite>("SkillIcons/"+icon);
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
		return (SkillID)UnityEngine.Random.Range(0, (int)SkillID._Amount);
	}

    public int GetAmountOfSkill()
    {
        return icons.Length;
    }
}
