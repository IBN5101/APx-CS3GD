using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
	// (IBN) Look, I know this is a dumb way to do it.
	// Definitely should have used something like a dictionary.
	// But a hack it is.

	// Overall progress
	// Hack version: dependant of LevelName Enum
	public static int level_progress = 1;

	// Default value
	public static int DEFAULT_TP = 9999;

	// Testing ground
	public static int level_testing = 460;
	// G01
	public static int level_0 = DEFAULT_TP;
	// F2_01
	public static int level_1 = DEFAULT_TP;
	// F2_02
	public static int level_2 = DEFAULT_TP;
	// F2_03
	public static int level_3 = DEFAULT_TP;
	// F2_04
	public static int level_4 = DEFAULT_TP;

	public enum LevelName
	{
		TESTING,
		G01,
		F2_01,
		F2_02,
		F2_03,
		F2_04,
	}

	public static void SetLevelScore(LevelName level, int amount)
	{
		switch (level)
		{
			case LevelName.TESTING:
				level_testing = amount;
				break;
			case LevelName.G01:
				level_0 = amount;
				break;
			case LevelName.F2_01:
				level_1 = amount;
				break;
			case LevelName.F2_02:
				level_2 = amount;
				break;
			case LevelName.F2_03:
				level_3 = amount;
				break;
			case LevelName.F2_04:
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
			case LevelName.G01:
				return level_0;
			case LevelName.F2_01:
				return level_1;
			case LevelName.F2_02:
				return level_2;
			case LevelName.F2_03:
				return level_3;
			case LevelName.F2_04:
				return level_4;
			default:
				return 0;
		}
	}

	public static int GetTotalScore()
	{
		// (IBN) Unholy
		int l0 = level_0 == DEFAULT_TP ? 0 : level_0;
		int l1 = level_1 == DEFAULT_TP ? 0 : level_1;
		int l2 = level_2 == DEFAULT_TP ? 0 : level_2;
		int l3 = level_3 == DEFAULT_TP ? 0 : level_3;
		int l4 = level_4 == DEFAULT_TP ? 0 : level_4;

		return l0 + l1 + l2 + l3 + l4;
	}

	public static string TPFormatting(int tp)
	{
		// Hours, Minutes, Seconds
		int tp_h = 0;
		int tp_m = 0;
		int tp_s = 0;

		// Lazy validation
		if (tp >= 3600 || tp < 0)
		{
			tp_h = 99; tp_m = 99; tp_s = 99;
		}
		// Minutes and Seconds
		else if (tp >= 60)
		{
			tp_m = Mathf.FloorToInt(tp / 60.0f);
			tp_s = tp - tp_m * 60;
		}
		// Seconds only
		else
		{
			tp_s = tp;
		}

		return tp_h.ToString("D2") + ":" + tp_m.ToString("D2") + ":" + tp_s.ToString("D2");
	}
}
