using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

/*--------------------------------------------------------------------------------------*/
/*																						*/
/*		AsteroidData: Holds, saves, and loads all data									*/
/*																						*/
/*			Functions:																	*/
/*					public AsteroidData();												*/																		
/*					public AsteroidData(string filename);								*/
/*					public AsteroidData(Vector3 position, Vector3 scale);				*/
/*					public JSONClass ToJSON();											*/
/* 					public JSONClass ToJSON(AsteroidData[] data);						*/
/*					public JSONClass ToJSON(JSONArray jsonArray);						*/
/*					public void SaveData(string filename);								*/
/*					public void SaveData(string filename, AsteroidData[] data);			*/
/*					public void SaveData(string filename, JSONArray jsonArray);			*/
/*					public void SaveData(string filename, JSONClass data);				*/
/*																						*/
/*--------------------------------------------------------------------------------------*/
public class AsteroidData 
{
	//	Public Const Variables
	public const int NUMBER_OF_ASTEROIDS = 4;								//	Number of asteroids to load
	public const string ASTEROID_TAG = "Asteroid";							//	Asteroid tag

	//	Public Variables
	public AsteroidData[] data = new AsteroidData[NUMBER_OF_ASTEROIDS];		//	Array to hold multiple Astorid data

	/*======================================================================================*/
	/*																						*/
	/*		Why I did it like this: 														*/
	/*																						*/
	/*		I couldn't figure out a way to save a JSONArray to a file. The function 		*/																		
	/*		WriteJSONtoFile takes in a JSONClass and I couldn't figure out a way to			*/
	/*		cast a JSONArray into a JSONClass. Alternatively I stored each position			*/
	/*		as a JSONNode and then all them upon initalizing. Pros, it works! Cons,			*/
	/*		you need to know how many asteroids you are loading each time.					*/
	/*																						*/
	/*======================================================================================*/

	public Vector3 position;										//	Position of Asteroid
	public Vector3 scale;											//	Scale of Asteroid

