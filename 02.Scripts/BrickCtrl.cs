using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickCtrl : MonoBehaviour {

	public Transform tr;
	public Animator brickanim;
	private AnimatorStateInfo CurrentState;
	static int BrickState = Animator.StringToHash("Base Layer.Brick");

	void Start(){
		tr = GetComponent<Transform> ();
		brickanim = GetComponent<Animator> (); 
	}

	void OnCollisionEnter(Collision coll)
	{
		if (coll.gameObject.tag == "Player") {
			brickanim.SetBool ("Up", true);
		}
	}
	/*
	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "Player") {
			brickanim.SetBool ("Up", true);
		}
	}*/
	void FixedUpdate(){
		CurrentState = brickanim.GetCurrentAnimatorStateInfo(0); 
		if (CurrentState.fullPathHash == BrickState) {
			if (!brickanim.IsInTransition (0)) {
				brickanim.SetBool ("Up", false);
			}
		}
	}
}
