//ABOUT coin 생성과 효과음
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class CoinSpawner : MonoBehaviour {

	public GameObject CoinPrefab;
	public ButtonTextUI BS;
	//public float interval;
	public int maxCoin=7;
	//private PlayerCtrl Script;//다른 스크립트에서 변수 갖고 오기 위함
	//public int count=0;
	//public float range = 3.0f;


	void Start(){
		BS=GameObject.Find ("ButtonTextUI").GetComponent<ButtonTextUI>();
		for (int i = 0; i < maxCoin; i++) { //동적 생성. 굳이 코루틴 돌 필요가 없어보인다.
			CreateCoin ();
		}

	}
	void CreateCoin(){
		if (BS.a == 2) {
			transform.position = new Vector3 (Random.Range (-18f, 21f), transform.position.y, Random.Range (-30f, 10f));
			Instantiate (CoinPrefab, transform.position, transform.rotation);
		}
		/*
		if (BS.a == 2) {//노을진 똥꼬발랄 브금의 스테이지?
			transform.position = new Vector3 (Random.Range (-80f, 65f), transform.position.y, Random.Range (-60f, 10f));
			Instantiate (CoinPrefab, transform.position, transform.rotation);
		}
		if (BS.a == 3) {
			transform.position = new Vector3 (Random.Range (-18f, 21f), transform.position.y, Random.Range (-30f, 10f));
			Instantiate (CoinPrefab, transform.position, transform.rotation);
		}*/
	}

}