using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<Level> levelPrefabs = new List<Level>();

    private Level currentLevel;
    private int currentLevelIndex;  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInit()
    {

    }

    public void OnDespawn()
    {

    }

    public void InitLevel()
    {

    }

    public void ReplayCurrentLevel()
    {

    }

    public void LoadNextLevel()
    {

    }

    public void LoadLevel()
    {

    }

    public int GetCurrentLevelIndex()
    {
        return currentLevelIndex;
    }
}
