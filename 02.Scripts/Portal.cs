//scene의 이동 총괄
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//go the the other scene

public class Portal : MonoBehaviour {
	public PlayerCtrl playerScript;
	private MenuCtrl MenuScript;//다음 씬으로 넘어가는 함수를 갖고온다.
	public ButtonTextUI BS;//공대생의 상태(statue)와 몇학년 몇학년인지 변수 갖고오기 위함

	private Canvas nextStageCanvas;
	private Canvas pauseCanvas;

	public Button startText;
	public Button quitText;
	public Button HyuHakText;
	public Button pauseText;
	public Button continueText;

	public GameObject sparkEffect;

	public float timeleft = 120;
	public int[,] _time = new int[4, 4];

	public bool open_portal=false;//if the stage is clear, player can move on to the portal in order to go to the other stages
	public bool HyuHak; //... 다음 신 넘어갈 때 창을 잠시 띄우는 Canvas 만들기 (모든 스테이지마다 복붙)
	//canvas에도 버튼이 있지. 여기서 휴학 선택할 것인지 여부를 보여줌(버튼)
	public bool start;//
	public bool start_time;//시간재기 시작

	void Awake(){
		start_time = true;
	}

	void Start(){
		nextStageCanvas = GameObject.FindWithTag ("NEXTSTAGE").GetComponent<Canvas> (); 
		pauseCanvas = GameObject.FindWithTag ("PAUSESTAGE").GetComponent<Canvas> (); 
		startText = GameObject.FindWithTag ("START").GetComponent<Button> ();
		quitText = GameObject.FindWithTag ("QUIT").GetComponent<Button> ();
		pauseText = GameObject.FindWithTag ("PAUSE").GetComponent<Button> ();
		continueText=GameObject.FindWithTag("CONTINUE").GetComponent<Button>();

		nextStageCanvas.enabled = false;//처음엔 invisible하게
		pauseCanvas.enabled=false;

		BS=GameObject.Find ("ButtonTextUI").GetComponent<ButtonTextUI>();
	}

	void Update(){
		if (open_portal==true){
			JumpPortal ();
			BS.Statue_f(2);
			start_time = false;
		}
		else {
			if (start_time == true) {
				timeleft -= Time.deltaTime;
			}
				/*if (timeleft <= 0) {//게임오버씬
				}*/
			}//게임이 끝날 때까지 시간 측정
		}

	void JumpPortal(){
		if (Input.GetButtonDown ("Jump")) {
			StartCoroutine ("openPotal");
		}
	}

	IEnumerator openPotal(){
		start_time = false;
		yield return new WaitForSeconds (1.5f);
		GameObject spark=(GameObject)Instantiate(sparkEffect,playerScript.tr.position,Quaternion.identity);//스파크 파티클 동적생성하고 변수에 할당
		//Quaternion은 써야하는 데 회전이 필요없을 경우 Quaternion.identity를 쓴다.
		Destroy (spark, spark.GetComponent<ParticleSystem>().main.duration + 0.1f);
		yield return new WaitForSeconds (1f);
		nextStageCanvas.enabled = true;
	}
	//--------------------------------------NextStageCanvas' Three Buttons-------------
	public void Hyuhak_f(){//휴학버튼
		HyuHak = true;
		BS.Statue_f(5);//상태를 휴학으로
		nextStageCanvas.enabled=false;
		Debug.Log ("허잉 휴학 Scene을 만들어야 겠구먼");
		//휴학 상태의 씬을 보여주는 걸 로드하기 SceneManager.LoadScene(6)이라던가
	}
	public void StartGame(){//이동
		//start=true;
		nextStageCanvas.enabled = false;
		start_time = true;
		if (BS.b == 1) {//1학기 //스테이지는 그대로. 다만 모든걸 리셋시킴
			BS.b = 2;
			//nextStageCanvas.enabled = false;
			SceneManager.LoadScene (BS.a);//다시 돌아감.
			//start = false;
		}
		if (BS.b == 2) {//2학기인 경우
			BS.a++;//다음 학년으로
			BS.b = 1;//1학기
			//nextStageCanvas.enabled = false;//다음 스테이지로 넘어가고 nextStageCanvas을 띄운다.
			SceneManager.LoadScene (BS.a);
		}
		//Test용 : SceneManager.LoadScene (2);
		//다음 씬으로 넘어간다. SceneManager.LoadScene (a+1);라던가! 만약 a==4이고 b==2이면 게임 오버인가요?
	}
	public void PauseGame(){
		pauseCanvas.enabled = true;
		start_time = false;
		//시간 멈추는 줄
	}
	public void ContinueGame(){
		pauseCanvas.enabled = false;
		start_time = true;
		//시간 시작
		//적군 움직임 멈춘다->idle로
		//
	}
	public void ExitGame(){//Quit->Yes//그 canvas도 띄울 것인지? if 다되면 
		Application.Quit();
	}
	//----------------------------------------------------------------------------------
}


