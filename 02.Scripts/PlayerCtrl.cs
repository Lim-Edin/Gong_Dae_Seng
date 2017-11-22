using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof(AudioSource))]
//it's difficult to write 주석 in Korean using MonoDevelop;;;;;->English
//study from Youtube (Official Unity) -> how to scripting
//using animation not LEGACY but MECANIM (7.11~7.13)
//have a difficulty in controlling player -> make it smooth
//couldn't use character controller.... Just use capsuleCollider&rigidbody...T_T
//[System.Serializable]
public class PlayerCtrl : MonoBehaviour {
	
	public float animSpeed = 1.5f;			
	public float forwardSpeed = 7.0f;
	public float backwardSpeed = 2.0f;
	private float rotSpeed =1.5f;
	//private float jumpPower = 3.0f; //oh my gooooooooodness
	public bool alive=true;

	private bool useCurve = true;	//for changing 			
	private float useCurveHeight = 0.5f;

	private float collHeight;
	private Vector3 collCenter;

	private ButtonTextUI textUI;
	public Portal portal_S;//script for using the variable start_time
	//public bool open_portal=false;//portal script랑 같이 쓰이는 변수. 다음 scene으로 넘어갈 수 있게 하는 권한
	public LifeBar lifeBar_S;

	public int Count=0;
	public bool End = false;
	//public AudioClip[] _audio;
	private AudioSource _audio;
	public GameObject sparkEffect;


	//private float h=0.0f;
	//private float v=0.0f;
	public Transform tr;//최적화하기 위해 update함수 전에 선언하지 않을 것임!
	//public float lookSmoother = 3.0f;	

	private Animator anim;
	private AnimatorStateInfo CurrentState;//current
	private CapsuleCollider coll;
	private Rigidbody rigid;
	private Vector3 velocity;

	//smooth for camera->should i add for this? thinking

	static int WalkRunState = Animator.StringToHash("Base Layer.WalkRun");//blend tree~~
	static int JumpState = Animator.StringToHash("Base Layer.Jump");//when running
	static int IdleState = Animator.StringToHash("Base Layer.Idle");//when standing
	static int GreetState = Animator.StringToHash("Base Layer.Greet");//when resting
	static int Jump2State = Animator.StringToHash("Base Layer.Jump2");
	static int DeadState=Animator.StringToHash("Base Layer.Dead");

	//public RandomRange RR;

	//Gets Component! in Start Function
	void Start(){
		
		tr = GetComponent<Transform> ();
		anim = GetComponent<Animator> ();
		coll = GetComponent<CapsuleCollider>();	
		rigid = GetComponent<Rigidbody>();
		collHeight = coll.height;
		collCenter = coll.center;
		_audio = GetComponent<AudioSource> ();
		textUI= GameObject.Find ("ButtonTextUI").GetComponent<ButtonTextUI>();
		lifeBar_S = GameObject.Find ("PanelHpbar").GetComponent<LifeBar> ();
		anim.SetBool ("Dead", false);//죽기전에만 다른 애니메이션을 사용할 수 있음.
		//start_time = portal_S.GetComponent<Canvas> ();
	}


	//about Coin (Get Scores + Effects and so on...)
	void OnTriggerEnter(Collider coll){
		if (coll.gameObject.tag == "COIN") {
			_audio.Play();
			GameObject spark=(GameObject)Instantiate(sparkEffect,coll.transform.position,Quaternion.identity);//스파크 파티클 동적생성하고 변수에 할당
			Destroy (spark, spark.GetComponent<ParticleSystem>().main.duration + 0.1f);
			Destroy (coll.gameObject);
			Count++;

			//int number = Random.Range (0, 9);
			//double sscore = sscore [number];
			//double sscore=Random.Range(1.7,4.3);
			//double sscore = RR(1.7,4.3);
			//float sscore = Random.Range(1.7,4.3);
			textUI.DispScore (3.6,Count);

			if (Count>=7){
				End = true;//제대로 실행되는지 확인할 것.
			}
		}
		if (coll.gameObject.tag == "ENEMY") {
			coll.transform.parent.GetComponent<FscoreCtrl> ().CheckIfCollides ();
			lifeBar_S.hp -= 5;
		}

		if (coll.gameObject.tag == "PORTAL") {
			if (End == true) {//수강을 전부 해서 다음 스테이지로 넘어갈 수 있게 하는 권한 부여
				portal_S.open_portal = true;
			}
			//스테이지 숫자올라감
			//다음 스테이지(scene)으로
		} else {
			portal_S.open_portal = false;
		}
	}


