using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
	// (IBN) Look, I know this is a dumb way to do it.
	// Definitely should have used something like a dictionary.
	// But a hack it is.

	// Testing ground
	public static int level_testing = 460;
	// G01
	public static int level_0 = 0;
	// F2_01
	public static int level_1 = 0;
	// F2_02
	public static int level_2 = 0;
	// F2_03
	public static int level_3 = 0;
	// F2_04
	public static int level_4 = 0;

	public enum LevelName
	{
		TESTING,
		LEVEL_0,
		LEVEL_1,
		LEVEL_2,
		LEVEL_3,
		LEVEL_4,
	}

	public static void SetLevelScore(LevelName level, int amount)
	{
		switch (level)
		{
			case LevelName.TESTING:
				level_testing = amount;
				break;
			case LevelName.LEVEL_0:
				level_0 = amount;
				break;
			case LevelName.LEVEL_1:
				level_1 = amount;
				break;
			case LevelName.LEVEL_2:
				level_2 = amount;
				break;
			case LevelName.LEVEL_3:
				level_3 = amount;
				break;
			case LevelName.LEVEL_4:
				level_4 = amount;
				break;
			default:
				break;
		}
	}

	public static int GetLevelScore(LevelName level)
	{
		switch (level)
		{
			case LevelName.TESTING:
				return level_testing;
			case LevelName.LEVEL_0:
				return level_0;
			case LevelName.LEVEL_1:
				return level_1;
			case LevelName.LEVEL_2:
				return level_2;
			case LevelName.LEVEL_3:
				return level_3;
			case LevelName.LEVEL_4:
				return level_4;
			default:
				return 0;
		}
	}
}
