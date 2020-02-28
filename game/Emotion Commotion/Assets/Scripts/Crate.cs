using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour {

	bool intact;
	Animator anim;
	AudioSource crateBreak;
	GameObject attackRisio;
	CircleCollider2D hurtBox;


	// Use this for initialization
	void Start () {
		intact = true;
		anim = GetComponent <Animator> ();
		crateBreak = GetComponent<AudioSource> ();
		attackRisio = GameObject.Find("RisioAttack");
		hurtBox = attackRisio.GetComponent<CircleCollider2D>();
	}

	// Update is called once per frame
	void Update () {



	}
}
