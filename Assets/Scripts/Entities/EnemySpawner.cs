using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
	public static EnemySpawner I;

    public static int level;

    public int enemyAmount;
    float spawnTime;
    public static Vector2 field;

    public PlayerView player;

    public EnemyMovement enemy; // Goes down
    public EnemyMovement enemy2; // Goes down with sprints
    public EnemyMovement enemy3; // zigzag
    public EnemyMovement enemy4; // homing 
    EnemyMovement[] enemies;

	void Awake()
	{
		I = this;

		level = 1;
		field = new Vector2(Camera.main.ScreenToWorldPoint(Vector3.zero).x, -Camera.main.ScreenToWorldPoint(Vector3.zero).x);
		enemies = new EnemyMovement[enemyAmount];
	}

    // Update is called once per frame     
    void Update()
    {
        if (GameController.I.RoundTime == 0 || player.Dead == true) return;
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
                        StartCoroutine(Spawn(enemy2, Random.Range(5, 6), 0.2f, min, min));
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
        if (Input.GetKeyDown(KeyCode.E)) StartCoroutine(Spawn(enemy, enemyAmount, 0.1f, -4, 2));
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

    IEnumerator Spawn(EnemyMovement enemy, int amount, float spawnSpeed, float spawnMin, float spawnMax)
    {
        if (enemy.enemyType == 1) // Red blocks
        {
            print("RoundTime " + GameController.I.RoundTime);
            while (GameController.I.RoundTime > 0)
            {
                EnemyMovement def = Instantiate(enemy, new Vector2(Random.Range(spawnMin, spawnMax), 10), Quaternion.identity) as EnemyMovement;
                def.speed = Random.Range(3, 5) + (float)level/5;
                yield return new WaitForSeconds(Mathf.Clamp(0.15f * 1 / level, 0.02f, 1));
            }
        }
        else // Everything else as waves
        {
            for (int i = 0; i < amount; i++)
            {
                enemies[i] = Instantiate(enemy, new Vector2(spawnMin, 10), Quaternion.identity) as EnemyMovement;
                yield return new WaitForSeconds(spawnSpeed);
            }
        }
    }
}
