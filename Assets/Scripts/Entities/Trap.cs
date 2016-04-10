using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trap : MonoBehaviour {

    public float PullDuration;
    public float ActivationTime;
    public float Radius;
    public float TrapForce;
    public int PullNumber;

	public PlayAudio TrapAudio;

    SpriteRenderer sr;
    int timesDone = 0;
    int trapStage = 0; //1 = Planted, 2 = Activating, 3 = Activated
    public Collider2D[] enemies;

    public Transform vortex;

    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        
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
        //shrink the vortex
       if (trapStage != 0) vortex.localScale = new Vector2(vortex.localScale.x - timesDone * Time.deltaTime * 10 - 0.1f, vortex.localScale.y - timesDone * Time.deltaTime * 10 - 0.1f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "enemy" && trapStage == 0)
        {
            trapStage = 1;
            StartCoroutine(Launch(1));
            sr.color = Color.red;

			TrapAudio.Play();
        }
    }
    IEnumerator Launch(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        //if (sr.enabled == true) sr.enabled = false;
        if (vortex.gameObject.activeSelf == false) vortex.gameObject.SetActive(true);
        enemies = Physics2D.OverlapCircleAll(transform.position, Radius + 1.5f * timesDone, Layers.Enemy);
        trapStage = 2;
        vortex.localScale = new Vector2(5 + 1.5f * 5 * timesDone, 5 + 1.5f * 5 * timesDone);
        yield return new WaitForSeconds(PullDuration);
        trapStage = 1;
        timesDone++;
        
        if (timesDone < PullNumber) StartCoroutine(Launch(ActivationTime));
        else Destroy(gameObject);
    }
}
