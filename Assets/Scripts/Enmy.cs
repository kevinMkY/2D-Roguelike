using UnityEngine;
using System.Collections;

public class Enmy : MovingObject {

	public int enmyDmg = 1;
	private Animator animator;
	private Transform target;
	private bool skip;

	public AudioClip enmyAttack1;
	public AudioClip enmyAttack2;

	// Use this for initialization
	public override void Start () {
		GameManager.instance.AddEnmy(this);
		animator = GetComponent<Animator> ();
//		target = GameObject.FindGameObjectWithTag("Player").transform;
		base.Start ();
	}
	
	public override void AttempMove<T> (int x, int y)
	{
		
		base.AttempMove<Player> (x, y);
	}

	void Update () {
	
	}

	public void MoveEnmy(){

		target = GameManager.instance.playerCurrentPosition;

		if (target == null) {
			return;
		}

		int x = 0;
		int y = 0;

		if (Mathf.Abs(transform.position.x - target.transform.position.x) >= 1) {
			x = transform.position.x > target.position.x ? -1 : 1;
		} else if (Mathf.Abs(transform.position.y  - target.transform.position.y) >= 1){
			y = transform.position.y > target.position.y ? -1 : 1;
		}
		AttempMove<Player> (x, y);
	}

	public override void CantMove<T> (T componet)
	{
		Player player = componet as Player;
		animator.SetTrigger ("enmyHit");
		player.GetHurt (enmyDmg);

		SoundManager.instance.PlayRandomClip (enmyAttack1, enmyAttack2);
	}
}
