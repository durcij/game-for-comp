using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreDefault : MonoBehaviour {
	public static int score;
	GameObject scoreDisplay;
	Text text;
	bool newFrame;

	// Use this for initialization
	void Start () {
		scoreDisplay = GameObject.Find("RisioScore");
		text = scoreDisplay.GetComponent<Text>();
		score = 0;
	}

	// Update is called once per frame
	void Update () {
		text.text = "Player:  " + score;
		newFrame = true;
	}

	void OnTriggerEnter2D (Collider2D other){
		if (newFrame) {
			if (other.tag == "1 Gemote") {
				score += 1;
			} else if (other.tag == "5 Gemotes") {
				score += 5;
			} else if (other.tag == "10 Gemotes") {
				score += 10;
			} else if (other.tag == "25 Gemotes") {
				score += 25;
			} else if (other.tag == "50 Gemotes") {
				score += 50;
			} else if (other.tag == "100 Gemotes") {
				score += 100;
			}
			newFrame = false;
		}
	}
}