	// Private Const Variables
	private const string POS_X = "xPos";							//	Name of the x-posiiton JSONNode
	private const string POS_Y = "yPos";							//	Name of the y-posiiton JSONNode
	private const string POS_Z = "zPos";							//	Name of the z-posiiton JSONNode
	private const string SCALE_X = "xScale";						//	Name of the x-scale JSONNode
	private const string SCALE_Y = "yScale";						//	Name of the y-scale JSONNode
	private const string SCALE_Z = "zScale";						//	Name of the z-scale JSONNode

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*		AsteroidData: Creates an AsteroidData Object at the origin point.				*/
	/*																						*/
	/*			Param:		None															*/
	/*			Returns:	AsteroidData at the origin point with scale of 1,1,1			*/																		
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public AsteroidData()
	{
		position = Vector3.zero;
		scale = Vector3.one;
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*		AsteroidData: Creates an AsteroidData Object using data in file					*/
	/*																						*/
	/*			Param:		string filename - name of your file								*/
	/*			Returns:	AsteroidData at the position and scale indicated in file		*/																		
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public AsteroidData(string filename)
	{
		JSONNode jsonNode = UtilScript.ReadJSONFromFile(Application.dataPath, filename);
	
		//	Goes through each node and creates new asteroid data from the values given
		for (int i = 0; i < data.Length; i++)
		{
			position = new Vector3(jsonNode[POS_X + i].AsFloat, jsonNode[POS_Y + i].AsFloat, jsonNode[POS_Z + i].AsFloat);
			scale = new Vector3(jsonNode[SCALE_X + i].AsFloat, jsonNode[SCALE_Y + i].AsFloat, jsonNode[SCALE_Z + i].AsFloat);
			data[i] = new AsteroidData(position, scale);
		}
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*		AsteroidData: Creates an AsteroidData Object using position and scale given		*/
	/*																						*/
	/*			Param:		Vector3 position - position of asteroid							*/
	/*						Vector3 scale - scale of asteroid								*/
	/*			Returns:	AsteroidData at the position and scale given					*/																		
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public AsteroidData(Vector3 position, Vector3 scale)
	{
		this.position = position;
		this.scale = scale;
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*		ToJSON: Turns Vector3 data of position and scale into JSONCass					*/
	/*																						*/
	/*			Param:		None															*/
	/*			Returns:	JSONClass with positons and scales stored in floats				*/																		
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public JSONClass ToJSON()
	{
		JSONClass json = new JSONClass();

		json[POS_X].AsFloat = position.x;
		json[POS_Y].AsFloat = position.y;
		json[POS_Z].AsFloat = position.z;

		json[SCALE_X].AsFloat = scale.x;
		json[SCALE_Y].AsFloat = scale.y;
		json[SCALE_Z].AsFloat = scale.z;

		return json;
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*		ToJSON: Turns an AsteroidData array into a JSONCass								*/
	/*																						*/
	/*			Param:		AsteroidData[] data - the data to be stored						*/
	/*			Returns:	JSONClass with positons and scales stored in a string			*/																		
	/*																						*/
	/*		Note:	While this function allowed for good indexing, converting the 			*/
	/*				string back into a Vector3 would be annoyoing at best.					*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public JSONClass ToJSON(AsteroidData[] data)
	{
		JSONClass json = new JSONClass();

		for (int i = 0; i < data.Length; i++)
		{
			Vector3 pos = data[i].position;
			Vector3 scale = data[i].scale;

			data[i] = new AsteroidData(pos, scale);

			json[ASTEROID_TAG + i] = new JSONClass();
			json[ASTEROID_TAG + i]["Position"] = pos.ToString();
			json[ASTEROID_TAG + i]["Scale"] = scale.ToString();
		}

		return json;
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*		ToJSON: Turns an JSONArray into a JSONCass										*/
	/*																						*/
	/*			Param:		JSONArray jsonArray - the data to be stored						*/
	/*			Returns:	JSONClass with positons and scales stored in a JSONClass		*/																		
	/*																						*/
	/*		Note:	When turning a JSONArray into a JSONClass using this function, the		*/
	/*				data gets very messy.													*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public JSONClass ToJSON(JSONArray jsonArray)
	{
		JSONClass json = new JSONClass();

		for(int i = 0; i < jsonArray.Count; i++)
		{
			json["AsteroidData" + i] = jsonArray[i];
		}
		return json;
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*		SaveData: Saves data to disc													*/
	/*																						*/
	/*			Param:		string filename - name of file to be SaveData					*/
	/*			Returns:	void															*/																		
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public void SaveData(string filename)
	{
		JSONClass json = ToJSON();

		UtilScript.WriteJSONtoFile(Application.dataPath, filename, json);
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*		SaveData: Saves data to disc													*/
	/*																						*/
	/*			Param:		string filename - name of file to be SaveData					*/
	/*						AsteroidData[] data - data to be turned into a JSONClass		*/
	/*			Returns:	void															*/																		
	/*																						*/
	/*		Note:	I tried using the an Array of AsteroidData but it felt like cheating	*/
	/*				when I went to save a file												*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public void SaveData(string filename, AsteroidData[] data)
	{
		JSONClass json = ToJSON(data);

		UtilScript.WriteJSONtoFile(Application.dataPath, filename, json);
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*		SaveData: Saves data to disc													*/
	/*																						*/
	/*			Param:		string filename - name of file to be SaveData					*/
	/*						JSONArray jsonArray - data to be turned into a JSONClass 		*/
	/*			Returns:	void															*/																		
	/*																						*/
	/*		Note:	I tried using the JSONArray way of storing the data, but I couldn't		*/
	/*				cast the JSONArray into a JSONClass cleanly to allow for indxing		*/
	/*				when reading from the file.												*/
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public void SaveData(string filename, JSONArray jsonArray)
	{
		JSONClass json = ToJSON(jsonArray);
		UtilScript.WriteJSONtoFile(Application.dataPath, filename, json);
		//jsonArray.
	}

	/*--------------------------------------------------------------------------------------*/
	/*																						*/
	/*		SaveData: Saves data to disc													*/
	/*																						*/
	/*			Param:		string filename - name of file to be SaveData					*/
	/*						JSONClass data - data to be SaveData							*/
	/*			Returns:	void															*/																		
	/*																						*/
	/*--------------------------------------------------------------------------------------*/
	public void SaveData(string filename, JSONClass data)
	{
		UtilScript.WriteJSONtoFile(Application.dataPath, filename, data);
	}
}