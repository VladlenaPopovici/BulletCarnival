using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace SaveSystem
{
    public class GameSavesManager
    {
        private const string FilePath = "/saveFile.json";
        private static readonly GameSavesManager Instance = new();

        private GameSavesManager()
        {
        }

        public static GameSavesManager GetInstance()
        {
            return Instance;
        }

        public void SaveWin()
        {
            Debug.Log("Saving win");
            SaveGame(true);
        }

        public void SaveLose()
        {
            Debug.Log("Saving lose");
            SaveGame(false);
        }

        public Totals GetWinsAndLoses()
        {
            var gameSummaries = LoadFromJson().data;
            var wins = gameSummaries.Count(x => x.isWin);
            var loses = gameSummaries.Count(x => !x.isWin);

            return new Totals()
            {
                Wins = wins,
                Loses = loses
            };
        }

        private static void SaveGame(bool isWin)
        {
            var loadFromJson = LoadFromJson();
            var newGameSummary = new GameSummary(isWin);

            loadFromJson.data.Add(newGameSummary);

            var json = JsonUtility.ToJson(loadFromJson, true);
            File.WriteAllText(Application.dataPath + FilePath, json);
        }

        private static ListOfGameSummary LoadFromJson()
        {
            CreateIfNone();
            
            var json = File.ReadAllText(Application.dataPath + FilePath);
            return JsonUtility.FromJson<ListOfGameSummary>(json) ?? new ListOfGameSummary();
        }

        private static void CreateIfNone()
        {
            if (!File.Exists(Application.dataPath + FilePath))
            {
                File.WriteAllText(Application.dataPath + FilePath, "{}");
            }
        }
    }
}