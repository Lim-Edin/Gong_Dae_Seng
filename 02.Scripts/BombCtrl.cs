using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class BombCtrl : MonoBehaviour {
	//public GameObject sparkEffect;
	public GameObject expEffect;
	private Transform tr;
	private AudioSource _audio;//폭발음
	private AudioSource _audio_;
	private AudioSource _bip;
	private AudioSource _bomb;
	//public AudioClip[] _audio_;//비명
	public LifeBar lifeBar_S;
	//폭발 효과 파티클 연결 변수

	void Awake(){
		lifeBar_S = GameObject.Find ("PanelHpbar").GetComponent<LifeBar> ();
	}
	void Start(){
		tr=GetComponent<Transform>();
		_audio= this.GetComponent<AudioSource> ();
		_audio_= GameObject.Find ("SoundObject(AA)").GetComponent<AudioSource> ();//유니티5부터 audioclip이 안된다.. <-음원파일을 배열처럼 두는 거
		_bip= GameObject.Find ("SoundObject(BIP)").GetComponent<AudioSource> ();//그래서 GameObject만들고 Prefab도 만듦
		_bomb= GameObject.Find ("SoundObject(BOMB)").GetComponent<AudioSource> ();
	}

	void OnTriggerEnter(Collider coll){//충돌 시 발생하는 콜백 함수(callback function)
		if (coll.gameObject.tag=="Player"){
			_audio.Play();
			_bip.Play ();
			StartCoroutine ("BombExp");
		}
	}
	IEnumerator BombExp(){
		yield return new WaitForSeconds (3f);
		Instantiate(expEffect,tr.position,Quaternion.identity);
		Destroy(gameObject,0.2f);
		_bomb.Play ();

		Collider[] colls=Physics.OverlapSphere(tr.position,7.0f);//폭탄 주위로 collder컴포넌트를 갖고 있는 게임오브젝트가 3초안에 아직도 반경 7.0f안에 있다? 없애버려^-^
		foreach (Collider coll in colls) {//배열의 멤버들 탐색,, 사실 player만 있는지 확인만 하면 되지만 문제는 배열이 있어야 주변에 탐색이 가능한 것 같다.
			if (coll.gameObject.tag == "Player") {
				_audio_.Play();
				if (lifeBar_S) { //nullreference오류잡기용
					lifeBar_S.hp -= 40;
					_audio_.Play();
				} else {
					Debug.Log ("Oh no");
				}
			}
		}
		StopCor();
	}
	public void StopCor(){
		StopCoroutine ("BombExp");
	}
}
