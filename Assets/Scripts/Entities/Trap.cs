using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trap : MonoBehaviour {

    public float PullDuration;
    public float ActivationTime;
    public float Radius;
    public float TrapForce;
    public int PullNumber;

    int timesDone = 0;
    int trapStage = 0; //1 = Planted, 2 = Activating, 3 = Activated
    public Collider2D[] enemies;

    void Awake()
    {
        
    }

    void Update()
    {
        if (trapStage == 2)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null)
                {
                    Vector2 pos = enemies[i].gameObject.transform.position;
                    pos = Vector2.MoveTowards(pos, transform.position, TrapForce * timesDone / 1.5f * Vector2.Distance(pos, transform.position) * Time.deltaTime);
                    enemies[i].transform.position = pos;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "enemy" && trapStage == 0)
        {
            trapStage = 1;
            StartCoroutine(Launch());

        }
    }
    IEnumerator Launch()
    {
        yield return new WaitForSeconds(ActivationTime);
        enemies = Physics2D.OverlapCircleAll(transform.position, Radius + 1.5f * timesDone, Layers.Enemy);
        print(enemies);
        trapStage = 2;
        yield return new WaitForSeconds(PullDuration);
        trapStage = 1;
        timesDone++;
        if (timesDone < PullNumber) StartCoroutine(Launch());
        else Destroy(gameObject);
    }
}
