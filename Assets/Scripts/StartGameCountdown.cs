using UnityEngine;
using System.Collections;

public class StartGameCountdown : MonoBehaviour {

	public float m_enableTextDelay = 1;

	public Player player1;
	public PlayerMovement playerMovement1;

	public Player player2;
	public PlayerMovement playerMovement2;

	public Player player3;
	public PlayerMovement playerMovement3;

	public Player player4;
	public PlayerMovement playerMovement4;

	public GameObject three;
	public GameObject two;
	public GameObject one;
	public GameObject fight;

	void Awake ()
	{
		player1.enabled = false;
		playerMovement1.enabled = false;

		player2.enabled = false;
		playerMovement2.enabled = false;

		player3.enabled = false;
		playerMovement3.enabled = false;

		player4.enabled = false;
		playerMovement4.enabled = false;

		StartCoroutine (StartGameDelay ());
	}

	IEnumerator StartGameDelay ()
	{
		three.SetActive (true);
		yield return new WaitForSeconds (m_enableTextDelay);
		three.SetActive (false);
		two.SetActive (true);
		yield return new WaitForSeconds (m_enableTextDelay);
		two.SetActive (false);
		one.SetActive (true);
		yield return new WaitForSeconds (m_enableTextDelay);
		one.SetActive (false);
		fight.SetActive (true);

		EnablePlayers ();

		yield return new WaitForSeconds (m_enableTextDelay);
		fight.SetActive (false);
	}

	void EnablePlayers ()
	{
        EnablePlayer(player1, playerMovement1);
        EnablePlayer(player2, playerMovement2);
        EnablePlayer(player3, playerMovement3);
        EnablePlayer(player4, playerMovement4);
	}

    private void EnablePlayer(Player player, PlayerMovement playerMovement)
    {
        if (player == null) return;

        player.enabled = true;
        playerMovement.enabled = true;

    }
}
