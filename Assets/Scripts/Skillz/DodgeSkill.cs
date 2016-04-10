using UnityEngine;
using System.Collections;

public class DodgeSkill : PlayerSkillBase
{
    public PlayAudio DodgeAudio;

	protected override void OnActivate ()
	{
        DodgeAudio.Play();

		movementSystem.Dodge (skillSystem.MousePosition);
	}
}
