using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerView : MonoBehaviour
{
	public Rigidbody2D Rigidbody;
	public Collider2D Collider;

	public GameObject DeathParticles;
	public PlayerSkillSystem SkillSystem;

	public event System.Action OnDeathEvent;

	public void Start()
	{
		transform.position = new Vector3(transform.position.x,transform.position.y, 0);
	}

	public void Die()
	{
		if (OnDeathEvent != null)
			OnDeathEvent ();

		Instantiate (DeathParticles, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	public void SetKinematic(bool kinematic)
	{
		Rigidbody.isKinematic = kinematic;
		Collider.isTrigger = kinematic;
	}
}