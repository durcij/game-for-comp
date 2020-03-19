using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour {
	public float totalTime;
	string minutes;
	string seconds;
	float startTime;
	float timeElapsed;
	float timeLeft;
	GameObject timerDisplay;
	GameObject risio;
	GameObject tristitia;
	GameObject furia;
	GameObject dormio;
	Text text;

	bool winScreen;
	GameObject finalScreen;
	Animator anim;
	bool visible;
	GameObject finalScore;
	Text finalScoreText;
	string totalScore;
	string risioScore;
	string tristitiaScore;
	string furiaScore;
	string dormioScore;

	bool winSound;
	AudioSource risioSound;
	AudioSource tristitiaSound;
	AudioSource furiaSound;
	AudioSource dormioSound;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		timerDisplay = GameObject.Find("GameTimer");
		text = timerDisplay.GetComponent<Text>();
		totalTime = 180.0f;
		risio = GameObject.Find("Risio");
		tristitia = GameObject.Find("Tristitia");
		furia = GameObject.Find("Furia");
		dormio = GameObject.Find("Dormio");
		finalScore = GameObject.Find("FinalScore");

		risioSound = GameObject.Find("RisioWin").GetComponent<AudioSource>();
		tristitiaSound = GameObject.Find("TristitiaWin").GetComponent<AudioSource>();
		furiaSound = GameObject.Find("FuriaWin").GetComponent<AudioSource>();
		dormioSound = GameObject.Find("DormioWin").GetComponent<AudioSource>();
		finalScreen = GameObject.Find("FinalScreen");
		finalScoreText = GameObject.Find("FinalScore").GetComponent<Text>();
		anim = finalScreen.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		timeElapsed = Time.time - startTime;
		timeLeft = totalTime - timeElapsed;
		minutes = ((int) timeLeft / 60).ToString();
		seconds = (timeLeft % 60).ToString("f2");
		if ((timeLeft % 60) < 9) {
			text.text = minutes + ":0" + seconds;
		} else {
			text.text = minutes + ":" + seconds;
		}

		if (timeLeft <= 0 && !winScreen) {
			text.text = "0:00.00";

			visible = true;
			anim.SetBool("visible", visible);

			totalScore = (risio.GetComponent<PlayerScoreRisio>().score + tristitia.GetComponent<PlayerScoreTristitia>().score + furia.GetComponent<PlayerScoreFuria>().score + dormio.GetComponent<PlayerScoreDormio>().score).ToString();
			risioScore = (risio.GetComponent<PlayerScoreRisio>().score).ToString();
			tristitiaScore = (tristitia.GetComponent<PlayerScoreTristitia>().score).ToString();
			furiaScore = (furia.GetComponent<PlayerScoreFuria>().score).ToString();
			dormioScore = (dormio.GetComponent<PlayerScoreDormio>().score).ToString();

			finalScoreText.text = "Time's Up!\n\nTotal Score:  " + totalScore + "\nPlayer 1:  " + risioScore + "\nPlayer 2:  " + tristitiaScore + "\nPlayer 3:  " + furiaScore + "\nPlayer 4:  " + dormioScore;

			Destroy(risio);
			Destroy(tristitia);
			Destroy(furia);
			Destroy(dormio);

			if (!winSound) {
				if (risio.GetComponent<PlayerScoreRisio>().score >= tristitia.GetComponent<PlayerScoreTristitia>().score && risio.GetComponent<PlayerScoreRisio>().score >= furia.GetComponent<PlayerScoreFuria>().score && risio.GetComponent<PlayerScoreRisio>().score >= dormio.GetComponent<PlayerScoreDormio>().score) {
					risioSound.Play();
				}
				if (tristitia.GetComponent<PlayerScoreTristitia>().score >= risio.GetComponent<PlayerScoreRisio>().score && tristitia.GetComponent<PlayerScoreTristitia>().score >= furia.GetComponent<PlayerScoreFuria>().score && tristitia.GetComponent<PlayerScoreTristitia>().score >= dormio.GetComponent<PlayerScoreDormio>().score) {
					tristitiaSound.Play();
				}
				if (furia.GetComponent<PlayerScoreFuria>().score >= risio.GetComponent<PlayerScoreRisio>().score && furia.GetComponent<PlayerScoreFuria>().score >= tristitia.GetComponent<PlayerScoreTristitia>().score && furia.GetComponent<PlayerScoreFuria>().score >= dormio.GetComponent<PlayerScoreDormio>().score) {
					furiaSound.Play();
				}
				if (dormio.GetComponent<PlayerScoreDormio>().score >= risio.GetComponent<PlayerScoreRisio>().score && dormio.GetComponent<PlayerScoreDormio>().score >= tristitia.GetComponent<PlayerScoreTristitia>().score && dormio.GetComponent<PlayerScoreDormio>().score >= furia.GetComponent<PlayerScoreFuria>().score) {
					dormioSound.Play();
				}
				winSound = true;
			}

			winScreen = true;
		}
	}
}
