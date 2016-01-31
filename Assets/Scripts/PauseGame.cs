using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour {
	public GameObject pauseGame;

	public void Pause() {
		pauseGame.SetActive (true);
		Time.timeScale = 0;
	}

	public void Unpause() {
		pauseGame.SetActive (false);
		Time.timeScale = 1;
	}
}
