using DataPersistenceProject;
using UnityEngine;
using UnityEngine.UI;

public class LocalDataManager : MonoBehaviour
{
    private string jsonDataFilePath;

    public InputField PlayerNameByDefaultTextBox;
    public static LocalDataManager Instance;
    public Text StartMenuBestScore;

    public int BestScore { get; set; }

    public string PlayerName { get; set; }

    public GameDataFile HighScoreData { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        jsonDataFilePath = $"{Application.persistentDataPath}/data.json";

        Instance = this;
        HighScoreData = new GameDataFile(jsonDataFilePath);
        HighScoreData.LoadData();
        StartMenuBestScore.text = $"Best Score : {HighScoreData.PlayerName} : {HighScoreData.BestScore}";
        PlayerNameByDefaultTextBox.text = HighScoreData.PlayerName;
        DontDestroyOnLoad(gameObject);
    }

    public bool TrySave()
    {
        if (BestScore > HighScoreData.BestScore)
        {
            HighScoreData.SaveData(this);
            return true;
        }

        return false;
    }
}
