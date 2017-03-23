using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	public class Count {

		public int maxNum;
		public int minNum;

		public Count(int minN,int maxN)
		{
			maxNum = maxN;
			minNum = minN;
		}
	}

	public int row = 8;
	public int column = 8;
	public Count wallCount = new Count(1,8);
	public Count foodCount = new Count(1,5);

	public GameObject[] floorTiles;
	public GameObject[] otherWallTiles;
	public GameObject[] wallTiles;
	public GameObject[] foodTiles;
	public GameObject[] enmyTiles;
	public GameObject 	exit;

	private Transform boardHolder;
	private List <Vector3> gridlist = new List<Vector3>();

	//初始化一个区域,该区域内可放入内墙,怪物,食物
	void InitPositionList(){
		for(int x = 1; x < row-1;x++){
			for(int y = 1; y < column-1;y++){
				gridlist.Add (new Vector3 (x, y, 0));
			}
		}
	}
	//
	Vector3 RandomPosition(){
		int ranIdx = Random.Range (0, gridlist.Count);
		Vector3 position = gridlist [ranIdx];
		gridlist.RemoveAt (ranIdx);
		return position;
	}

	//初始化一个区域,该区域内放入外墙及地板
	void InitObjects(){

		boardHolder = new GameObject ("Board").transform;

		for (int x = -1; x < row + 1; x++) {
			for (int y = -1; y < column + 1; y++) {
				GameObject obj = floorTiles[Random.Range(0,floorTiles.Length)];
				if(x == -1 || x == row || y == -1 || y == column){
					obj = otherWallTiles[Random.Range(0,otherWallTiles.Length)];
				}
				GameObject instance = Instantiate (obj,new Vector3(x,y,0),Quaternion.identity) as GameObject;
				instance.transform.SetParent(boardHolder);
			}
		}
	}

	void LayoutRightPosition(GameObject[] tiles,int minNumber,int maxNumber){

		int ranIdx = Random.Range(minNumber, maxNumber);

		for (int i = 0; i < ranIdx; i++) {
			Vector3 position = RandomPosition ();
			GameObject obj = tiles [i];
			Instantiate (obj, position, Quaternion.identity);
		}
	}

	public void SetupScence(int level){
		InitPositionList ();
		InitObjects ();
		LayoutRightPosition (wallTiles, wallTiles.Length, wallTiles.Length);
		LayoutRightPosition (foodTiles, foodTiles.Length, foodTiles.Length);

		int enmyCount = (int)Math.Log (level, 2.0f);
		LayoutRightPosition (enmyTiles,enmyCount,enmyCount);

		Instantiate (exit, new Vector3 (row - 1, column - 1), Quaternion.identity);
	}

}
