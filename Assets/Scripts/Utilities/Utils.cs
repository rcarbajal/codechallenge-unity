using UnityEngine;

public class Utils
{
	public static int GetRandom(int min, int max)
	{
		return Mathf.RoundToInt(Random.value * ((float)max - (float)min) + (float)min);
	} //end method GetRandom
} //end class Utils