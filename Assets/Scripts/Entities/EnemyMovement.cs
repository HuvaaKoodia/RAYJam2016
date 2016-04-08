using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{

    Rigidbody2D rb;
    GameObject player;
    Transform swing;
    public float sprint;
    public float enemyType;
    public float speed;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        swing = transform.GetChild(1).transform;
        player = GameObject.Find("Player");
        sprint = 20;
    }

    // Update is called once per frame
    void Update()
    {
        sprint -= 10* Time.deltaTime;
        if (enemyType == 1)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, player.transform.position, speed * sprint/2 * Time.deltaTime));
        }
        else if (enemyType == 2)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, new Vector2(transform.position.x, transform.position.y - 100), speed * sprint * Time.deltaTime));
        }
        //swing.eulerAngles = new Vector3(0,0,swing.eulerAngles.z+5);
        if (sprint < 0) sprint = 20;
        
    }
}
