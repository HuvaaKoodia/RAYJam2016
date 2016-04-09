using UnityEngine;
using System.Collections;
using System;

public enum SkillID
{
	Bomb,
	Shield,
	Seekers,
	Dodge,
	Slowdown,
	Rapidfire,
	Trap,
	Lazor,
	Pet,
	Melee,
	_Amount,
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
		AddSkill(SkillID.Shield, typeof(BombSkill), "ShieldIcon");
		AddSkill(SkillID.Seekers, typeof(BombSkill), "SeekersIcon");
		AddSkill(SkillID.Dodge, typeof(BombSkill), "DodgeIcon");
		AddSkill(SkillID.Slowdown, typeof(BombSkill), "SlowdownIcon");
		AddSkill(SkillID.Rapidfire, typeof(BombSkill), "RapidfireIcon");
		AddSkill(SkillID.Trap, typeof(BombSkill), "TrapIcon");
		AddSkill(SkillID.Lazor, typeof(BombSkill), "LazorIcon");
		AddSkill(SkillID.Pet, typeof(BombSkill), "PetIcon");
		AddSkill(SkillID.Melee, typeof(BombSkill), "MeleeIcon");
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
}
