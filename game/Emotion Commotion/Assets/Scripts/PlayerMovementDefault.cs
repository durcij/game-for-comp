using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementDefault : MonoBehaviour {

	public float speed;
	bool isMovedLeft = false;
	bool isMovedRight = false;
  bool isMovedForward = false;
  bool isMovedBackward = false;
  bool isAttackingLeft = false;
  bool isAttackingRight = false;
  bool isAttackingForward = false;
  bool isAttackingBackward = false;
  bool isFacingLeft = false;
  bool isFacingRight = false;
  bool isFacingForward = true;
  bool isFacingBackward = false;
  bool hasAttacked = false;
	bool isMoving = false;
	Animator anim;
  AudioSource attack;
	private int wait;
	public float attackWait = 0.5f;
	float timer;


	void Awake () {
		anim = GetComponent <Animator> ();
		attack = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {

		speed = 3.0f;

		checkAttack ();

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

		if (isMovedRight)
			{
				transform.Translate(Vector3.right * Time.deltaTime * speed);
		} else if (isMovedLeft) {
				transform.Translate(Vector3.left * Time.deltaTime * speed);
		} else if (isMovedBackward) {
				transform.Translate(Vector3.up * Time.deltaTime * speed);
		} else if (isMovedForward) {
				transform.Translate(Vector3.down * Time.deltaTime * speed);
		}

    if(!isMovedLeft && !isMovedRight && !isMovedForward && !isMovedBackward) {
        AnimatingIdle(isFacingLeft, isFacingRight, isFacingForward, isFacingBackward, isMovedLeft, isMovedRight, isMovedForward, isMovedBackward);
      }


		if (Input.GetKey(KeyCode.Space) && !isMoving && hasAttacked == false && isFacingRight == true) {
			isAttackingRight = true;
			hasAttacked = true;
			AnimatingAttack (isAttackingLeft, isAttackingRight, isAttackingForward, isAttackingBackward);
		}	else if (Input.GetKey(KeyCode.Space) && !isMoving && hasAttacked == false && isFacingLeft == true) {
			isAttackingLeft = true;
			hasAttacked = true;
			AnimatingAttack (isAttackingLeft, isAttackingRight, isAttackingForward, isAttackingBackward);
		} else if (Input.GetKey(KeyCode.Space) && !isMoving && hasAttacked == false && isFacingForward == true) {
			isAttackingForward = true;
			hasAttacked = true;
			AnimatingAttack (isAttackingLeft, isAttackingRight, isAttackingForward, isAttackingBackward);
    } else if (Input.GetKey(KeyCode.Space) && !isMoving && hasAttacked == false && isFacingBackward == true) {
			isAttackingBackward = true;
			hasAttacked = true;
			AnimatingAttack (isAttackingLeft, isAttackingRight, isAttackingForward, isAttackingBackward);
	  }
  }

	void AnimatingMove(bool isMovedLeft, bool isMovedRight, bool isMovedForward, bool isMovedBackward)	{
		anim.SetBool ("isMovedLeft", isMovedLeft);
		anim.SetBool ("isMovedRight", isMovedRight);
		anim.SetBool ("isMovedForward", isMovedForward);
		anim.SetBool ("isMovedBackward", isMovedBackward);
	}

  void AnimatingIdle(bool isFacingLeft, bool isFacingRight, bool isFacingForward, bool isFacingBackward, bool isMovedLeft, bool isMovedRight, bool isMovedForward, bool isMovedBackward) {
    anim.SetBool ("isFacingLeft", isFacingLeft);
    anim.SetBool ("isFacingRight", isFacingRight);
    anim.SetBool ("isFacingForward", isFacingForward);
    anim.SetBool ("isFacingBackward", isFacingBackward);
  }

	void AnimatingAttack (bool isAttackingLeft, bool isAttackingRight, bool isAttackingForward, bool isAttackingBackward) {
		anim.SetBool ("isAttackingRight", isAttackingRight);
		anim.SetBool ("isAttackingLeft", isAttackingLeft);
    anim.SetBool ("isAttackingForward", isAttackingForward);
    anim.SetBool ("isAttackingBackward", isAttackingBackward);
	}

	void checkAttack () {
		timer += Time.deltaTime;
		if (timer >= attackWait) {
			isAttackingLeft = false;
			isAttackingRight = false;
      isAttackingForward = false;
      isAttackingBackward = false;
			AnimatingAttack (isAttackingRight, isAttackingLeft, isAttackingForward, isAttackingBackward);
			hasAttacked = false;
			timer = 0f;
		}
	}
}
