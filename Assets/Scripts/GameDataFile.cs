using System;
using System.IO;
using UnityEngine;

namespace DataPersistenceProject
{
    public class GameDataFile
    {
        [Serializable]
        private class DataFormat
        {
            public int bestScore;

            public string playerName;
        }

        private readonly string jsonDataFilePath;

        public GameDataFile(string jsonDataFilePath)
        {
            this.jsonDataFilePath = jsonDataFilePath;
        }

        public int BestScore { get; private set; }

        public string PlayerName { get; private set; }

        public void LoadData()
        {
            if (File.Exists(jsonDataFilePath))
            {
                var savedJson = File.ReadAllText(jsonDataFilePath);
                var savedDataFromJson = JsonUtility.FromJson<DataFormat>(savedJson);

                BestScore = savedDataFromJson.bestScore;
                PlayerName = savedDataFromJson.playerName;
            }
        }

        public void SaveData(LocalDataManager dataManager)
        {
            BestScore = dataManager.BestScore;
            PlayerName = dataManager.PlayerName;

            var dataFormatForFile = new DataFormat
            {
                bestScore = BestScore,
                playerName = PlayerName
            };

            var dataJson = JsonUtility.ToJson(dataFormatForFile);

            File.WriteAllText(jsonDataFilePath, dataJson);
        }
    }
}
