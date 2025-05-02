using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MainMenu : MonoBehaviour {
    public GameObject menuPanel;
    public Text playerCoinsText;
    int playerCoins = 1000;

    void Start() {
        LoadPlayerData();
        UpdateUI();
    }

    void UpdateUI() {
        playerCoinsText.text = "Coins: " + playerCoins.ToString();
    }

    void LoadPlayerData() {
        string path = Application.persistentDataPath + "/playerdata.txt";
        if (File.Exists(path)) {
            string[] data = File.ReadAllLines(path);
            int.TryParse(data[0], out playerCoins);
        }
    }

    public void SavePlayerData() {
        string path = Application.persistentDataPath + "/playerdata.txt";
        File.WriteAllText(path, playerCoins.ToString());
    }
}
