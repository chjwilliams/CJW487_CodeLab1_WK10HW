using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*		Saver: The component responsible for saving. Used by the GameManager			*/
/*																						*/
/*			Functions:																	*/
/*					public void Save();													*/																		
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class Saver : MonoBehaviour 
{
	//	Public Const Variables
	public const string ASTEROID_TAG = "Asteroid";					//	Asteroid Tag
	public const string ASTEROID_FILENAME = "AsteroidData.txt";		//	Name of file to save
	
	//	Public variables
	public GameObject[] asteroids;									//	Array of asteroid GameObjects
	public AsteroidData[] asteroid;									//	Array for soon to be collected asteroid data

	//	Private Const Variables
	private const string POS_X = "xPos";							//	Name for x-posiiton JSONNode
	private const string POS_Y = "yPos";							//	Name for y-posiiton JSONNode
	private const string POS_Z = "zPos";							//	Name for z-posiiton JSONNode
	private const string SCALE_X = "xScale";						//	Name for x-scale JSONNode
	private const string SCALE_Y = "yScale";						//	Name for y-scale JSONNode
	private const string SCALE_Z = "zScale";						//	Name for z-scale JSONNode

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*		Save: Saves data for all objects with the ASTEROID_TAG							*/
	/*																						*/
	/*			Param:		None															*/
	/*			Returns:	Void															*/																		
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public void Save () 
	{
		//	Finds all GameObjects with the "Asteroid" tag
		asteroids = GameObject.FindGameObjectsWithTag(ASTEROID_TAG);
		//	We'll use this to store the data we have
		AsteroidData dataStorer = new AsteroidData();

		//	Initializes the array of asteroirds
		asteroid = new AsteroidData[asteroids.Length];

		//	The json class we'll use to store our data
		JSONClass data = new JSONClass();

		//	Iterates through the asteroids array and pulls its position and scale
		for (int i = 0; i < asteroids.Length; i++)
		{
			Vector3 pos = asteroids[i].transform.position;
			Vector3 scale = asteroids[i].transform.localScale;

			asteroid[i] = new AsteroidData(pos, scale);

			//	Stores the position and scale in nodes as floats specified by the index
			data[POS_X + i].AsFloat = pos.x;
			data[POS_Y + i].AsFloat = pos.y;
			data[POS_Z + i].AsFloat = pos.z;
			data[SCALE_X + i].AsFloat = scale.x;
			data[SCALE_Y + i].AsFloat = scale.y;
			data[SCALE_Z + i].AsFloat = scale.z;
		}

		//	Saves the data
		dataStorer.SaveData(ASTEROID_FILENAME, data);
	}
}
