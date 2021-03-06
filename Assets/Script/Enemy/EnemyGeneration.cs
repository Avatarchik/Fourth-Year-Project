﻿/// <summary>
/// Enemy generation.cs
/// Author: Harris Kevin
/// generates correct enemy and the number of them onto the battle field and into the main game
/// also makes sure that if a character is loaded back into the game scence that enemies aren't 
/// to close for a looping battle simulation to occurr
/// </summary>
using UnityEngine;
using System.Collections;

public class EnemyGeneration : MonoBehaviour {

	GameObject[] Enmy;
	public GameObject[] enemylisting;
	GameObject[] positions;
	GameObject ObjPlacement;

	// Use this for initialization
	void Start () {
		if(Application.loadedLevelName=="Game")
			Enmy = GameObject.FindGameObjectsWithTag("Enemy");
		if(Application.loadedLevelName=="Battle Simulation")
			Enmy = GameObject.FindGameObjectsWithTag("EnemyBattle");
		Generate();
	}

	void Generate(){
		positions = new GameObject[Enmy.Length];
		int enemyNumb;
		if(Application.loadedLevelName=="Game"){
			GameObject constVar = GameObject.FindGameObjectWithTag("Constant");
			StoredInformation stored = constVar.GetComponent<StoredInformation>();

			for( int cnt = 0; cnt < Enmy.Length; cnt ++){
				enemyNumb = Random.Range(0,3);
				if(Vector3.Distance(stored.positionOnScreen,Enmy[cnt].transform.position)>20){
					Enmy[cnt] = Instantiate(enemylisting[enemyNumb],Enmy[cnt].transform.position, Quaternion.identity) as GameObject;

				}else{
					Destroy(Enmy[cnt]);
				}
			}
		}
		if(Application.loadedLevelName=="Battle Simulation"){
			
			GameObject constVar = GameObject.FindGameObjectWithTag("Constant");
			StoredInformation stored = constVar.GetComponent<StoredInformation>();
			int EnemyAmount = Random.Range(1,3);

			for( int cnt = 0; cnt < Enmy.Length; cnt ++){
				enemyNumb = stored.enemyTypeNumber;

				if(cnt <= EnemyAmount)
					Enmy[cnt] = Instantiate(enemylisting[enemyNumb],Enmy[cnt].transform.position, Quaternion.AngleAxis(210,new Vector3(0,1,0))) as GameObject;
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

}
