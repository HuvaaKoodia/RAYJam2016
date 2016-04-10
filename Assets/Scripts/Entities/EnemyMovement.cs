using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public GameObject DeathParticles;
    Rigidbody2D rb;
    PlayerView player;
    Transform swing;
    Vector2 targetPos;
    public float sprint;
    public int score;
    public int enemyType;
    public float speed;
    float zig;
	bool dead = false;

	public EnemyDeathAnimation DeathAnimation;

    // Use this for initialization
    void Awake()
    {
		rb = GetComponent<Rigidbody2D>();
        swing = transform.GetChild(1).transform;

        sprint = 20;
        zig = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.position.y < Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y - 2) Destroy(gameObject);
        if (GameController.I.PlayerDead == true)
        {
			dead = true;
            Instantiate(DeathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

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
                        rb.MovePosition(new Vector2(transform.position.x + (zig - 1) * 20 * Time.deltaTime, transform.position.y - speed * Time.deltaTime));
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
                    if (GameController.I.PlayerDead == true) return;
                    if (player == null)
                    {
                        if (GameObject.Find("Player") != null)
                        {
                            player = GameObject.Find("Player").GetComponent<PlayerView>();
                            targetPos = player.transform.position;
                        }
                    }
                    if (Vector2.Distance((Vector2)transform.position, targetPos) < 1 || sprint < 0)
                    {
                        targetPos = player.transform.position;
                        sprint = 20;
                    }
                    else sprint -= 10 * Time.deltaTime;
                    rb.MovePosition(Vector2.MoveTowards(rb.position, targetPos, speed + (float)EnemySpawner.level / 20 * sprint * 2 * Time.deltaTime));

                    break;
                }
            default: break;
        }
    }

    //kill player
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerView>().Die();
        }
    }

	public void Die(bool addScore)
	{
		if (dead)
			return;//no double death pls!

		if (DeathAnimation)
			DeathAnimation.Animate ();
        if (addScore) CameraControl.I.AddScore(score);
		Destroy (gameObject);
	}

	public void AddSpin(float spin)
	{
		rb.AddTorque (spin, ForceMode2D.Impulse);
	}
}
