using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour {
	
	public int hp;
	[SerializeField]//private여도 Inspector뷰에서 보이게할 수 있음
	private int initHp=100; //초기값
	[SerializeField]
	private Image imageHpBar;
	public ButtonTextUI BS;

	void Start () {
		BS=GameObject.Find ("ButtonTextUI").GetComponent<ButtonTextUI>();
		hp = initHp;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		HandleBar ();
	}
	private void HandleBar(){//tag가 어떤거고 어떤거일 때마다 switch문 하기..!
		imageHpBar.fillAmount = (float)hp/(float)initHp;
		if (hp <= 0) {
			BS.Statue_f (6);
			//소리나 죽는 애니메이션 추가
		 //게임오버... 씬을 따로 만들지...? 효과음 넣어야 겟지.
		 //캐릭터는 멘탈 바사삭으로 자퇴를 했습니다.

		}
	}
}