	void Update(){
		CurrentState = anim.GetCurrentAnimatorStateInfo(0); //current
	}

		
	//물리적 움직임
	void FixedUpdate ()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		anim.SetFloat ("Speed", v);
		anim.SetFloat ("Direction", h);
		anim.speed = animSpeed;	
		rigid.useGravity = true;

		velocity = new Vector3 (0, 0, v);//상하 키 입력에서 Z축 방향의 이동량을 얻음
		velocity = tr.TransformDirection (velocity);//캐릭터의 로컬 공간에서의 방향으로 변환한다. tr은 앞서 선언함!
		//다음 v 임계 값은 Mecanim 측의 전환과 함께 조정함

		//movement!!!!!!!!!!!
		if (v > 0.1) {
			velocity *= forwardSpeed;// 이동속도 곱
		} else if (v < -0.1) {
			velocity *= backwardSpeed;
		}

		//hp
		if (lifeBar_S.hp<=0){
			anim.SetBool ("Dead", true);
			//Input.anyKeyDown=false;
			portal_S.start_time = false;
			//게임오버 화면 띄우기
		}

		if (Input.GetButtonDown ("Jump")) {	
			if (CurrentState.fullPathHash == WalkRunState) {
				if (!anim.IsInTransition (0)) {//if animation is finished!! 겨우 알았다 이런 방법이 있다는 걸 from Unity Q&A
					//rigid.AddForce (Vector3.up * jumpPower, ForceMode.VelocityChange);
					anim.SetBool ("Jump", true);
				}
			
			}
		}
		tr.localPosition += velocity * Time.fixedDeltaTime; //사실 translation함수를 써도됨
		transform.Rotate (0, h * rotSpeed, 0);//좌우움직일때 회전!

		if (CurrentState.fullPathHash == WalkRunState) {
			if (useCurve) {
				resetColl ();
			}
		}
		else if (CurrentState.fullPathHash == JumpState) {
			if (!anim.IsInTransition (0)) {
				if (useCurve) {
					float jumpHeight = anim.GetFloat ("JumpHeight");
					float gravityCtrl = anim.GetFloat ("GravityCtrl"); 

					if (gravityCtrl > 0)
						rigid.useGravity = false;	

					Ray ray = new Ray (transform.position + Vector3.up, -Vector3.up);
					RaycastHit hitInfo = new RaycastHit ();

					if (Physics.Raycast (ray, out hitInfo)) {
						if (hitInfo.distance > useCurveHeight) {
							coll.height = collHeight - jumpHeight;			
							float adjCenterY = collCenter.y + jumpHeight;
							coll.center = new Vector3 (0, adjCenterY, 0);	
						}
						else {					
							resetColl ();

						}
					}
				}		
				anim.SetBool ("Jump", false);
			}
		}
		else if (CurrentState.fullPathHash == IdleState) {
			if (useCurve) {
				resetColl ();
			}
			if (Input.GetButtonDown ("Jump")) {
				anim.SetBool ("Greet", true);
			}
		}
		else if (CurrentState.fullPathHash == GreetState) {
			if (!anim.IsInTransition (0)) {
				anim.SetBool ("Greet", false);
			}
		}


	}

	void resetColl ()//collider의 높이와 중심값을 리셋
	{
		coll.height = collHeight;
		coll.center = collCenter;
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "TOMB") {
			//lifeBar_S.hp -= 10;
			anim.SetBool ("Collide", true);
		} else {
			anim.SetBool ("Collide", false);
		}
		if (CurrentState.fullPathHash == Jump2State) {
			if (!anim.IsInTransition (0)) {
				anim.SetBool ("Jump", false);
				anim.SetBool ("Collide", false);
			}
		}
	}
}