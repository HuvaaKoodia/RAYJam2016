using UnityEngine;
using System.Collections;

public class PlayerView : MonoBehaviour
{
	public bool Dead{ get; private set;}

	public PlayerSkillSystem SkillSystem;

	public void Die()
	{
		Dead = true;
	}
}
