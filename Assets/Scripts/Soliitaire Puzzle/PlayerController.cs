using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
    Text currentLevelText;

    private void Start()
    {
        //Load Data.
        if (PlayerPrefs.GetInt("Player Level") > 1)
            PlayerPrefs.GetInt("Player Level");
        currentLevelText = GameObject.Find("Current Level Text").GetComponent<Text>();

        UpdateCurrentLevelUI();
    }

    public void UpdateCurrentLevelUI()
    {
        if (PlayerPrefs.GetInt("Player Level") > 0)
            currentLevelText.text = "Level: " + PlayerPrefs.GetInt("Player Level").ToString();
        else currentLevelText.text = "Level: " + 1.ToString();
    }
}

[Serializable]
public class PlayerData
{
    public int _currentLevel;
}
