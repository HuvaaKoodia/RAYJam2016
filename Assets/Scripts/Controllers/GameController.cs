﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public static GameController I;

	public PlayerView Player;
	public float RoundTime{get;private set;}

	public Transform PlayerStartPosition;

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
		RemoveExcessStuff ();
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
			Player.SetInputEnabled (false);
			if (PlayerStartPosition) Player.transform.position = PlayerStartPosition.position;

			//setup state
			if (state == 0) 
			{
				//randomize three starting skills
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

				//wait until game starts
				state = 1;
				//GUIController.I.ShowSlotMachinePanel ();
				while (GUIController.I.SlotMachineVisible) yield return null;
			}

			//gameplay state
			if (state == 1)
			{
				Player.SetInputEnabled(true);

				RoundTime = 10f;
			
				EnemySpawner.I.StartWave ();

				Player.SkillSystem.RefreshSkills ();
				GUIController.I.UpdatePlayerSkills (Player.SkillSystem);

				while (state == 1) 
				{
					RoundTime -= Time.deltaTime;

					if (PlayerDead) 
					{
						// game over and all that jazz
						yield break;
					}
						
					if (RoundTime < 0) 
					{
						RoundTime = 0;

						EnemySpawner.level++;
                        CameraControl.I.UpdateLevel();
                        

						yield return new WaitForSeconds(8f);
					
						if (EnemySpawner.I.AmountOfEnemies != 0)
						{
							foreach (Transform child in EnemySpawner.I.EnemyParent) 
							{
								var enemy =  child.GetComponent<EnemyMovement> ();
								if (enemy.transform.position.y > EnemySpawner.I.FieldBottomY)
									enemy.Die (false);
							}

							yield return new WaitForSeconds(1f);
						}

						if (PlayerDead) 
						{
							// game over and all that jazz
							yield break;
						}

                        CameraControl.I.AddScore(200);
						state = 2;
						//round over goto intermission
					}

					yield return null;
				}
			}

			//intermission state
			if (state == 2)
			{
				Player.SetInputEnabled (false);

				GUIController.I.ShowSlotMachinePanel ();

				while (GUIController.I.SlotMachineMoving)
					yield return null;

				if (PlayerStartPosition) Player.transform.position = PlayerStartPosition.position;


				while (GUIController.I.SlotMachineVisible) yield return null;
				{
					yield return null;
				}

				//new skills! New round!

				state = 1;
			}
		}
	}

	private void RemoveExcessStuff()
	{
		//remove all corpses and particle systems! LAZY!
		var corpses = GameObject.FindObjectsOfType<EnemyDeathAnimation>();
		foreach (var corpse in corpses) 
		{
			GameObject.Destroy (corpse.gameObject);	
		}

		var particleSystems = GameObject.FindObjectsOfType<ParticleSystem>();
		foreach (var particleSystem in particleSystems) 
		{
			GameObject.Destroy (particleSystem.gameObject);	
		}
	}
}
