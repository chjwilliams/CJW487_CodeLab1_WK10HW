using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*		GameManager: Manages the state and view of the "game"							*/
/*																						*/
/*			Functions:																	*/
/*					private void Start();												*/																		
/*					private void Update();												*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class GameManager : MonoBehaviour 
{
	//	Public Const Variables
	public const string ASTEROID_FILENAME = "AsteroidData.txt";		//	 Filename where data is located

	//	Public Variables
	public KeyCode saveKey = KeyCode.S;								//	Key used to save positions and scales
	public KeyCode loadKey = KeyCode.L;								//	Key used to load position and scales from file

	//	Private variables
	[SerializeField]												//	Allows us to see private fields in inspector
	private Loader myLoader;										//	Object responsible for loading data
	[SerializeField]												//	Allows us to see private fields in inspector
	private Saver mySaver;											//	Object responsible for saving data

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*		Start: Unity built function. Use this to initalize variables					*/
	/*																						*/
	/*			Param:		None															*/
	/*			Returns:	Void															*/																		
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	void Start () 
	{
		//	Initalizes myLoader
		myLoader = GetComponent<Loader>();
		

		//	Initalizes mySaver
		mySaver = GetComponent<Saver>();
		
	}

	void Update()
	{
		if(Input.GetKeyDown(loadKey))
		{
			//	Loads this the file
			myLoader.Load(ASTEROID_FILENAME);
		}

		if(Input.GetKeyDown(saveKey))
		{
			//	Saves the file after loading it
			mySaver.Save();
		}
	}
}