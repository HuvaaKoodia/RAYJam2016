using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    float inputX;
    float inputY;

	public PlayerView Player;
	public bool Dodging{ get; private set;}
	public float speed, DodgeSpeed = 15f;
	public float DodgeMinDistance = 0.5f, DodgeEndDistance = 0.1f;

    // Use this for initialization
    void Start()
    {
		Dodging = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Dodging)
			return;

        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");
        if (inputX != 0 || inputY != 0)
        {
            rb.velocity = Vector2.zero;
            //rb.transform.position += new Vector3(inputX * speed * Time.deltaTime, inputY * speed * Time.deltaTime);
            rb.MovePosition(new Vector2(transform.position.x + inputX * speed * Time.deltaTime,transform.position.y +  inputY * speed * Time.deltaTime));
        }
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
		Player.SetKinematic (true);

		Vector3 oldPosition;
		while(Vector3.Distance(transform.position, worldPosition) > DodgeEndDistance)
		{
			oldPosition = transform.position;
			transform.position = Vector3.Lerp(transform.position, worldPosition, Time.deltaTime * DodgeSpeed);

			yield return null;
		}

		transform.position = worldPosition;

		Dodging = false;
		Player.SetKinematic (false);
	}

}
