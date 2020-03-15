using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

	bool intact;
	Animator anim;
	AudioSource crateBreak;


	// Use this for initialization
	void Start () {
		intact = true;
		anim = GetComponent <Animator> ();
		crateBreak = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		if (!intact) {
			anim.SetBool ("intact", intact);
			crateBreak.Play();
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
