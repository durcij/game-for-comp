using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nullos : MonoBehaviour {

	float timer;
	float deathSoundWait;
	float deathWait;
	float speed;

	bool alive;
	bool dying;
	bool runningLeft;
	bool runningRight;
	bool runningForward;
	bool runningBackward;

	public GameObject fiveGemotes;
	public GameObject tenGemotes;
	public GameObject twentyFiveGemotes;

	Animator anim;
	AudioSource death;


	// Use this for initialization
	void Start () {

		timer = 0.0f;
		deathSoundWait = 0.2f;
		deathWait = 0.6667f;
		speed = 2.0f;

		alive = true;
		dying = false;
		runningLeft = false;
		runningRight = false;
		runningForward = false;
		runningBackward = false;

		anim = GetComponent <Animator> ();
		death = GetComponent<AudioSource> ();

	}

	// Update is called once per frame
	void Update () {

		if (!alive) {
			anim.SetBool ("dead", alive);
			timer += Time.deltaTime;
			if (timer >= deathSoundWait && !dying) {
				dying = true;
				death.Play();
				SpawnGemotes();
			} else if (timer >= deathWait) {
				Destroy(gameObject);
			}
		} else if (runningRight) {
			transform.Translate(Vector3.right * Time.deltaTime * speed);
		} else if (runningLeft) {
			transform.Translate(Vector3.left * Time.deltaTime * speed);
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		if (alive) {
			if (other.tag == "Risio Projectile" || other.tag == "Furia Projectile" || other.tag == "Tristitia Projectile" || other.tag == "Dormio Projectile" || other.tag == "Dilectio Projectile" || other.tag == "Verecundia Projectile" || other.tag == "Invidia Projectile" || other.tag == "Mercuria Projectile") {
				alive = false;
			} else if (other.tag == "Patrol 1") {
				runningLeft = false;
				runningRight = true;
				AnimatingRun(runningLeft, runningRight);
			} else if (other.tag == "Patrol 2"){
				runningRight = false;
				runningLeft = true;
				AnimatingRun(runningLeft, runningRight);
			}
		}
	}

	void SpawnGemotes() {
		// Generates four gemotes (totalling 45 gemotes) at random positions near the nullos
		Instantiate(fiveGemotes, new Vector3((transform.position.x + Random.Range(-0.06f, 0.06f)), ((transform.position.y - 0.08f) + Random.Range(-0.08f, 0.08f)), 0), Quaternion.identity);
		Instantiate(fiveGemotes, new Vector3((transform.position.x + Random.Range(-0.06f, 0.06f)), ((transform.position.y - 0.08f) + Random.Range(-0.08f, 0.08f)), 0), Quaternion.identity);
		Instantiate(tenGemotes, new Vector3((transform.position.x + Random.Range(-0.06f, 0.06f)), ((transform.position.y - 0.08f) + Random.Range(-0.08f, 0.08f)), 0), Quaternion.identity);
		Instantiate(twentyFiveGemotes, new Vector3((transform.position.x + Random.Range(-0.06f, 0.06f)), ((transform.position.y - 0.08f) + Random.Range(-0.08f, 0.08f)), 0), Quaternion.identity);
	}

	void AnimatingRun(bool runningLeft, bool runningRight){
		anim.SetBool ("runningLeft", runningLeft);
		anim.SetBool ("runningRight", runningRight);
	}
}
