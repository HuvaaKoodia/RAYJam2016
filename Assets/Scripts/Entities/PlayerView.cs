using UnityEngine;
using System.Collections;

public class PlayerView : MonoBehaviour
{
	public bool Dead{ get; private set;}

	public Rigidbody2D Rigidbody;
	public Collider2D Collider;

	public GameObject DeathParticles;
	public PlayerSkillSystem SkillSystem;

	public void Start()
	{
		transform.position = new Vector3(transform.position.x,transform.position.y, 0);
	}

	public void Die()
	{
		Dead = true;

		Instantiate (DeathParticles, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	public void SetKinematic(bool kinematic)
	{
		Rigidbody.isKinematic = kinematic;
		Collider.isTrigger = kinematic;
	}
}