using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinGame : MonoBehaviour {

	GameObject gameEnd;

	public void PlayerWins(GameObject player) {
		gameEnd.GetComponent<Text> ().text = string.Format ("{0} Wins", player.name);
		gameEnd.SetActive (true);
	}
}
