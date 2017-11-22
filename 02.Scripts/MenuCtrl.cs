//씬넘어가는 함수구현 및 캐릭터 상태와 과목관리
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//to use 씬전환
[RequireComponent(typeof(AudioSource))]

public class MenuCtrl : MonoBehaviour {

	public Canvas quitMenu;
	public Button startText;
	public Button quitText;


	void Start(){
		quitMenu = quitMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		quitText = startText.GetComponent<Button> ();
		quitMenu.enabled = false;//처음엔 invisible하게
	}

	public void ExitPress(){//Quit
		quitMenu.enabled = true;
		quitText.enabled = false;
		startText.enabled = false;
		//quitText.enabled = false;
	}
	public void NoPress(){//Quit->No
		quitMenu.enabled = false;
		startText.enabled = true;
		quitText.enabled = true;
	}
	public void ExitGame(){//Quit->Yes
		Application.Quit();
	}
	public void Level(){//이동
		SceneManager.LoadScene (5);
	}
}
