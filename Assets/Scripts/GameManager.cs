using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Prince prince;
    private float totalShots;
    private GameObject[] totalEnemies;
    private GameObject[] enemies;
    private Player player;
    private Button nextLevelButton;
    private Button restartButton;
    private GameObject mainMenu;
    private Button chooseLevelButton;
    private Button chooseLevelButtonFromMenu;
    private Button quitGameFromMenu;
    private Button saveGameFromMenu;
    private Button loadGameFromMenu;
    private GameObject growIcon;
    private GameObject dashIcon;
    private GameObject gravityIcon;
    public static int level = 0;
    private bool mainMenuOn = false;
    private GameObject playerGO;
    private AudioSource audioSource;
    private Enemy enemyScript;
    private static int temporaryLevel = 0;
    private static bool currentlyReplaying = false;
    [SerializeField] private bool dashAbility = false;
    [SerializeField] private bool growAbility = false;
    [SerializeField] private bool gravityAbility = false;

    void Start()
    {
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalShots = totalEnemies.Length;
        playerGO = GameObject.Find("Player");
        audioSource = GetComponent<AudioSource>();

        nextLevelButton = GameObject.Find("NextLevel")?.GetComponent<Button>();
        if(nextLevelButton != null)
        {
            nextLevelButton.onClick.AddListener(LevelFromGame);
            nextLevelButton.gameObject.SetActive(false);
        }

        restartButton = GameObject.Find("Restart")?.GetComponent<Button>();
        if(restartButton != null)
        {
            restartButton.onClick.AddListener(LevelFromGame);
            restartButton.gameObject.SetActive(false);
        }

        chooseLevelButton = GameObject.Find("ChooseLevel")?.GetComponent<Button>();
        if(chooseLevelButton != null)
        {
            chooseLevelButton.onClick.AddListener(BackToChooseLevel);
            chooseLevelButton.gameObject.SetActive(false);
        }

        chooseLevelButtonFromMenu = GameObject.Find("ChooseLevelFromMenu")?.GetComponent<Button>();
        if(chooseLevelButtonFromMenu != null)
        {
            chooseLevelButtonFromMenu.onClick.AddListener(BackToChooseLevel);
        }

        quitGameFromMenu = GameObject.Find("QuitFromMenu")?.GetComponent<Button>();
        if(quitGameFromMenu != null)
        {
            quitGameFromMenu.onClick.AddListener(Quit);
        }
        
        saveGameFromMenu = GameObject.Find("SaveGameFromMenu")?.GetComponent<Button>();
        if(saveGameFromMenu != null)
        {
            saveGameFromMenu.onClick.AddListener(SaveGame);
        }
        
        loadGameFromMenu = GameObject.Find("LoadGameFromMenu")?.GetComponent<Button>();
        if(loadGameFromMenu != null)
        {
            loadGameFromMenu.onClick.AddListener(LoadGame);
        }
        
        mainMenu = GameObject.Find("MainMenu");
        if(mainMenu != null)
        {
            mainMenu.SetActive(false);
        }

        gravityIcon = GameObject.Find("GravitySpecialIcon");
        dashIcon = GameObject.Find("DashSpecialIcon");
        growIcon = GameObject.Find("GrowSpecialIcon");
        if(gravityAbility)
        {
            dashIcon?.SetActive(false);
            growIcon?.SetActive(false);
        }
        else if (dashAbility)
        {
            gravityIcon?.SetActive(false);
            growIcon?.SetActive(false);
        }
        else if (growAbility)
        {
            dashIcon?.SetActive(false);
            gravityIcon?.SetActive(false);
        }
        else if (!growAbility && !dashAbility && !gravityAbility)
        {
            dashIcon?.SetActive(false);
            growIcon?.SetActive(false);
            gravityIcon?.SetActive(false);
        }

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            LoadMainMenu();
        }
    }

    public void CheckForWin()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var enemy in enemies)
        {
            enemyScript = enemy.GetComponent<Enemy>();
            if(enemyScript.fireAudio)
            {
                audioSource.Play();
            }
        }
        prince = GameObject.Find("Prince")?.GetComponent<Prince>();
        player = GameObject.Find("Player").GetComponent<Player>();
        if(prince && prince.death == true)
        {
            restartButton.gameObject.SetActive(true);
        }
        if(player.shotsAbsorbed == totalShots && currentlyReplaying == false)
        {
            nextLevelButton.gameObject.SetActive(true);
            level++;
        }
        else if(player.shotsAbsorbed == totalShots && currentlyReplaying == true)
        {
            chooseLevelButton.gameObject.SetActive(true);
        }
    }

    public void LevelFromMenu()
    {
        currentlyReplaying = false;
        level++;
        SceneManager.LoadScene(level);
    }

    public void LevelFromGame()
    {
        currentlyReplaying = false;
        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        if(mainMenu && mainMenuOn == false)
        {
            mainMenu.gameObject.SetActive(true);
            mainMenuOn = true;
            playerGO.SetActive(false);
            foreach(var enemy in totalEnemies)
            {
                enemy.SetActive(false);
            }
        }
        else if (mainMenuOn == true)
        {
            mainMenu.gameObject.SetActive(false);
            mainMenuOn = false;
            playerGO.SetActive(true);
            foreach(var enemy in totalEnemies)
            {
                enemy.SetActive(true);
            }
        }
    }

    public void ReplayLevel()
    {
        currentlyReplaying = true;
        string clickedButtonName = EventSystem.current.currentSelectedGameObject.name;
        temporaryLevel = Int32.Parse(clickedButtonName);
        SceneManager.LoadScene(temporaryLevel);
    }

    public void SaveGame()
    {
        currentlyReplaying = false;
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        currentlyReplaying = false;
        level = PlayerPrefs.GetInt("level");
        SceneManager.LoadScene(level);
    }

    public void BackToChooseLevel()
    {
        SceneManager.LoadScene(34);
    }

    public void RestartCompletely()
    {
        currentlyReplaying = false;
        level = 0;
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.Save();
        SceneManager.LoadScene(level);
    }
}
