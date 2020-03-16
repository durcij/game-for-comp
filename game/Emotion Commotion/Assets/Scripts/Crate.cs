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
	public GameObject oneGemote;
	public GameObject fiveGemotes;
	public GameObject tenGemotes;

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
		// Generates six gemotes (totalling 23 gemotes) at random positions near the crate
		Instantiate(oneGemote, new Vector3((transform.position.x + Random.Range(-0.28f, 0.28f)), ((transform.position.y - 0.32f) + Random.Range(-0.32f, 0.32f)), 0), Quaternion.identity);
		Instantiate(oneGemote, new Vector3((transform.position.x + Random.Range(-0.28f, 0.28f)), ((transform.position.y - 0.32f) + Random.Range(-0.32f, 0.32f)), 0), Quaternion.identity);
		Instantiate(oneGemote, new Vector3((transform.position.x + Random.Range(-0.28f, 0.28f)), ((transform.position.y - 0.32f) + Random.Range(-0.32f, 0.32f)), 0), Quaternion.identity);
		Instantiate(fiveGemotes, new Vector3((transform.position.x + Random.Range(-0.28f, 0.28f)), ((transform.position.y - 0.32f) + Random.Range(-0.32f, 0.32f)), 0), Quaternion.identity);
		Instantiate(fiveGemotes, new Vector3((transform.position.x + Random.Range(-0.28f, 0.28f)), ((transform.position.y - 0.32f) + Random.Range(-0.32f, 0.32f)), 0), Quaternion.identity);
		Instantiate(tenGemotes, new Vector3((transform.position.x + Random.Range(-0.28f, 0.28f)), ((transform.position.y - 0.32f) + Random.Range(-0.32f, 0.32f)), 0), Quaternion.identity);
	}
}
