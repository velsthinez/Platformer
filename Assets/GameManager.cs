using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int TotalCoins = 0;
    public int CurrentLevel = 1;
    
    public static GameManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = (GameManager) FindObjectOfType(typeof(GameManager ));
                
                if (m_Instance == null)
                {
                    GameObject go = new GameObject();
                    m_Instance = go.AddComponent<GameManager>();
                }
                DontDestroyOnLoad(m_Instance.gameObject);
            }
            return m_Instance;
        }
    }
    
    private static GameManager m_Instance = null;

    public void SaveGame()
    {
        PlayerPrefs.SetInt("CoinsCollected",TotalCoins);
        PlayerPrefs.SetInt("CurrentLevel",CurrentLevel);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        TotalCoins = PlayerPrefs.GetInt("CoinsCollected");
        CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
    }

    private void OnEnable()
    {
        LoadGame();
    }

    public void StartGame()
    {
        SaveGame();

        SceneManager.LoadScene(CurrentLevel);
    }

    public void GoToNextLevel()
    {
        if (CurrentLevel == 3)
        {
            CurrentLevel = 1;
            SaveGame();
            SceneManager.LoadScene(0);
            return;
        }

        CurrentLevel++;
        SaveGame();
        SceneManager.LoadScene(CurrentLevel);
    }
}
