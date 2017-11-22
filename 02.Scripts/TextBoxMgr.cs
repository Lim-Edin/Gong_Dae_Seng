using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//to use 씬전환

public class TextBoxMgr : MonoBehaviour {

	public PlayerDialogCtrl PDC;
	public GameObject textBox;
	public Text theText;//화면
	public TextAsset textFile;
	public int currentLine;
	public int endAtLine;

	public string[] textLines;

	void Start(){
		theText=GameObject.Find ("Text").GetComponent<Text>();
		PDC = GameObject.Find ("Player").GetComponent<PlayerDialogCtrl> ();
		if (textFile != null) {
			textLines = (textFile.text.Split ('\n'));//엔터의 단위로 쪼개서 textLine이란 string형 배열에 넣음.
		}
		if (endAtLine == 0) {
			endAtLine = textLines.Length - 1;
		}
	}

	void Update(){
		StartCoroutine (TypeSentence (textLines[currentLine]));//아래에 하면 코루틴 2개가 충돌함! + 첫번째 문장 안나타남.
		//Debug.Log (textLines[currentLine].Length.ToString ());
		if (Input.GetKeyDown (KeyCode.Return)) {
			StopAllCoroutines();
			currentLine++;
			PDC.StartCor ();
		}
		if (currentLine>endAtLine){
			DisableTextBox ();
		}
	}
	IEnumerator TypeSentence (string sentence){
		theText.text = "";
		foreach (char letter in sentence.ToCharArray()) {
			theText.text += letter;
			yield return null;//한 프레임 간격의 속도만큼. (WaitForSeconds도 가능하지만)
		}
	}

	public void DisableTextBox(){
		textBox.SetActive(false);
		SceneManager.LoadScene (1);
	}
		
}
