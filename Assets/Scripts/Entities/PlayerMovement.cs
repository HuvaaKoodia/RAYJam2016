using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    float inputX;
    float inputY;

    public float speed;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        if (inputX != 0 || inputY != 0)
        {
            rb.velocity = Vector2.zero;
            //rb.transform.position += new Vector3(inputX * speed * Time.deltaTime, inputY * speed * Time.deltaTime);
            rb.MovePosition(new Vector2(transform.position.x + inputX * speed * Time.deltaTime,transform.position.y +  inputY * speed * Time.deltaTime));
        }
    }
}
