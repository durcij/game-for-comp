using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDefault : MonoBehaviour {

	public float speed;
	bool isFired = false;
	Animator anim;
	float timer;
	public float lifespan = 0.5f;

	// Use this for initialization
	void Awake () {
		anim = GetComponent <Animator> ();
	}

	// Update is called once per frame
	void Update () {

		speed = 5.0f;

		if (CheckLifespan()) {
			if (PlayerMovementDefault().isAttackingForward.value) {
				isFired = true;
				transform.Translate(Vector3.down * Time.deltaTime * speed);
				AnimatingProjectile(isFired);
			} else if (PlayerMovementDefault.isAttackingBackward) {
				isFired = true;
				transform.Translate(Vector3.up * Time.deltaTime * speed);
				AnimatingProjectile(isFired);
			} else if (PlayerMovementDefault.isAttackingLeft) {
				isFired = true;
				transform.Translate(Vector3.left * Time.deltaTime * speed);
				AnimatingProjectile(isFired);
			} else if (PlayerMovementDefault.isAttackingRight) {
				isFired = true;
				transform.Translate(Vector3.right * Time.deltaTime * speed);
				AnimatingProjectile(isFired);
			}
		} else {
			// Kill projectile
			isFired = false;
		}
	}

	bool CheckLifespan () {
		bool alive = true;
		timer += Time.deltaTime;
		if (timer >= lifespan) {
			alive = false;
		}
		return alive;
	}

	void AnimatingProjectile(bool isFired) {
		anim.SetBool ("isFired", isFired);
	}
}
