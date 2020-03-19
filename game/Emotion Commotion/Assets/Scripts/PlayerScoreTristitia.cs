using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreTristitia : MonoBehaviour {
	public int score;
	GameObject scoreDisplay;
	Text text;
	bool newFrame;

	// Use this for initialization
	void Start () {
		scoreDisplay = GameObject.Find("TristitiaScore");
		text = scoreDisplay.GetComponent<Text>();
		score = 0;
	}

	// Update is called once per frame
	void Update () {
		text.text = "Player 2:  " + score;
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
		} else if (other.tag == "Nullos" || other.tag == "Risio Projectile" || other.tag == "Furia Projectile" || other.tag == "Dormio Projectile" || other.tag == "Dilectio Projectile" || other.tag == "Verecundia Projectile" || other.tag == "Invidia Projectile" || other.tag == "Mercuria Projectile") {
			if (score > 50) {
				score -= 50;
			} else {
				score = 0;
			}
		}
	}
}
