﻿/// <summary>
/// Player interaction.cs
/// Author: Harris Kevin
/// player and npc interaction
/// </summary>
using UnityEngine;
using System.Collections;

public class PlayerInteraction : MonoBehaviour {
	
	public bool playWalk = false;
	
	private int widthOfButton=200,
	heightOfButton = 50,
	buttonGroupWidth = 200,
	buttonGroupHeight = 230;
	public float counter ,optionCounter;
	public GUIStyle clearView,newV;
	public bool greetingCheck = false,paused = false;
	public string[] answers = new string[10];
	public string[] reply = new string[11];
	public Texture2D[] DPad = new Texture2D[2];
	public Texture2D[] buttonProperty = new Texture2D[4];
	public int dist=0;
	public int optionChosen;
	public int flag =0;
	public bool flagCheck=true;
	// Use this for initialization
	void Start () {
		counter =0;
		optionCounter=0;
		answers[0]="I'm Good. Thank You";
		answers[1]="What are you doing today?";
		answers[2]="Go Away.";
		answers[3]="Join me.";
		answers[4]="How's the weather?";
		answers[5]="Any information on the attack recently?";
		answers[6]="How are you today?";
		answers[7]="Enjoying your powers?";
		answers[8]="How's the new home?";
		answers[9]="I've nothing more to say.";

		reply[0]="";
		reply[1]="Happy to hear it.";
		reply[2]="Nothing really.";
		reply[3]="OK. No Need to be rude.";
		reply[4]="This is adventure is not for me.";
		reply[5]="Very hot with no rain.. As Always.";
		reply[6]="I heard your sister is missing.. I'm sorry.";
		reply[7]="I'm Ok. Wish the heat would hold up a bit.";
		reply[8]="They make life a bit easier but also a bit stressful.";
		reply[9]="A bit strange.";
		reply[10]="Ok. See you later.";
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKeyUp("joystick button 0"))&&(playWalk==true)&&(!paused)){
			paused = enableDisablePauseScreen();
		}
		if(paused){
			if(Input.GetKeyUp("joystick button 8")){
				if(dist > 0)
					dist--;
			}
			if(Input.GetKeyUp("joystick button 9")){
				if(dist<7)
					dist++;
			}
			
			if((Input.GetKeyUp("joystick button 0"))&&(optionCounter==0)){
				optionChosen = dist+1;
			}
			if((Input.GetKeyUp("joystick button 1"))&&(optionCounter==0)){
				optionChosen = dist+2;
			}
			if((Input.GetKeyUp("joystick button 3"))&&(optionCounter==0)){
				optionChosen = dist+3;
			}
			if(optionChosen >0){
				optionCounter+=0.1f;
				if(optionCounter >=10){
					if((optionChosen==3)||(optionChosen==10)){
						paused = false;
						counter=0;
						Time.timeScale = 1;
						playWalk=false;
						greetingCheck=false;
					}
					optionChosen=0;
					optionCounter=0;
				}
			}
		}
	}
	/// <summary>
	/// Raises the GU event.
	/// deals with displaying the interactions for the character to deal with
	/// </summary>
	void OnGUI(){
		string greet;
		if(playWalk){
			greet = "Press 0 to Talk";
			GUI.Label(new Rect(0,0, 300,30),greet);
			//greetingCheck=false;
		}
		
		clearView.alignment = TextAnchor.MiddleCenter;
		clearView.fontSize = 24;
		newV.alignment = TextAnchor.MiddleCenter;
		newV.fontSize = 24;


		if(paused){
			if((!greetingCheck)&&(counter<=10)&&(playWalk)){
				counter+=0.1f;
				greet = "Hello there. How are you?";
				
				GUI.enabled = false;
				GUI.Label(new Rect(((Screen.width/2)-(buttonGroupWidth/2)),
				                   ((Screen.height/2)-(buttonGroupHeight/2)),
				                   buttonGroupWidth, buttonGroupHeight),greet,clearView);
				if(counter>=10)
					greetingCheck=true;
			}
			clearView.alignment = TextAnchor.MiddleLeft;
			clearView.fontSize = 24;
			if((playWalk)&&(greetingCheck)&&(counter>=10)){
				buttonGroupWidth = 650;
				buttonGroupHeight = 300;
				//animation.Play("idle");
				greet ="";
				GUI.BeginGroup(new Rect(((Screen.width/2)-(buttonGroupWidth/3)),
				                        ((Screen.height/2)-(buttonGroupHeight/3)),
				                        buttonGroupWidth, buttonGroupHeight));
				//GUI.enabled =false;
				int height = buttonGroupHeight/6;
				int count =0;
				
				for(int cnt =0; cnt < answers.Length; cnt ++){
					GUI.enabled = false;
					if((cnt <3)&&((dist+cnt) <answers.Length)){
						GUI.Button(new Rect(0,count,buttonGroupWidth/1.5f,heightOfButton),answers[dist+cnt],newV);
						
					}

					count +=heightOfButton;
				}
				GUI.Label(new Rect(buttonGroupWidth/1.5f,0,buttonGroupWidth/3,height),buttonProperty[0],newV);
				GUI.Label(new Rect(buttonGroupWidth/1.5f,heightOfButton,buttonGroupWidth/3,height),buttonProperty[1],newV);
				GUI.Label(new Rect(buttonGroupWidth/1.5f,heightOfButton*2,buttonGroupWidth/3,height),buttonProperty[3],newV);

				GUI.Label(new Rect(0,heightOfButton*3,buttonGroupWidth,height),reply[optionChosen],newV);

				GUI.EndGroup();
				GUI.Label(new Rect(((Screen.width/2)-(buttonGroupWidth/3)-50),
				                   ((Screen.height/2)-(buttonGroupHeight/3)),
				                   50, 
				                   50),DPad[0],newV);
				GUI.Label(new Rect(((Screen.width/2)-(buttonGroupWidth/3)-50),
				                   ((Screen.height/2)-(buttonGroupHeight/3)+50),
				                   50, 
				                   50),DPad[1],newV);
				
			}
		}
	}
	/// <summary>
	/// Enables the disable pause screen.
	/// </summary>
	/// <returns><c>true</c>, if disable pause screen was enabled, <c>false</c> otherwise.</returns>
	bool enableDisablePauseScreen(){
		if(Time.timeScale ==0){
			Time.timeScale=1;
			return false;
		}else{
			Time.timeScale = 0;
			return true;
		}
	}
	
	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="playerInRange">Player in range.</param>
	void OnTriggerEnter (Collider playerInRange)
	{	
		if(playerInRange ==true){
			playWalk =true;
		}
	}
	/// <summary>
	/// Raises the trigger exit event.
	/// </summary>
	/// <param name="playerNotInRange">Player not in range.</param>
	void OnTriggerExit (Collider playerNotInRange)
	{
		if(playWalk ==true){
			playWalk =false;
		}
	}
}
