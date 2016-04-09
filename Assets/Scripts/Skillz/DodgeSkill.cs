using UnityEngine;
using System.Collections;

public class DodgeSkill : PlayerSkillBase
{
	protected override void OnActivate ()
	{
		movementSystem.Dodge (skillSystem.MousePosition);
	}
}
