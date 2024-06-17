using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Player player;
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

    public Character GetPlayer() => player;

    public void InitLevel(int levelIndex)
    {
        currentLevel = Instantiate(levelPrefabs[currentLevelIndex]/*, levelPrefabs[currentLevelIndex].originPos, Quaternion.identity*/);
    }

    public Level GetCurrentLevel() => currentLevel;

    public int GetCurrentLevelIndex() => currentLevelIndex;

    // reset trang thai khi ket thuc game
    public void OnReset()
    {
        BrickPool.CollectAll();
    }

    // tao prefab level moi
    public void OnLoadLevel(int levelIndex)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        currentLevel = Instantiate(levelPrefabs[levelIndex]);
    }

    
}
