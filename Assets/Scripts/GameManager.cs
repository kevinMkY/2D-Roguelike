using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public BoardManager boardScript;
	[HideInInspector] public bool playersTurn = true;
	[HideInInspector] public bool enmysTurn = false;
	private int level = 3;
	private List<Enmy> enmies;
	public Transform playerCurrentPosition;

	// Use this for initializatio
	void Awake () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);

		enmies = new List<Enmy>();
		boardScript = GetComponent<BoardManager>();
		boardScript.SetupScence(level);
		
	}

	void Update(){
		
		if (playersTurn || enmysTurn) {
			return;
		}

		StartCoroutine (MoveEnmy());
	}

	public void AddEnmy(Enmy enmy){
		enmies.Add(enmy);
	}

	IEnumerator MoveEnmy(){
		enmysTurn = true;
		yield return new WaitForSeconds (0.1f);

		if (enmies.Count == 0) {
			yield return new WaitForSeconds (0.1f);
		}

		for(int i = 0; i < enmies.Count ; i++){
			enmies [i].MoveEnmy ();
			yield return new WaitForSeconds (0.1f);
		}

		enmysTurn = false;
		playersTurn = true;
	}
}
