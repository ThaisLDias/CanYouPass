﻿using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	
	public float speed = 1f;
	public float jump = 0;
	public bool hasKey = false;
	public GameObject c;

	#region AnimationBools

	bool animWalking;
	bool dead;
	int animJump;

	#endregion

	Animator anim;
	Rigidbody2D rb;


	
	void Start(){
		
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}

	void AnimUpdate(){

		anim.SetBool ("Moving" , animWalking);
		anim.SetBool("Dead" , dead);
		anim.SetInteger("JumpState" , animJump);

	}
	
	void Update () 
	{
		animWalking = false;

		if (Input.GetKey (KeyCode.D)) {
			if (Application.loadedLevel != 3) {
				transform.Translate (Vector3.right * speed * Time.deltaTime);
			} else {
				transform.Translate (Vector3.left * speed * Time.deltaTime);
			}

			if(Application.loadedLevel != 9)
				animWalking = true;
		}
		
		
		if (Input.GetKey (KeyCode.A)) {
			if (Application.loadedLevel != 3) {
				transform.Translate (Vector3.left * speed * Time.deltaTime);
			} else {
				transform.Translate (Vector3.right * speed * Time.deltaTime);
			}
			if(Application.loadedLevel != 9)
				animWalking = true;
		}

		if (Input.GetKey (KeyCode.W) && Application.loadedLevel == 9) {
			transform.Translate (Vector3.up * speed * Time.deltaTime);
		} else if (Input.GetKey (KeyCode.S) && Application.loadedLevel == 9) {
			transform.Translate (Vector3.down * speed * Time.deltaTime);
		}
		

		if (Input.GetKey (KeyCode.W) && jump <= 0 && Application.loadedLevel != 9) {
			rb.velocity = new Vector3 (0, 20, 0);
			jump += 1;
			animJump += 1;
		}

		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
			
		} 

		
	

		AnimUpdate ();
    }

	void OnMouseDrag() {
		if (Application.loadedLevel == 7) {
			Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			gameObject.transform.position = position;

		}
	}



	void OnCollisionEnter2D (Collision2D colisor)
	{

		if (colisor.gameObject.tag == "Triangle") {

			Application.LoadLevel(Application.loadedLevel);
            PlayerPrefs.SetInt("mortes", PlayerPrefs.GetInt("mortes") + 1);
			StartCoroutine(wait());
		} 



		if (colisor.gameObject.tag != "MainCamera" && colisor.gameObject.tag != "Key")
		{
			jump = 0;
			animJump = 0;
		}
	}

	IEnumerator wait() 
	{
		yield return new WaitForSeconds(0.9f);
		Application.LoadLevel(Application.loadedLevel);
	} 


	void OnApplicationQuit() 
	{
		//PlayerPrefs.DeleteAll ();
	} 
}
