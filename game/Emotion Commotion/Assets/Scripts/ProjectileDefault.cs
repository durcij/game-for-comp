﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDefault : MonoBehaviour {

	public float speed;
	bool isFiredLeft = false;
	bool isFiredRight = false;
	bool isFiredForward = false;
	bool isFiredBackward = false;
	bool isFired = false;
	bool alive = true;
	Animator anim;
	float timer;
	public float lifespan = 0.5f;
	Vector3 spawnPosition;
	GameObject player;
	PlayerMovementDefault playerData;


	// Use this for initialization
	void Awake () {
		anim = GetComponent <Animator> ();

		player = GameObject.Find("Risio");

		speed = 3.0f;

		playerData = player.GetComponent<PlayerMovementDefault>();
	}

	// Update is called once per frame
	void Update () {

		CheckLifespan ();

		// New if based on player attack methods from before?

		if (!isFired){
			spawnPosition = player.transform.position; // Sets the projectile's spawn position
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

		if (isFiredLeft || isFiredRight || isFiredForward || isFiredBackward) {
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
			if (timer >= lifespan) {
				isFired = false;
				isFiredLeft = false;
				isFiredRight = false;
				isFiredForward = false;
				isFiredBackward = false;
				timer = 0.0f;
			}
		}
	}

	void AnimatingProjectile(bool isFired) {
		anim.SetBool ("isFired", isFired);
	}
}
