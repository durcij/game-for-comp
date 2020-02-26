using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDefault : MonoBehaviour {

	public float speed;
	bool isFired = false;
	bool alive = true;
	Animator anim;
	float timer;
	public float lifespan = 0.5f;
	PlayerMovementDefault PlayerData = new PlayerMovementDefault();
	Vector3 originalPosition;


	// Use this for initialization
	void Awake () {
		anim = GetComponent <Animator> ();

		originalPosition = gameObject.transform.position; // Sets the projectile's original position
	}

	// Update is called once per frame
	void Update () {

		speed = 5.0f;

		CheckLifespan ();

		// New if based on player attack methods from before?
		if (!isFired) {
			if (PlayerData.isAttackingForward) {
				isFired = true;
				transform.Translate(Vector3.down * Time.deltaTime * speed);
				AnimatingProjectile(isFired);
			} else if (PlayerData.isAttackingBackward) {
				isFired = true;
				transform.Translate(Vector3.up * Time.deltaTime * speed);
				AnimatingProjectile(isFired);
			} else if (PlayerData.isAttackingLeft) {
				isFired = true;
				transform.Translate(Vector3.left * Time.deltaTime * speed);
				AnimatingProjectile(isFired);
			} else if (PlayerData.isAttackingRight) {
				isFired = true;
				transform.Translate(Vector3.right * Time.deltaTime * speed);
				AnimatingProjectile(isFired);
			}
		} else {
			// Kill projectile
			AnimatingProjectile(isFired);
		}
	}

	void CheckLifespan () {
		timer += Time.deltaTime;
		if (timer >= lifespan) {
			isFired = false;
		}
	}

	void AnimatingProjectile(bool isFired) {
		anim.SetBool ("isFired", isFired);
	}
}
