using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour {

	bool intact;
	bool breaking;
	bool broken;
	float timer;
	float breakWait;
	Animator anim;
	AudioSource statueBreak;
	public GameObject fiftyGemotes;
	public GameObject oneHundredGemotes;

	// Use this for initialization
	void Start () {
		intact = true;
		anim = GetComponent <Animator> ();
		statueBreak = GetComponent<AudioSource> ();
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
			statueBreak.Play();
			breaking = true;
		} else if (breaking && !broken) {
			timer += Time.deltaTime;
			if (timer >= breakWait) {
				broken = true;
				timer = 0.0f;
			}
		} else if (broken) {
			anim.SetBool ("broken", broken);
			timer += Time.deltaTime;
			if (timer >= breakWait) {
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		if (intact) {
			if (other.tag == "Risio Projectile" || other.tag == "Furia Projectile" || other.tag == "Tristitia Projectile" || other.tag == "Dormio Projectile" || other.tag == "Dilectio Projectile" || other.tag == "Verecundia Projectile" || other.tag == "Invidia Projectile" || other.tag == "Mercuria Projectile") {
				intact = false;
				SpawnGemotes();
			}
		}
	}

	void SpawnGemotes() {
		// Generates three gemotes (totalling 200 gemotes) at a random position near the statue
		Instantiate(fiftyGemotes, new Vector3((transform.position.x + Random.Range(-0.24f, 0.24f)), ((transform.position.y + 0.04f) + Random.Range(-0.30f, 0.30f)), 0), Quaternion.identity);
		Instantiate(fiftyGemotes, new Vector3((transform.position.x + Random.Range(-0.24f, 0.24f)), ((transform.position.y + 0.04f) + Random.Range(-0.30f, 0.30f)), 0), Quaternion.identity);
		Instantiate(oneHundredGemotes, new Vector3((transform.position.x + Random.Range(-0.24f, 0.24f)), ((transform.position.y + 0.04f) + Random.Range(-0.30f, 0.30f)), 0), Quaternion.identity);
	}
}
