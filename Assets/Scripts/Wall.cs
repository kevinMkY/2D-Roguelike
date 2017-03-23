using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	public Sprite hurtSprite;
	public int wallHp = 4;
	private SpriteRenderer render;

	// Use this for initialization
	void Start () {
		render = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	public void WallGetDmg(int dmg){
		render.sprite = hurtSprite;
		wallHp --;
		if(wallHp <= 0){
			gameObject.SetActive(false);
		}
	}
}
