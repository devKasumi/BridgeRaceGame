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
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInit()
    {
        currentLevelIndex = 0;
        InitLevel(currentLevelIndex);
    }

    public void OnDespawn()
    {

    }

    public void InitLevel(int levelIndex)
    {
        currentLevel = Instantiate(levelPrefabs[currentLevelIndex], Vector3.zero, Quaternion.identity);
    }

    public Level GetCurrentLevel() => currentLevel;

    public int GetCurrentLevelIndex() => currentLevelIndex;

    public void ReplayCurrentLevel()
    {

    }

    public void LoadNextLevel()
    {

    }

    public void LoadLevel()
    {

    }

    
}
