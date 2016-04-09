using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	public static GameController I;

	public PlayerView Player;
	public float RoundTime{get;private set;}

	private int state = 1;

	void Awake() 
	{
		I = this;
        RoundTime = 10.0f;
	}

	void Start()
	{
		StartCoroutine (StateCoroutine ());
	}
	
	IEnumerator StateCoroutine()
	{
		while (true) 
		{
			//setup state
			if (state == 0) 
			{
				//randomize three skills for player
				Player.SkillSystem.ReplaceSkill(0, SkillzDatabase.I.GetRandomSkillID());
				Player.SkillSystem.ReplaceSkill(1, SkillzDatabase.I.GetRandomSkillID());
				Player.SkillSystem.ReplaceSkill(2, SkillzDatabase.I.GetRandomSkillID());

				//wait until game starts
				while (state == 0) yield return null;
			}

			//gameplay state
			if (state == 1)
			{
				RoundTime = 10f;
			
				while (state == 1) 
				{
					if (Player.Dead) 
					{
						// game over and all that jazz
					}

					RoundTime -= Time.deltaTime;
					if (RoundTime < 0) 
					{
						RoundTime = 0;

						//round over goto intermission
					}

					yield return null;
				}
			}

			//intermission state
			if (state == 2)
			{
				//give player news skills, or remove some
				while (state == 2) 
				{
					
					yield return null;
				}
			}
		}

	}
}
