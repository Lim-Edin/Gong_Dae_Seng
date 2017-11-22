using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState { //그냥 명칭들
	Move,
	Chase,
	Listen,//idle
}
public class FscoreCtrl : MonoBehaviour {

	public EnemyState ES;
	public Animator anim;
	public float MoveSpeed=1.2f;
	public float AttackSpeed=3f;
	public int damage=2;
	public float traceDist=8.0f;//추적거리
	public float attackDist=3.0f;//공격범위

	public Transform playerTr;
	public Transform tr;
	public Transform eyes;//적의 눈같은 추적
	public LifeBar lifeBar_S;
	private NavMeshAgent nvAgent;

	private AudioSource _audio;
	public AudioSource _bbip;
	//public AudioClip[] sounds;

	//public AudioClip[] footsounds;
	//private AudioSource sound;


	//public float waitTime = 1f;
	//public Transform[] WayPoints;
	//public EnemySight enemySight;
	//private LastPlayerSighting lastPlayerSighting;

	//private bool isDie=false;*/


	void Start(){
		tr = GetComponent<Transform> ();
		playerTr = GameObject.Find("Player").transform;
		anim = GetComponent<Animator> ();
		_audio = GetComponent<AudioSource> ();
		_bbip= GameObject.Find ("SoundObject(BBIP)").GetComponent<AudioSource> ();

		lifeBar_S = GameObject.Find ("PanelHpbar").GetComponent<LifeBar> ();
		nvAgent = this.gameObject.GetComponent<NavMeshAgent> ();
		ES = EnemyState.Listen;
		//sound = GetComponent<AudioSource> ();
		nvAgent.speed = 1.2f;
		anim.speed = 1.2f;
	}

	void Update(){
		//Debug.DrawLine (eyes.position, playerTr.position, Color.green);
		anim.SetFloat ("Velocity", nvAgent.velocity.magnitude);//속도크기. 스칼라라고 생각.

		if (ES == EnemyState.Listen) {
			Vector3 RandomPosition = Random.insideUnitSphere * 15f;
			NavMeshHit hit;//이라고 하면
			NavMesh.SamplePosition (tr.position + RandomPosition, out hit, 20f, NavMesh.AllAreas);
			/*
public static bool SamplePosition(Vector3 sourcePosition, out NavMeshHit hit, float maxDistance, int areaMask);

hit : 결과 위치의 프로퍼티를 저장.
maxDistance:	sourcePosition으로부터 이 거리로 샘플링.
			*/
			nvAgent.SetDestination (hit.position);
			ES = EnemyState.Move;
			anim.speed = 1.2f;
			//Debug.Log ("REMAIN" + nvAgent.remainingDistance.ToString ());
			//Debug.Log (nvAgent.stoppingDistance.ToString ());
		}
		if (ES == EnemyState.Move) {
			if (nvAgent.remainingDistance <= nvAgent.stoppingDistance) {
				//Debug.Log ("REMAIN" + nvAgent.remainingDistance.ToString ());
				//Debug.Log (nvAgent.stoppingDistance.ToString ());
				ES = EnemyState.Listen;
				anim.speed = 0.7f;
			}
		}
		if (ES == EnemyState.Chase) {
			nvAgent.destination = playerTr.position;
			if (Vector3.Distance (eyes.position, playerTr.position) > traceDist) {
				ES = EnemyState.Listen;
				_bbip.Stop ();

			}
		}
	}


		public void CheckIfCollides (){
		RaycastHit rayhit;
		if (Physics.Linecast (eyes.position, playerTr.position, out rayhit)) {
			//Debug.Log ("hit" + rayhit.collider.gameObject.name);
			ES=EnemyState.Chase;
			_audio.Play();
			_bbip.Play();
			nvAgent.speed=3.5f;
			anim.speed = 3.5f;
			//소리
		}
			//Distance ();
			//nvAgent.isStopped=true;
			/*
		} else if (ES == EnemyState.Move) {
			nvAgent.destination = playerTr.position;
			nvAgent.isStopped = false;
		}*/
	}
	/*
	public void footstep(int num){
		sound.clip = footsounds [num];
		sound.Play();
	}
	void Update(){
		anim.SetBool ("velocity", nvAgent.velocity.magnitude);
		nvAgent.SetDestination (playerTr.position);
		/*if (Vector3.Distance (playerTr.position, FscoreTr.position) < 10) {
			Vector3 direction = playerTr.position - FscoreTr.position;
			if (direction.magnitude > 5) {
				FscoreTr.Translate (0, 0, 0.5f);
			}
		}*/
	
	/*IEnumerator CheckMonsterState()
	{
		while (!isDie) {
			yield return new WaitForSeconds (0.2f);
			float dist = Vector3.Distance (playerTr.position, monsterTr.position);

			if (dist <= attackDist) {
				monsterState = MonsterState.attack;

			} else if (dist <= traceDist) {
				monsterState = MonsterState.trace;//추적

			} 
			else {
				monsterState = MonsterState.idle;
			}

		}

	}*/
	/*
	IEnumerator MonsterAction()
	{
		while (!isDie) {

			switch (monsterState) {

			case MonsterState.idle:
				nvAgent.Stop ();
				break;

			case MonsterState.trace:
				nvAgent.destination = playerTr.position;
				nvAgent.Resume ();
				break;
			case MonsterState.attack:
				break;
			}
			yield return null;
		}

	}*/

}
