using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*		Loader: The component responsible for loading. Used by the GameManager			*/
/*																						*/
/*			Functions:																	*/
/*					public void Load(string filename);									*/																		
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class Loader : MonoBehaviour
{
	public const string ASTEROID_TAG = "Asteroid";				//	Tag for the Asteroid

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*		Load: Saves data for all objects with the ASTEROID_TAG							*/
	/*																						*/
	/*			Param:		string filename - name of the file you want to load. 			*/
	/*							Include file extension. ex: myLoader.Load(example.txt); 	*/
	/*			Returns:	Void															*/																		
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public void Load(string filename)
	{
		//	Loads asteroirds based on file name
		AsteroidData loadAsteroidData = new AsteroidData(filename);

		//	Creates an asteroid for each peice of data we have in loadAsteroidData
		for(int i = 0; i < loadAsteroidData.data.Length; i++)
		{
			//	Creates asteroird GameObject
			GameObject newAsteroird = GameObject.CreatePrimitive(PrimitiveType.Sphere);

			//	Tags the asteroird with the Asteroid tag
			newAsteroird.gameObject.tag = ASTEROID_TAG;

			//	Adjusts the newly created asteroird's scale
			newAsteroird.transform.localScale = loadAsteroidData.data[i].scale;

			//	Sets the newly created asteroird's position
			newAsteroird.transform.position = loadAsteroidData.data[i].position;
		}
	}
}