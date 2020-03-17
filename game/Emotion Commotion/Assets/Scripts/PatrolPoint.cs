using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour {

	CircleCollider2D point;

	// Use this for initialization
	void Start () {
		point = GetComponent<CircleCollider2D>();
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Nullos") {
			point.enabled = true;
		} else {
			point.enabled = false;
		}
	}

}
