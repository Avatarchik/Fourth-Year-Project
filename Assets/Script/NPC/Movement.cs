﻿/// <summary>
/// Movement.cs
/// Author: Harris Kevin
/// 
/// moves npc arund the village
/// reads in the correct points in the correct village
/// changes targets once the point is close to the character
/// </summary>
using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	
	private Transform target;
	private int moveSpeed, rotationSpeed, maxDistance, numberOfPointsInMovementAllowance;
	private Transform myTransform;
	public int counter = 0;
	public string tagName;
	
	void Awake ()
	{
		myTransform = transform;	
		moveSpeed = 2;
		rotationSpeed = 3;
	}
	
	GameObject[] movementAllowance;
	// Use this for initialization
	void Start ()
	{
		movementAllowance = GameObject.FindGameObjectsWithTag (tagName);
		numberOfPointsInMovementAllowance = movementAllowance.Length - 1;
		for(int i = 0; i <= numberOfPointsInMovementAllowance; i++){
			if(movementAllowance[i].name == "MovementAllowance - "+ counter)
				target = movementAllowance [i].transform;
			if(movementAllowance[i].name == "MovementAllowance")
				target = movementAllowance [i].transform;
		}
		target = movementAllowance [counter].transform;
		maxDistance = 4;
	}
	
	int changeWalkingDirection = 0;
	// Update is called once per frame
	void Update ()
	{
		animation.Play ("walk");

		Quaternion rot;
		
		rot = Quaternion.Slerp (myTransform.rotation,
		                        Quaternion.LookRotation (target.position - myTransform.position), 
		                        rotationSpeed * Time.deltaTime);
		myTransform.rotation = rot;
		Vector3 tar = new Vector3 (target.position.x, myTransform.position.y, target.position.z);
		if (Vector3.Distance (target.position, myTransform.position) >= maxDistance) {
			myTransform.position += new Vector3 (myTransform.forward.x * moveSpeed * Time.deltaTime,
			                                     0, 
			                                     myTransform.forward.z * moveSpeed * Time.deltaTime);
			
		}
		if (Vector3.Distance (target.position, myTransform.position)<= maxDistance) {
			counter ++;
			if(counter >numberOfPointsInMovementAllowance){
				counter = 0;
			}
			for(int i = numberOfPointsInMovementAllowance; i >=0 ; i--){
				if(movementAllowance[i].name == "MovementAllowance - "+ counter)
					target = movementAllowance [i].transform;
				if(movementAllowance[i].name == "MovementAllowance")
					target = movementAllowance [i].transform;
			}


			
		}
	}

}
