using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

	bool intact;
	bool breaking;
	bool broken;
	float timer;
	float breakWait;
	Animator anim;
	AudioSource crateBreak;


	// Use this for initialization
	void Start () {
		intact = true;
		anim = GetComponent <Animator> ();
		crateBreak = GetComponent<AudioSource> ();
		anim.SetBool ("intact", intact);
		breaking = false;
		broken = false;
		breakWait = 0.417f;
		timer = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		if (!intact && !breaking && !broken) {
			anim.SetBool ("intact", intact);
			anim.SetBool ("broken", broken);
			crateBreak.Play();
			breaking = true;
		} else if (breaking && !broken) {
			timer += Time.deltaTime;
			if (timer >= breakWait) {
				broken = true;
			}
		} else if (broken) {
			anim.SetBool ("broken", broken);
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		if (intact) {
			if (other.tag == "Risio Projectile" || other.tag == "Furia Projectile" || other.tag == "Tristitia Projectile" || other.tag == "Dormio Projectile" || other.tag == "Dilectio Projectile" || other.tag == "Verecundia Projectile" || other.tag == "Invidia Projectile" || other.tag == "Mercuria Projectile") {
				intact = false;
			}
		}
	}
}
