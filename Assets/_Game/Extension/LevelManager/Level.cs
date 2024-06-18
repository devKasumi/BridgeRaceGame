using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Character> characters = new List<Character>();
    [SerializeField] private List<Bot> bots = new List<Bot>();
    [SerializeField] private Stage[] stages;
    [SerializeField] private PoolControl PoolControl;
    [SerializeField] private List<Transform> finalLines = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        characters.Add(LevelManager.GetInstance.GetPlayer());

        for (int i = 0; i < characters.Count; i++)
        {
            LoadStage(characters[i], 0);
        }
    }

    public PoolControl GetPoolControl() => PoolControl;

    public void LoadStage(Character character, int characterStageIndex)
    {
        Platform currentPlatform = stages[characterStageIndex].GetCurrentStagePlatform();
        int brickAmount = currentPlatform.GetBrickAmount();
        PoolControl.PreLoadPool(character, brickAmount);
        currentPlatform.SpawnBrick(characterStageIndex, character);
    }

    public Platform GetCurrentStagePlatform(int characterStageIndex)
    {
        return stages[characterStageIndex].GetCurrentStagePlatform();
    }

    public List<Bot> GetBots() => bots;

    public List<Transform> GetFinalLines() => finalLines;

    //public void DeletePool()
    //{
    //    Destroy(PoolControl.gameObject);
    //}

    public void ReloadStage()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            LoadStage(characters[i], 0);
        }
    }
}
