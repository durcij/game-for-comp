using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTristitia : MonoBehaviour {

	private float speed;
	bool isMovedLeft = false;
	bool isMovedRight = false;
  bool isMovedForward = false;
  bool isMovedBackward = false;
  public bool isAttackingLeft = false;
  public bool isAttackingRight = false;
  public bool isAttackingForward = false;
  public bool isAttackingBackward = false;
	bool isSlidingLeft = false;
	bool isSlidingRight = false;
	bool isSlidingForward = false;
	bool isSlidingBackward = false;
  bool isFacingLeft = false;
  bool isFacingRight = false;
  bool isFacingForward = true;
  bool isFacingBackward = false;
  bool hasAttacked = false;
	bool isMoving = false;
	public bool hurt = false;
	Animator anim;
  AudioSource attack;
	Rigidbody2D physics;
	private float attackWait;
	float timer;
	float harmWait;
	float harmTimer;
	public GameObject tristitiaAttack;
	public GameObject oneGemote;
	public GameObject fiveGemotes;
	int score;
	AudioSource pain;
	bool vulnerable;
	bool hasSlid;
	AudioSource slide;
	float slideTimer;
	float slideWait;


	void Awake () {
		anim = GetComponent <Animator> ();
		attack = GetComponent<AudioSource> ();
		physics = GetComponent<Rigidbody2D> ();
		timer = 0.0f;
		harmTimer = 0.0f;
		attackWait = 0.5f;
		harmWait = 0.5f;
		speed = 120.0f;
		score = GetComponent<PlayerScoreTristitia>().score;
		pain = GameObject.Find("TristitiaHurtBox").GetComponent<AudioSource>();
		vulnerable = true;
		slide = GameObject.Find("TristitiaSlide").GetComponent<AudioSource>();
		slideTimer = 0.0f;
		slideWait = .91665f;
	}

	// Update is called once per frame
	void Update () {

		checkAttack ();
		checkHarm ();
		checkSlide();

		if(hurt && vulnerable) {
			vulnerable = false;

			isMoving = false;

			isMovedLeft = false;
			isMovedRight = false;
			isMovedForward = false;
			isMovedBackward = false;

			isFacingLeft = false;
			isFacingRight = false;
			isFacingForward = false;
			isFacingBackward = false;

			isSlidingLeft = false;
			isSlidingRight = false;
			isSlidingForward = false;
			isSlidingBackward = false;

			AnimatingPain(hurt);
			AnimatingMove(isMovedLeft, isMovedRight, isMovedForward, isMovedBackward);
			AnimatingIdle(isFacingLeft, isFacingRight, isFacingForward, isFacingBackward, hurt, hasSlid);
			AnimatingSlide(isSlidingLeft, isSlidingRight, isSlidingForward, isSlidingBackward);
			pain.Play();
			SpawnGemotes();
		}

		if (!hasAttacked && !isAttackingLeft && !isAttackingRight && !isAttackingForward && !isAttackingBackward && !hurt && vulnerable && !hasSlid){
			if (Input.GetKey(KeyCode.LeftArrow) && (!isMoving || isMovedLeft)) // Move left
				{
					isMovedLeft = true;
					isMovedRight = false;
					isMovedForward = false;
					isMovedBackward = false;

	        isFacingLeft = true;
	        isFacingRight = false;
	        isFacingForward = false;
	        isFacingBackward = false;

					isMoving = true;

					AnimatingMove(isMovedLeft, isMovedRight, isMovedForward, isMovedBackward);
				} else if (Input.GetKey(KeyCode.RightArrow) && (!isMoving || isMovedRight)) // Move right
					{
						isMovedRight = true;
						isMovedLeft = false;
						isMovedForward = false;
						isMovedBackward = false;

	        	isFacingLeft = false;
	        	isFacingRight = true;
	        	isFacingForward = false;
	        	isFacingBackward = false;

						isMoving = true;

						AnimatingMove(isMovedLeft, isMovedRight, isMovedForward, isMovedBackward);
				} else if (Input.GetKey(KeyCode.DownArrow) && (!isMoving || isMovedForward)) // Move forward
	  			{
	  				isMovedForward = true;
						isMovedLeft = false;
						isMovedRight = false;
	  				isMovedBackward = false;

	          isFacingLeft = false;
	          isFacingRight = false;
	          isFacingForward = true;
	          isFacingBackward = false;

						isMoving = true;

	  				AnimatingMove(isMovedLeft, isMovedRight, isMovedForward, isMovedBackward);
	  		} else if (Input.GetKey(KeyCode.UpArrow) && (!isMoving || isMovedBackward)) // Move backward
					{
	  				isMovedBackward = true;
						isMovedLeft = false;
						isMovedRight = false;
	  				isMovedForward = false;

	        	isFacingLeft = false;
	        	isFacingRight = false;
	        	isFacingForward = false;
	        	isFacingBackward = true;

						isMoving = true;

	  				AnimatingMove(isMovedLeft, isMovedRight, isMovedForward, isMovedBackward);
				} else // Doesn't move
					{
						isMovedLeft = false;
						isMovedRight = false;
						isMovedForward = false;
						isMovedBackward = false;

						isMoving = false;

						AnimatingMove(isMovedLeft, isMovedRight, isMovedForward, isMovedBackward);
					}
		}

		if (isMovedRight && !hurt && !hasAttacked && vulnerable)
			{
				// transform.Translate(Vector3.right * Time.deltaTime * speed);
				physics.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), (Input.GetAxisRaw("Vertical") * 0)) * Time.deltaTime * speed;
		} else if (isMovedLeft && !hurt && !hasAttacked) {
				// transform.Translate(Vector3.left * Time.deltaTime * speed);
				physics.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), (Input.GetAxisRaw("Vertical") * 0)) * Time.deltaTime * speed;
		} else if (isMovedBackward && !hurt && !hasAttacked) {
				// transform.Translate(Vector3.up * Time.deltaTime * speed);
				physics.velocity = new Vector2((Input.GetAxisRaw("Horizontal") * 0), Input.GetAxisRaw("Vertical")) * Time.deltaTime * speed;
		} else if (isMovedForward && !hurt && !hasAttacked) {
				// transform.Translate(Vector3.down * Time.deltaTime * speed);
				physics.velocity = new Vector2((Input.GetAxisRaw("Horizontal") * 0), Input.GetAxisRaw("Vertical")) * Time.deltaTime * speed;
		}

    if(!isMovedLeft && !isMovedRight && !isMovedForward && !isMovedBackward && !hurt && !hasAttacked && vulnerable && !hasSlid) {
			physics.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Time.deltaTime * speed;
      AnimatingIdle(isFacingLeft, isFacingRight, isFacingForward, isFacingBackward, hurt, hasSlid);
    }


		if (Input.GetKey(KeyCode.Space) && !isMoving && !hasAttacked && isFacingRight == true && !hurt && vulnerable) {
			isAttackingRight = true;
			hasAttacked = true;
			AnimatingAttack (isAttackingLeft, isAttackingRight, isAttackingForward, isAttackingBackward);
			attack.Play();
			Instantiate(tristitiaAttack, transform.position, Quaternion.identity);
		}	else if (Input.GetKey(KeyCode.Space) && !isMoving && !hasAttacked && isFacingLeft == true && !hurt && vulnerable) {
			isAttackingLeft = true;
			hasAttacked = true;
			AnimatingAttack (isAttackingLeft, isAttackingRight, isAttackingForward, isAttackingBackward);
			attack.Play();
			Instantiate(tristitiaAttack, transform.position, Quaternion.identity);
		} else if (Input.GetKey(KeyCode.Space) && !isMoving && !hasAttacked && isFacingForward == true && !hurt && vulnerable) {
			isAttackingForward = true;
			hasAttacked = true;
			AnimatingAttack (isAttackingLeft, isAttackingRight, isAttackingForward, isAttackingBackward);
			attack.Play();
			Instantiate(tristitiaAttack, transform.position, Quaternion.identity);
    } else if (Input.GetKey(KeyCode.Space) && !isMoving && !hasAttacked && isFacingBackward == true && !hurt && vulnerable) {
			isAttackingBackward = true;
			hasAttacked = true;
			AnimatingAttack (isAttackingLeft, isAttackingRight, isAttackingForward, isAttackingBackward);
			attack.Play();
			Instantiate(tristitiaAttack, transform.position, Quaternion.identity);
	  }

		if (Input.GetKey(KeyCode.Alpha0) && !hasSlid && !hasAttacked && isFacingRight == true && !hurt && vulnerable && !isMoving) {
			isSlidingRight = true;
			hasSlid = true;
		}	else if (Input.GetKey(KeyCode.Alpha0) && !hasSlid && !hasAttacked && isFacingLeft == true && !hurt && vulnerable && !isMoving) {
			isSlidingLeft = true;
			hasSlid = true;
		} else if (Input.GetKey(KeyCode.Alpha0) && !hasSlid && !hasAttacked && isFacingForward == true && !hurt && vulnerable && !isMoving) {
			isSlidingForward = true;
			hasSlid = true;
    } else if (Input.GetKey(KeyCode.Alpha0) && !hasSlid && !hasAttacked && isFacingBackward == true && !hurt && vulnerable && !isMoving) {
			isSlidingBackward = true;
			hasSlid = true;
	  }

		if (hasSlid) {
			isFacingLeft = false;
			isFacingRight = false;
			isFacingForward = false;
			isFacingBackward = false;
			if (isSlidingLeft) {
				AnimatingIdle(isFacingLeft, isFacingRight, isFacingForward, isFacingBackward, hurt, hasSlid);
				AnimatingSlide(isSlidingLeft, isSlidingRight, isSlidingForward, isSlidingBackward);
				physics.velocity = new Vector2(-1, 0) * Time.deltaTime * speed * 2;
				slide.Play();
			} else if (isSlidingRight) {
				AnimatingIdle(isFacingLeft, isFacingRight, isFacingForward, isFacingBackward, hurt, hasSlid);
				AnimatingSlide(isSlidingLeft, isSlidingRight, isSlidingForward, isSlidingBackward);
				physics.velocity = new Vector2(1, 0) * Time.deltaTime * speed * 2;
				slide.Play();
			} else if (isSlidingForward) {
				AnimatingIdle(isFacingLeft, isFacingRight, isFacingForward, isFacingBackward, hurt, hasSlid);
				AnimatingSlide(isSlidingLeft, isSlidingRight, isSlidingForward, isSlidingBackward);
				physics.velocity = new Vector2(0, -1) * Time.deltaTime * speed * 2;
				slide.Play();
			} else if (isSlidingBackward) {
				AnimatingIdle(isFacingLeft, isFacingRight, isFacingForward, isFacingBackward, hurt, hasSlid);
				AnimatingSlide(isSlidingLeft, isSlidingRight, isSlidingForward, isSlidingBackward);
				physics.velocity = new Vector2(0, 1) * Time.deltaTime * speed * 2;
				slide.Play();
			}
		}

		score = GetComponent<PlayerScoreTristitia>().score;
  }

	void AnimatingMove(bool isMovedLeft, bool isMovedRight, bool isMovedForward, bool isMovedBackward)	{
		anim.SetBool ("isMovedLeft", isMovedLeft);
		anim.SetBool ("isMovedRight", isMovedRight);
		anim.SetBool ("isMovedForward", isMovedForward);
		anim.SetBool ("isMovedBackward", isMovedBackward);
	}

  void AnimatingIdle(bool isFacingLeft, bool isFacingRight, bool isFacingForward, bool isFacingBackward, bool hurt, bool hasSlid) {
    anim.SetBool ("isFacingLeft", isFacingLeft);
    anim.SetBool ("isFacingRight", isFacingRight);
    anim.SetBool ("isFacingForward", isFacingForward);
    anim.SetBool ("isFacingBackward", isFacingBackward);
		anim.SetBool ("hurt", hurt);
		anim.SetBool("hasSlid", hasSlid);

  }

	void AnimatingAttack (bool isAttackingLeft, bool isAttackingRight, bool isAttackingForward, bool isAttackingBackward) {
		anim.SetBool ("isAttackingRight", isAttackingRight);
		anim.SetBool ("isAttackingLeft", isAttackingLeft);
    anim.SetBool ("isAttackingForward", isAttackingForward);
    anim.SetBool ("isAttackingBackward", isAttackingBackward);
	}

	void AnimatingSlide(bool isSlidingLeft, bool isSlidingRight, bool isSlidingForward, bool isSlidingBackward)	{
		anim.SetBool ("isSlidingLeft", isSlidingLeft);
		anim.SetBool ("isSlidingRight", isSlidingRight);
		anim.SetBool ("isSlidingForward", isSlidingForward);
		anim.SetBool ("isSlidingBackward", isSlidingBackward);
	}

	void AnimatingPain (bool hurt) {
		anim.SetBool ("hurt", hurt);
	}

	void checkAttack () {
		if (hasAttacked) {
			timer += Time.deltaTime;
			if (timer > attackWait) {
				isAttackingLeft = false;
				isAttackingRight = false;
	      isAttackingForward = false;
	      isAttackingBackward = false;
				AnimatingAttack (isAttackingRight, isAttackingLeft, isAttackingForward, isAttackingBackward);
				hasAttacked = false;
				timer = timer - attackWait;
			}
		}
	}

	void checkHarm () {
		if (hurt){
			harmTimer += Time.deltaTime;
			if (harmTimer > harmWait) {
				AnimatingPain(hurt);
				isFacingForward = true;
				harmTimer = harmTimer - harmWait;
				hurt = false;
				vulnerable = true;
			}
		}
	}

	void checkSlide() {
		if (hasSlid) {
			slideTimer += Time.deltaTime;
			if (slideTimer > slideWait) {
				hasSlid = false;
				if (isSlidingLeft) {
					isFacingLeft = true;
				} else if (isSlidingRight) {
					isFacingRight = true;
				} else if (isSlidingForward) {
					isFacingForward = true;
				} else if (isSlidingBackward) {
					isFacingBackward = true;
				}
				isSlidingLeft = false;
				isSlidingRight = false;
				isSlidingForward = false;
				isSlidingBackward = false;
				AnimatingSlide(isSlidingLeft, isSlidingRight, isSlidingForward, isSlidingBackward);
				AnimatingIdle(isFacingLeft, isFacingRight, isFacingForward, isFacingBackward, hurt, hasSlid);
				slideTimer = slideTimer - slideWait;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Nullos" || other.tag == "Risio Projectile" || other.tag == "Furia Projectile" || other.tag == "Dormio Projectile" || other.tag == "Dilectio Projectile" || other.tag == "Verecundia Projectile" || other.tag == "Invidia Projectile" || other.tag == "Mercuria Projectile") {
			if (vulnerable) {
				hurt = true;
			}
		}
	}

	void SpawnGemotes() {
		if (score >= 50) {
			// Generates one gemote (totalling 50 gemotes) at a random position near the player
			for (int i = 10; i > 0; i--) {
				Instantiate(fiveGemotes, new Vector3((transform.position.x + Random.Range(-0.32f, 0.32f)), ((transform.position.y - 0.16f) + Random.Range(-0.32f, 0.32f)), 0), Quaternion.identity);
			}
		} else if (score < 50) {
			for (int i = score; i > 0; i--) {
				Instantiate(oneGemote, new Vector3((transform.position.x + Random.Range(-0.32f, 0.32f)), ((transform.position.y - 0.16f) + Random.Range(-0.32f, 0.32f)), 0), Quaternion.identity);
			}
		}
	}
}
