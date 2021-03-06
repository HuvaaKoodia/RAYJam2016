﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
	public static EnemySpawner I;

    public static int level;

    public int enemyAmount;
    float spawnTime;
    public static Vector2 field;

	public Transform EnemyParent;

    public EnemyMovement enemy; // Goes down
    public EnemyMovement enemy2; // Goes down with sprints
    public EnemyMovement enemy3; // zigzag
    public EnemyMovement enemy4; // homing 

	public float FieldBottomY{get;private set;}

	void Awake()
	{
		I = this;

		level = 1;

		var worldCenter = Camera.main.ScreenToWorldPoint (Vector3.zero);
		field = new Vector2(worldCenter.x, -worldCenter.x);
		FieldBottomY = worldCenter.y;
	}

    // Update is called once per frame     
    void Update()
    {
		if (GameController.I.RoundTime == 0 || GameController.I.RoundTime == 10  || GameController.I.PlayerDead == true) return;

        spawnTime -= Time.deltaTime;
        if (spawnTime < 0)
        {
            int spawnType = Random.Range(2, 5);
            float min = Random.Range(field.x, field.y);
            float max = min + 3; //+ Random.Range(0, 3);

            switch (spawnType)
            {
                case 1:
                    {
                        StartCoroutine(Spawn(enemy, Random.Range(5, 6), 0.2f, field.x, field.y));
                        break;
                    }
                case 2:
                    {
                        StartCoroutine(Spawn(enemy2, 1, 0.2f, min, min));
                        break;
                    }
                case 3:
                    {
                        StartCoroutine(Spawn(enemy3, Random.Range(5, 6), 0.1f, min, min));
                        min = Random.Range(field.x, field.y);
                        StartCoroutine(Spawn(enemy3, Random.Range(5, 6), 0.2f, min, min));
                        break;
                    }
                case 4:
                    {
                        StartCoroutine(Spawn(enemy4, Random.Range(3, 4), 1f, min, max));
                        break;
                    }
                default: break;
            }

            spawnTime = Mathf.Clamp(2-(float)level/5,1,2);
        }

        // TEST COMMANDS
        if (Input.GetKeyDown(KeyCode.E)) StartWave();
        if (Input.GetKeyDown(KeyCode.Alpha1)) level = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) level = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) level = 3;
        if (Input.GetKeyDown(KeyCode.Alpha4)) level = 4;
        if (Input.GetKeyDown(KeyCode.Alpha5)) level = 5;
        if (Input.GetKeyDown(KeyCode.Alpha0)) level = 10;
    }

	public void StartWave()
	{
		StartCoroutine(Spawn(enemy, 1, Mathf.Clamp(0.15f*1/level,0.02f,1), field.x, field.y));
	}

	IEnumerator Spawn(EnemyMovement enemyPrefab, int amount, float spawnSpeed, float spawnMin, float spawnMax)
    {
        if (enemyPrefab.enemyType == 1) // Red blocks
        {
            while (GameController.I.RoundTime > 0 && GameController.I.PlayerDead == false)
            {
				EnemyMovement enemy = Instantiate(enemyPrefab, new Vector2(Random.Range(spawnMin, spawnMax), 10), Quaternion.identity) as EnemyMovement;
				enemy.transform.SetParent (EnemyParent);
                enemy.speed = Random.Range(3, 5) + (float)level/5;

				//SPIN!
				enemy.AddSpin(Random.Range(-0.5f, 0.5f));

                yield return new WaitForSeconds(Mathf.Clamp(0.15f * 1 / level, 0.02f, 1));
            }
        }
        else // Everything else as waves
        {
            if (enemyPrefab.enemyType == 2)
            {
                print("enemy2");
                EnemyMovement enemy = Instantiate(enemyPrefab, new Vector2(spawnMin, 10), Quaternion.identity) as EnemyMovement;
                enemy.transform.SetParent(EnemyParent);
                yield return new WaitForSeconds(spawnSpeed);
                enemy = Instantiate(enemyPrefab, new Vector2(spawnMin-1, 10), Quaternion.identity) as EnemyMovement;
                enemy.transform.SetParent(EnemyParent);
                enemy = Instantiate(enemyPrefab, new Vector2(spawnMin  + 1, 10), Quaternion.identity) as EnemyMovement;
                enemy.transform.SetParent(EnemyParent);
                yield return new WaitForSeconds(spawnSpeed);
                enemy = Instantiate(enemyPrefab, new Vector2(spawnMin - 2, 10), Quaternion.identity) as EnemyMovement;
                enemy.transform.SetParent(EnemyParent);
                enemy = Instantiate(enemyPrefab, new Vector2(spawnMin + 2, 10), Quaternion.identity) as EnemyMovement;
                enemy.transform.SetParent(EnemyParent);
            }
            for (int i = 0; i < amount; i++)
            {
				EnemyMovement enemy = Instantiate(enemyPrefab, new Vector2(spawnMin, 10), Quaternion.identity) as EnemyMovement;
				enemy.transform.SetParent (EnemyParent);
                yield return new WaitForSeconds(spawnSpeed);
            }
        }
    }

	public int AmountOfEnemies{get{return EnemyParent.childCount; }}
}
