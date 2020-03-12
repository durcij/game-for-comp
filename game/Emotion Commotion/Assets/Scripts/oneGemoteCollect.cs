using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oneGemoteCollect : MonoBehaviour {

	AudioSource collected;
  float timer;
  float soundWait;
  bool destroy = false;

	// Use this for initialization
	void Start () {
		collected = GetComponent<AudioSource>();
    timer = 0.0f;
    soundWait = 0.5f;
	}

	// Update is called once per frame
	void Update () {
    if (destroy) {
      timer += Time.deltaTime;
      if (timer >= soundWait) {
        Destroy(gameObject);
      }
    }
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Player") {
      collected.Play();
      destroy = true;
		}
	}
}
