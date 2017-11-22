//text띄울 스크립트 작성 및 플레이어 상태와 수강해야할 과목들 배열하기
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonTextUI : MonoBehaviour {

	public Text txtScore;//게임화면에 나오는 학점
	public Text Statue;//플레이어의 상태
	public Text timeLeft;//게임화면에 표시되는 남은 시간
	public Text lessons;//수강과목
	public Text ab;//a학년 b학기

	public Portal portalS;//스크립트
	public float totScore=0;//학점
	public double sum;

	public string[] _statue = { "개강", "재학", "종강", "방학", "계절학기", "휴학", "자퇴", "졸업" };
	public float[,] _score = new float[5,4];
	public int a;//n학년
	public int b;//m학기

	//public PlayerCtrl Script;


	void Start(){
		//totScore = PlayerPrefs.GetInt ("TOT_SCORE", 0);

		DispScore (0,0);
		Statue_f (1);

		a = SceneManager.GetActiveScene().buildIndex;//1학년
		b = 1;//1학기
		a_b (a, b);
	}
		
	void Update(){
		leftTime (portalS.timeleft);
	}

	public void DispScore(double score,int count){//상태
		sum += score;
		totScore=(float)sum/count;
		if (count != 0) {
			txtScore.text = "학점 : " + totScore.ToString ();
		}
		if (count == 7) {
			_score[a-1,b-1]=totScore;
		}
		//PlayerPrefs.SetInt ("TOT_SCORE", totScore);
	}
	public void Statue_f(int num)
	{
		Statue.text = "상태 : "+_statue[num];
	} 

	public void leftTime (float time){
		//time= portalS.timeleft;->화면 안나타므로 update함수쓴다.
		int itime = Mathf.FloorToInt (time);
		if (time > 30) {
			timeLeft.text = "Time : " + itime.ToString ();
		}
		if (time<= 30) {
		timeLeft.text = "Time : <color=#dd0000>" + itime.ToString ()+"</color>";
		}
	}
	public void a_b (int x,int y){
		ab.text = x.ToString() + "학년 " + y.ToString() + "학기";
	}
}
