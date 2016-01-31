using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGame : MonoBehaviour {

	public GameObject endGame;

	public void PlayerWins(GameObject player) {
		endGame.GetComponent<Text> ().text = string.Format ("{0} Wins", player.name);
		endGame.SetActive (true);
	}
}
