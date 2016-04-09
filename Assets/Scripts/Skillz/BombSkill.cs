﻿using UnityEngine;
using System.Collections;

public class BombSkill: PlayerSkillBase 
{
	public Bomb BombPrefab;
	public float ShootSpeed = 5f;

	protected override void OnActivate ()
	{
		var bomb = Instantiate (BombPrefab, transform.position + skillSystem.MouseDirection * 2f, Quaternion.identity) as Bomb;
		bomb.Rigidbody.AddForce (skillSystem.MouseDirection * ShootSpeed, ForceMode2D.Impulse);
	}
}