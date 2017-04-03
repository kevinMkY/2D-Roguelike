using UnityEngine;
using System.Collections;

public class Player : MovingObject {

	public int playerDmg = 1;
	public int foodCrease = 10;
	public int sodaCrease = 20;
	private Animator animator;
	public float currentFood;

	public AudioClip playerMove1;
	public AudioClip playerMove2;
	public AudioClip eatClip1;
	public AudioClip eatClip2;
	public AudioClip drinkClip1;
	public AudioClip drinkClip2;
	public AudioClip playerDie;


	// Use this for initialization
	public override void Start () {
		animator = GetComponent<Animator> ();
		currentFood = 100;
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {

		if (!GameManager.instance.playersTurn) {
			return;
		}

		int v = 0;
		int h = 0;

		h = (int)Input.GetAxis ("Horizontal");
		v = (int)Input.GetAxis ("Vertical");

//		print ("h:" + h +"v: " + v);

		if (h != 0 && v != 0) {
			v = 0;
		}

		if ((h == 0 && v != 0) || (h !=0 && v == 0)) {
			AttempMove<Wall> (h, v);
		}
	}

	public override void CantMove<T> (T componet)
	{
		Wall wall = componet as Wall;
		wall.WallGetDmg (playerDmg);
		animator.SetTrigger ("playerHit");

	}

	public override void AttempMove<T> (int x, int y)
	{
		base.AttempMove<T> (x, y);
		GameManager.instance.playersTurn = false;
		GameManager.instance.playerCurrentPosition = transform;

		SoundManager.instance.PlayRandomClip (playerMove1,playerMove2);
	}

	public void GetHurt(int dmg){
	
		animator.SetTrigger ("PlayerChop");

	}
}
