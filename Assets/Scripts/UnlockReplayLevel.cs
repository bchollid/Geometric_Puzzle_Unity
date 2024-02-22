using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockReplayLevel : MonoBehaviour
{
    private int currentLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentLevel = PlayerPrefs.GetInt("level");
        GameManager.level = currentLevel;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentLevel);
        if(currentLevel >= Int32.Parse(gameObject.name))
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        };
    }
}
