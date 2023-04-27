using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BayatGames.SaveGameFree;

public class SaveLoadGame : MonoBehaviour
{
	public enum SAVE_SLOT
	{
		SAVE01,
		SAVE02,
		SAVE03,
	}

	private static string slot1Name = "SAVE_01";
	private static string slot2Name = "SAVE_02";
	private static string slot3Name = "SAVE_03";

	public class SaveData
	{
		public int LevelProgress;
		public int Level_0;
		public int Level_1;
		public int Level_2;
		public int Level_3;
		public int Level_4;
		public int Total;
	}

	public static void SaveCurrent()
	{
		SaveData currentSaveData = new SaveData();
		currentSaveData.LevelProgress = LevelData.Level_progress;
		currentSaveData.Level_0 = LevelData.GetLevelScore(LevelData.LevelName.G01);
		currentSaveData.Level_1 = LevelData.GetLevelScore(LevelData.LevelName.F2_01);
		currentSaveData.Level_2 = LevelData.GetLevelScore(LevelData.LevelName.F2_02);
		currentSaveData.Level_3 = LevelData.GetLevelScore(LevelData.LevelName.F2_03);
		currentSaveData.Level_4 = LevelData.GetLevelScore(LevelData.LevelName.F2_04);
		currentSaveData.Total = LevelData.GetTotalScore();

		SaveGame.Save<SaveData>(SaveEnumToString(LevelData.CurrentSaveSlot), currentSaveData);
	}

	public static SaveData LoadSlot(SAVE_SLOT slot)
	{
		if (SaveGame.Exists(SaveEnumToString(slot)))
			return SaveGame.Load<SaveData>(SaveEnumToString(slot));
		else 
			return null;
	}

	public static void LoadIntoGame(SAVE_SLOT slot)
	{
		
		if (SaveGame.Exists(SaveEnumToString(slot)))
		{
			SaveData currentSaveData = SaveGame.Load<SaveData>(SaveEnumToString(slot));
			LevelData.CurrentSaveSlot = slot;

			LevelData.Level_progress = currentSaveData.LevelProgress;
			LevelData.SetLevelScore(LevelData.LevelName.G01, currentSaveData.Level_0);
			LevelData.SetLevelScore(LevelData.LevelName.F2_01, currentSaveData.Level_1);
			LevelData.SetLevelScore(LevelData.LevelName.F2_02, currentSaveData.Level_2);
			LevelData.SetLevelScore(LevelData.LevelName.F2_03, currentSaveData.Level_3);
			LevelData.SetLevelScore(LevelData.LevelName.F2_04, currentSaveData.Level_4);
		}
		else
		{
			LevelData.CurrentSaveSlot = slot;
		}
	}

	public static void DeleteSave(SAVE_SLOT slot)
	{
		SaveGame.Delete(SaveEnumToString(slot));
	}

	// (IBN) Deeply concerned
	private static string SaveEnumToString(SAVE_SLOT slot)
	{
		switch (slot)
		{
			case SAVE_SLOT.SAVE01:
				return slot1Name;
			case SAVE_SLOT.SAVE02:
				return slot2Name;
			case SAVE_SLOT.SAVE03:
				return slot3Name;
			default:
				return null;
		}
	}
}
