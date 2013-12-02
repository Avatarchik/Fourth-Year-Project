﻿using UnityEngine;
using System.Collections;
using System;

public class CharacterGen : MonoBehaviour {
	
	private float _screenHSize, _screenWsize;
	private PlayerInformation _playerInformation;
	public GUIStyle fontCharGenStyling;
	
	private const int STARTING_POINTS = 420;
	private const int MIN_STARTING_ATTRIBUTE_VALUE = 10;
	private const int STARTING_VALUE = 10;
	private int pointsLeft;
	private const int OFFSET = 5;
	private const int LINE_HEIGHT = 22;
	private const int STAT_LABEL_WIDTH = 160;
	private const int BASEVALUE_LABEL_WIDTH = 30;
	private const int OFFSET_FROM_BASEVALUE = OFFSET * 2 + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH * 2 + 32 * 2;
	private int statStartingPos = 400;
	
	
	public GameObject playerPrefab;
	
	// Use this for initialization
	void Start () {
		_screenHSize = Screen.height;
		_screenWsize = Screen.width;
		GUITexture GameSetting = FindObjectOfType (typeof(GUITexture)) as GUITexture;
		if (GameSetting.name == "Main Camera") {
			GameSetting.transform.position = Vector3.zero;
        	GameSetting.transform.localScale = Vector3.zero;
			GameSetting.pixelInset = new Rect (0,0, _screenWsize,_screenHSize);
		}
		
		GameObject player = Instantiate(playerPrefab,Vector3.zero, Quaternion.identity) as GameObject;

		player.name = "player";
		
		_playerInformation = new PlayerInformation();
		_playerInformation.Awake();
		
		
		for (int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
			_playerInformation.GetPrimaryAttribute (i).BaseValue = STARTING_VALUE;
			pointsLeft -= (STARTING_VALUE - MIN_STARTING_ATTRIBUTE_VALUE);
		}	
		
		_playerInformation.StatUpdate();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		fontCharGenStyling.fontSize = 20;
		
		DisplayAttributes();
		DisplayVitals();
		DisplayMana();
		DisplayDefence();
	}
	
	
	private void DisplayAttributes ()
	{
		for (int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
			GUI.Label (new Rect (OFFSET,
				statStartingPos + (i * LINE_HEIGHT),
				STAT_LABEL_WIDTH, 
				LINE_HEIGHT),
				((AttributeName)i).ToString (),fontCharGenStyling);
			
			GUI.Label (new Rect (STAT_LABEL_WIDTH + OFFSET,
				statStartingPos + (i * LINE_HEIGHT),
				BASEVALUE_LABEL_WIDTH, 
				LINE_HEIGHT),
				_playerInformation.GetPrimaryAttribute (i).AdjustedBaseValue.ToString (),fontCharGenStyling);
		}	
	}
	
	public void DisplayVitals ()
	{
		for (int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++) {
			GUI.Label (new Rect (OFFSET,
				statStartingPos + ((i + 7) * LINE_HEIGHT),
				STAT_LABEL_WIDTH,
				LINE_HEIGHT),
				((VitalName)i).ToString (),fontCharGenStyling);
			
			GUI.Label (new Rect (STAT_LABEL_WIDTH + OFFSET,
				statStartingPos + ((i + 7) * LINE_HEIGHT),
				BASEVALUE_LABEL_WIDTH,
				LINE_HEIGHT),
				_playerInformation.GetVitals (i).AdjustedBaseValue.ToString (),fontCharGenStyling);
			
		}
	}
	
	public void DisplayMana(){
		for(int i = 0; i < Enum.GetValues(typeof(ManaName)).Length;i++){
			GUI.Label(new Rect(OFFSET_FROM_BASEVALUE, 
				statStartingPos + (i * LINE_HEIGHT),
				STAT_LABEL_WIDTH, 
				LINE_HEIGHT),((ManaName)i).ToString(),fontCharGenStyling);
			GUI.Label(new Rect(OFFSET_FROM_BASEVALUE + STAT_LABEL_WIDTH,
				statStartingPos + (i * LINE_HEIGHT), 
				BASEVALUE_LABEL_WIDTH,
				LINE_HEIGHT),_playerInformation.GetMana(i).AdjustedBaseValue.ToString(),fontCharGenStyling);
		}
	}
	
	public void DisplayAttack(){
		for(int i = 0; i < Enum.GetValues(typeof(AttackName)).Length;i++){
			GUI.Label(new Rect(OFFSET_FROM_BASEVALUE, 
				statStartingPos + ((i+7) * LINE_HEIGHT),
				STAT_LABEL_WIDTH, 
				LINE_HEIGHT),((AttackName)i).ToString(),fontCharGenStyling);
			
			GUI.Label(new Rect(OFFSET_FROM_BASEVALUE + STAT_LABEL_WIDTH,
				statStartingPos + ((i+7) * LINE_HEIGHT), 
				BASEVALUE_LABEL_WIDTH,
				LINE_HEIGHT),_playerInformation.GetAttack(i).AdjustedBaseValue.ToString(),fontCharGenStyling);
		}
	}
	
	
	public void DisplayDefence()
	{
		for (int i = 0; i < Enum.GetValues(typeof(DefenceName)).Length; i++) {
			
			GUI.Label (new Rect (OFFSET_FROM_BASEVALUE*2,
				statStartingPos + (i * LINE_HEIGHT),
				STAT_LABEL_WIDTH,
				LINE_HEIGHT),
				((DefenceName)i).ToString (),fontCharGenStyling);
			
			GUI.Label (new Rect ((OFFSET_FROM_BASEVALUE*2) + STAT_LABEL_WIDTH,
				statStartingPos + (i * LINE_HEIGHT),
				BASEVALUE_LABEL_WIDTH,
				LINE_HEIGHT),
				_playerInformation.GetDefence (i).AdjustedBaseValue.ToString (),fontCharGenStyling);
			
		}
	}
	
	
}