using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRisio : MonoBehaviour {

	public float speed;
	public bool isFiredLeft = false;
	public bool isFiredRight = false;
	public bool isFiredForward = false;
	public bool isFiredBackward = false;
	public bool isFired = false;
	bool alive = true;
	Animator anim;
	public float timer;
	public float lifespan;
	Vector3 spawnPosition;
	GameObject player;
	PlayerMovementRisio playerData;


	// Use this for initialization
	void Awake () {
		anim = GetComponent <Animator> ();

		player = GameObject.Find("Risio");

		speed = 3.0f;

		playerData = player.GetComponent<PlayerMovementRisio>();

		transform.position = player.transform.position;

		timer = 0.0f;

		lifespan = 0.5f;
	}

	// Update is called once per frame
	void Update () {

		CheckLifespan ();
		spawnPosition = player.transform.position; // Sets the projectile's spawn position
		// New if based on player attack methods from before?

		if (!isFired && !isFiredLeft && !isFiredRight && !isFiredForward && !isFiredBackward){
			AnimatingProjectile(isFired);
			transform.position = spawnPosition;
			if (playerData.isAttackingLeft) {
				isFiredLeft = true;
			} else if (playerData.isAttackingRight) {
				isFiredRight = true;
			} else if (playerData.isAttackingForward) {
				isFiredForward = true;
			} else if (playerData.isAttackingBackward) {
				isFiredBackward = true;
			}
		}

		if ((isFiredLeft || isFiredRight || isFiredForward || isFiredBackward) && !isFired) {
			isFired = true;
		}

		if (isFired) {
			if (isFiredForward) {
				transform.Translate(Vector3.down * Time.deltaTime * speed);
				AnimatingProjectile(isFired);
			} else if (isFiredBackward) {
				transform.Translate(Vector3.up * Time.deltaTime * speed);
				AnimatingProjectile(isFired);
			} else if (isFiredLeft) {
				transform.Translate(Vector3.left * Time.deltaTime * speed);
				AnimatingProjectile(isFired);
			} else if (isFiredRight) {
				transform.Translate(Vector3.right * Time.deltaTime * speed);
				AnimatingProjectile(isFired);
			}
		}
	}

	void CheckLifespan () {
		if (isFired) {
			timer += Time.deltaTime;
			if (timer > lifespan) {
				isFired = false;
				isFiredLeft = false;
				isFiredRight = false;
				isFiredForward = false;
				isFiredBackward = false;
				Destroy(gameObject);
			}
		}
	}

	void AnimatingProjectile(bool isFired) {
		anim.SetBool ("isFired", isFired);
	}
}
