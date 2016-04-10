using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public GameObject DeathParticles;
    Rigidbody2D rb;
    float inputX;
    float inputY;

    float oldPos;
    float newPos;

    float minX, maxX, minY, maxY;

    public PlayerView Player;
    public bool Dodging { get; private set; }
    public float speed, DodgeSpeed = 15f;
    public float DodgeMinDistance = 0.5f, DodgeEndDistance = 0.1f;

    private string currentAnimation = "Nothing";
    private int frame = 0;
    private int animationFrames = 5;

    private Sprite[] sprites = new Sprite[4];
    public SpriteRenderer renderer;

    Animator animator = new Animator();

    // Use this for initialization
    void Start()
    {
        Dodging = false;
        rb = GetComponent<Rigidbody2D>();

        if (Random.Range(0, 100) < 50)
        {
            sprites = Resources.LoadAll<Sprite>("Player/Cat_SpriteSheet");
        }
        else sprites = Resources.LoadAll<Sprite>("Player/Dog_SpriteSheet");

        renderer.sprite = sprites[0];

        oldPos = transform.position.x;

        minX = EnemySpawner.field.x;
        maxX = EnemySpawner.field.y;
        minY = Camera.main.ScreenToWorldPoint(Vector3.zero).y;
        maxY = -Camera.main.ScreenToWorldPoint(Vector3.zero).y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Dodging)
            return;

        //if (frame == animationFrames) frame = 0;
        //frame++;

        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        newPos = transform.position.x;

        if ((newPos < oldPos) && (currentAnimation == "rightOne" || currentAnimation == "rightTwo"))
        {
            renderer.sprite = sprites[0];
        }
        else if ((newPos > oldPos) && (currentAnimation == "leftOne" || currentAnimation == "leftTwo"))
        {
            renderer.sprite = sprites[2];
        }

        if (frame == animationFrames)
        {
            if (newPos < oldPos)
            {
                if (currentAnimation == "leftOne")
                {
                    currentAnimation = "leftTwo";
                    renderer.sprite = sprites[1];
                }
                else
                {
                    currentAnimation = "leftOne";
                    renderer.sprite = sprites[0];
                }
            }
            else if (newPos > oldPos)
            {
                if (currentAnimation == "rightOne")
                {
                    currentAnimation = "rightTwo";
                    renderer.sprite = sprites[3];
                }
                else
                {
                    currentAnimation = "rightOne";
                    renderer.sprite = sprites[2];
                }
            }

            oldPos = newPos;
            frame = 0;
        }


        if (inputX != 0 || inputY != 0)
        {
            rb.velocity = Vector2.zero;
            rb.MovePosition(new Vector2(transform.position.x + inputX * speed * Time.deltaTime, transform.position.y + inputY * speed * Time.deltaTime));

            rb.MovePosition(new Vector2(transform.position.x + inputX * speed * Time.deltaTime, transform.position.y + inputY * speed * Time.deltaTime));
        }

        //Prevent movement outside the screen
        if (rb.position.x < minX) rb.position = new Vector2(minX, rb.position.y);
        if (rb.position.x > maxX) rb.position = new Vector2(maxX, rb.position.y);
        if (rb.position.y < minY) rb.position = new Vector2(rb.position.x, minY);
        if (rb.position.y > maxY) rb.position = new Vector2(rb.position.x, maxY);
        frame++;
    }

    public void Dodge(Vector3 worldPosition)
    {
        if (Dodging)
            return;
        if (Vector3.Distance(transform.position, worldPosition) < DodgeMinDistance) return;
        StartCoroutine(DodgeCoroutine(worldPosition));
    }

    private IEnumerator DodgeCoroutine(Vector3 worldPosition)
    {
        Dodging = true;
        Player.SetKinematic(true);

        Vector3 oldPosition;
        while (Vector3.Distance(transform.position, worldPosition) > DodgeEndDistance)
        {
            oldPosition = transform.position;
            transform.position = Vector3.Lerp(transform.position, worldPosition, Time.deltaTime * DodgeSpeed);

            yield return null;
        }

        transform.position = worldPosition;

        Dodging = false;
        Player.SetKinematic(false);
    }
}
