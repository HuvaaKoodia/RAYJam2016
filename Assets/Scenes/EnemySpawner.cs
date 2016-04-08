using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{

    public int enemyAmount;
    public GameObject enemy;
    GameObject[] enemies;

    // Use this for initialization
    void Start()
    {
        enemies = new GameObject[enemyAmount];
        CreateEnemies(enemyAmount);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) CreateEnemies(10);
    }

    void CreateEnemies(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            print("enemy" + i);
            enemies[i] = Instantiate(enemy, new Vector2(Random.Range(-20,20), 15+Random.Range(1,3)), Quaternion.identity) as GameObject;
            enemies[i].GetComponent<EnemyMovement>().enemyType = Random.Range(1, 3);
        }
    }
}
