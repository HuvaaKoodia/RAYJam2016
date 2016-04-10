using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class PlayerView : MonoBehaviour
{
	public Rigidbody2D Rigidbody;
	public Collider2D Collider;

	public GameObject DeathParticles;
	public PlayerSkillSystem SkillSystem;
	public PlayerMovement MovementSystem;

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
        CameraControl.I.StartShake(5.0f, 1.0f);
        CameraControl.I.ShowText("Game Over\nPress R to restart");
		Destroy(gameObject);
	}

	public void SetKinematic(bool kinematic)
	{
		Rigidbody.isKinematic = kinematic;
		Collider.isTrigger = kinematic;
	}

	public void SetInputEnabled(bool enabled)
	{
		SkillSystem.enabled = enabled;
		MovementSystem.enabled = enabled;
	}
}