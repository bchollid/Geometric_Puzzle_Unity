using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Prince prince;
    private float totalShots;
    private GameObject[] totalEnemies;
    private Player player;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private GameObject restartButton;
    public static int level = 0;
    // Start is called before the first frame update
    void Start()
    {
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalShots = totalEnemies.Length;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckForWin()
    {
        prince = GameObject.Find("Prince").GetComponent<Prince>();
        player = GameObject.Find("Player").GetComponent<Player>();
        if(prince.death == true)
        {
            restartButton.SetActive(true);
        }
        if(player.shotsAbsorbed == totalShots)
        {
            // player.anim.SetBool("isDying", true);
            nextLevelButton.SetActive(true);
            level++;
        }
    }

    public void NextLevel()
    {
        level++;
        SceneManager.LoadScene(level);
    }

    public void Level()
    {
        SceneManager.LoadScene(level);
    }
}
