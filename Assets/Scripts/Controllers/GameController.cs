using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public static GameController I;

	public PlayerView Player;
	public float RoundTime{get;private set;}

	public bool PlayerDead{get;private set;}

	private int state = 0;

	void Awake() 
	{
		I = this;
        RoundTime = 10.0f;
		PlayerDead = false;

		Player.OnDeathEvent += OnPlayerDeath;
	}

	private void OnPlayerDeath ()
	{
		PlayerDead = true;
	}
		
	void Start()
	{
		StartCoroutine (StateCoroutine ());
	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.R))
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	IEnumerator StateCoroutine()
	{
		while (true) 
		{
			//setup state
			if (state == 0) 
			{
				//randomize three skills for player

				//HACK
				List<SkillID> skills = new List<SkillID>();
			
				for (int i = 0; i < 3; i++) 
				{
					SkillID skill;

					do {
						skill = SkillzDatabase.I.GetRandomSkillID();
					} while (skills.Contains(skill));

					skills.Add (skill);
				}
			
				Player.SkillSystem.ReplaceSkill(0, skills[0]);
				Player.SkillSystem.ReplaceSkill(1, skills[1]);
				Player.SkillSystem.ReplaceSkill(2, skills[2]);

				Player.SkillSystem.RefreshSkills ();

				GUIController.I.UpdatePlayerSkills (Player.SkillSystem);

				//wait until game starts
				state = 1;
				while (state == 0) yield return null;
			}

			//gameplay state
			if (state == 1)
			{
				RoundTime = 10f;
			
				EnemySpawner.I.StartWave ();

				while (state == 1) 
				{
					if (PlayerDead) 
					{
						// game over and all that jazz
					}

					RoundTime -= Time.deltaTime;
					if (RoundTime < 0) 
					{
						RoundTime = 0;

						EnemySpawner.level++;
						state = 2;
						//round over goto intermission
					}

					yield return null;
				}
			}

			//intermission state
			if (state == 2)
			{
				yield return new WaitForSeconds (4.5f);
				state = 0;

				//give player news skills, or remove some
				while (state == 2) 
				{
					
					yield return null;
				}
			}
		}

	}
}
