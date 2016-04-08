using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{

    Rigidbody2D rb;
    GameObject player;
    public float enemyType;
    public float speed;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyType == 1)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, player.transform.position, speed * Time.deltaTime));
        }
        else if (enemyType == 2)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, new Vector2(transform.position.x, transform.position.y - 100), speed * Time.deltaTime));
        }
        
    }
}
