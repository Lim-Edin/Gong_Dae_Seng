using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialogCtrl : MonoBehaviour {

	public TextBoxMgr TextBox;
	public Animator anim;
	public AnimatorStateInfo CurrentState;

	static int IdleState = Animator.StringToHash("Base Layer.Idle");

	void Start () {
		anim = GetComponent<Animator> ();
	}
	void Update(){
		CurrentState = anim.GetCurrentAnimatorStateInfo(0); 
	}

	public void StartCor(){
		StartCoroutine (animation());
	}
	IEnumerator animation (){//void->ienumerator
		int num=TextBox.currentLine;
		switch(num){
		case 1:
			anim.SetBool ("HAND", true);//안녕
			yield return new WaitForSeconds(2f);
			if (CurrentState.fullPathHash!=IdleState){
				if (!anim.IsInTransition (0)) {
					anim.SetBool ("HAND", false);
				}
			}
			break;
		case 11:
			anim.SetBool ("WINK", true);//찡긋
			yield return new WaitForSeconds(2f);
			if (CurrentState.fullPathHash!=IdleState){
				if (!anim.IsInTransition (0)) {
					anim.SetBool ("WINK", false);
				}
			}
			break;
		case 7:
			anim.SetBool ("NODE", true);
			yield return new WaitForSeconds(2f);
			if (CurrentState.fullPathHash!=IdleState){
				if (!anim.IsInTransition (0)) {
					anim.SetBool ("NODE", false);
				}
			}
			break;
		case 13: 
			anim.SetBool ("LOOK", true);//오열
			yield return new WaitForSeconds(2f);
			if (CurrentState.fullPathHash!=IdleState){
				if (!anim.IsInTransition (0)) {
					anim.SetBool ("LOOK", false);
				}
			}
			break;
		}
		yield return null;
		}
	}