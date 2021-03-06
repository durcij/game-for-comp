using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementDefault : MonoBehaviour {

	private float speed;
	bool isMovedLeft = false;
	bool isMovedRight = false;
  bool isMovedForward = false;
  bool isMovedBackward = false;
  public bool isAttackingLeft = false;
  public bool isAttackingRight = false;
  public bool isAttackingForward = false;
  public bool isAttackingBackward = false;
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
	public GameObject risioAttack;
	public GameObject oneGemote;
	public GameObject fiveGemotes;
	int score;
	AudioSource pain;
	bool vulnerable;

	public string fireButton = "Fire1";
	public string altButton = "Fire2";

	public string horizontalControl = "Horizontal";
	public string verticalControl = "Vertical";

	void Awake () {
		anim = GetComponent <Animator> ();
		attack = GetComponent<AudioSource> ();
		physics = GetComponent<Rigidbody2D> ();
		timer = 0.0f;
		harmTimer = 0.0f;
		attackWait = 0.5f;
		harmWait = 0.5f;
		speed = 120.0f;
		score = GetComponent<PlayerScoreDefault>().score;
		pain = GameObject.Find("RisioHurtBox").GetComponent<AudioSource>();
		vulnerable = true;
	}

	// Update is called once per frame
	void Update () {

		checkAttack ();
		checkHarm ();

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
			AnimatingPain(hurt);
			AnimatingMove(isMovedLeft, isMovedRight, isMovedForward, isMovedBackward);
			AnimatingIdle(isFacingLeft, isFacingRight, isFacingForward, isFacingBackward, isMovedLeft, isMovedRight, isMovedForward, isMovedBackward, hurt);
			pain.Play();
			SpawnGemotes();
		}

		if (!hasAttacked && !isAttackingLeft && !isAttackingRight && !isAttackingForward && !isAttackingBackward && !hurt && vulnerable){
			if ((Input.GetAxisRaw(horizontalControl) < 0) && (!isMoving || isMovedLeft)) // Move left
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
				} else if ((Input.GetAxisRaw(horizontalControl) > 0) && (!isMoving || isMovedRight)) // Move right
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
				} else if ((Input.GetAxisRaw(verticalControl) < 0) && (!isMoving || isMovedForward)) // Move forward
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
	  		} else if ((Input.GetAxisRaw(verticalControl) > 0) && (!isMoving || isMovedBackward)) // Move backward
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
				physics.velocity = new Vector2(Input.GetAxisRaw(horizontalControl), (Input.GetAxisRaw(verticalControl) * 0)) * Time.deltaTime * speed;
		} else if (isMovedLeft && !hurt && !hasAttacked) {
				// transform.Translate(Vector3.left * Time.deltaTime * speed);
				physics.velocity = new Vector2(Input.GetAxisRaw(horizontalControl), (Input.GetAxisRaw(verticalControl) * 0)) * Time.deltaTime * speed;
		} else if (isMovedBackward && !hurt && !hasAttacked) {
				// transform.Translate(Vector3.up * Time.deltaTime * speed);
				physics.velocity = new Vector2((Input.GetAxisRaw(horizontalControl) * 0), Input.GetAxisRaw(verticalControl)) * Time.deltaTime * speed;
		} else if (isMovedForward && !hurt && !hasAttacked) {
				// transform.Translate(Vector3.down * Time.deltaTime * speed);
				physics.velocity = new Vector2((Input.GetAxisRaw(horizontalControl) * 0), Input.GetAxisRaw(verticalControl)) * Time.deltaTime * speed;
		}

    if(!isMovedLeft && !isMovedRight && !isMovedForward && !isMovedBackward && !hurt && !hasAttacked && vulnerable) {
			physics.velocity = new Vector2(Input.GetAxisRaw(horizontalControl), Input.GetAxisRaw(verticalControl)) * Time.deltaTime * speed;
      AnimatingIdle(isFacingLeft, isFacingRight, isFacingForward, isFacingBackward, isMovedLeft, isMovedRight, isMovedForward, isMovedBackward, hurt);
    }


		if (Input.GetButtonDown(fireButton) && !isMoving && !hasAttacked && isFacingRight == true && !hurt && vulnerable) {
			isAttackingRight = true;
			hasAttacked = true;
			AnimatingAttack (isAttackingLeft, isAttackingRight, isAttackingForward, isAttackingBackward);
			attack.Play();
			Instantiate(risioAttack, transform.position, Quaternion.identity);
		}	else if (Input.GetButtonDown(fireButton) && !isMoving && !hasAttacked && isFacingLeft == true && !hurt && vulnerable) {
			isAttackingLeft = true;
			hasAttacked = true;
			AnimatingAttack (isAttackingLeft, isAttackingRight, isAttackingForward, isAttackingBackward);
			attack.Play();
			Instantiate(risioAttack, transform.position, Quaternion.identity);
		} else if (Input.GetButtonDown(fireButton) && !isMoving && !hasAttacked && isFacingForward == true && !hurt && vulnerable) {
			isAttackingForward = true;
			hasAttacked = true;
			AnimatingAttack (isAttackingLeft, isAttackingRight, isAttackingForward, isAttackingBackward);
			attack.Play();
			Instantiate(risioAttack, transform.position, Quaternion.identity);
    } else if (Input.GetButtonDown(fireButton) && !isMoving && !hasAttacked && isFacingBackward == true && !hurt && vulnerable) {
			isAttackingBackward = true;
			hasAttacked = true;
			AnimatingAttack (isAttackingLeft, isAttackingRight, isAttackingForward, isAttackingBackward);
			attack.Play();
			Instantiate(risioAttack, transform.position, Quaternion.identity);
	  }
		score = GetComponent<PlayerScoreDefault>().score;
  }

	void AnimatingMove(bool isMovedLeft, bool isMovedRight, bool isMovedForward, bool isMovedBackward)	{
		anim.SetBool ("isMovedLeft", isMovedLeft);
		anim.SetBool ("isMovedRight", isMovedRight);
		anim.SetBool ("isMovedForward", isMovedForward);
		anim.SetBool ("isMovedBackward", isMovedBackward);
	}

  void AnimatingIdle(bool isFacingLeft, bool isFacingRight, bool isFacingForward, bool isFacingBackward, bool isMovedLeft, bool isMovedRight, bool isMovedForward, bool isMovedBackward, bool hurt) {
    anim.SetBool ("isFacingLeft", isFacingLeft);
    anim.SetBool ("isFacingRight", isFacingRight);
    anim.SetBool ("isFacingForward", isFacingForward);
    anim.SetBool ("isFacingBackward", isFacingBackward);
		anim.SetBool ("hurt", hurt);

  }

	void AnimatingAttack (bool isAttackingLeft, bool isAttackingRight, bool isAttackingForward, bool isAttackingBackward) {
		anim.SetBool ("isAttackingRight", isAttackingRight);
		anim.SetBool ("isAttackingLeft", isAttackingLeft);
    anim.SetBool ("isAttackingForward", isAttackingForward);
    anim.SetBool ("isAttackingBackward", isAttackingBackward);
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

	void OnTriggerEnter2D (Collider2D other){
		if (other.tag == "Nullos" || other.tag == "Risio Projectile" || other.tag == "Furia Projectile" || other.tag == "Tristitia Projectile" || other.tag == "Dormio Projectile" || other.tag == "Dilectio Projectile" || other.tag == "Verecundia Projectile" || other.tag == "Invidia Projectile" || other.tag == "Mercuria Projectile") {
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
