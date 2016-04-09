using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{

    Rigidbody2D rb;
    GameObject player;
    Transform swing;
    Vector2 targetPos;
    public float sprint;
    public int enemyType;
    public float speed;
    float zig;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        swing = transform.GetChild(1).transform;
        player = GameObject.Find("Player");
        sprint = 20;
        zig = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.position.y < Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y - 2) Destroy(gameObject);
        switch (enemyType)
        {
            case 1: // Basic enemy moving down
                {
                    rb.MovePosition(new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime));
                    break;
                }
            case 2: // Basic enemy moving down with sprints
                {
                    if (sprint < 0) sprint = 20; else sprint -= 20 * Time.deltaTime;
                    rb.MovePosition(Vector2.MoveTowards(rb.position, new Vector2(transform.position.x, transform.position.y - 1), speed * sprint * 2 * Time.deltaTime));
                    break;
                }
            case 3: // zigzag
                {
                    speed = 4;
                    if (zig >= 1)
                    {
                        rb.MovePosition(new Vector2(transform.position.x + (zig-1) * 20 * Time.deltaTime, transform.position.y - speed * Time.deltaTime));
                    }
                    else
                    {
                       rb.MovePosition(new Vector2(transform.position.x - zig * 20 * Time.deltaTime, transform.position.y - speed * Time.deltaTime));
                    }
                    if (sprint < 0) sprint = 20; else sprint -= 10 * Time.deltaTime;
                    if (zig <= 0) zig = 2; else zig -= Time.deltaTime;
                    break;
                }
            case 4: // sprint to player position
                {
                    if (sprint < 0)
                    {
                        sprint = 20;
                        targetPos = player.transform.position;
                    }
                    else sprint -= 10 * Time.deltaTime;
                    rb.MovePosition(Vector2.MoveTowards(rb.position, targetPos, speed + (float)EnemySpawner.level/20 * sprint*2 * Time.deltaTime));
                    break;
                }
            default: break;
        }


    }
}
