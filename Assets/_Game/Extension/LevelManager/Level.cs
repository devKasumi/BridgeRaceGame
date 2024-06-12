using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    //[SerializeField] private Player player;
    [SerializeField] private Stage[] stages;
    [SerializeField] private PoolControl PoolControl;

    private int currentStageIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        LoadCurrentStage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetCurrentStageIndex() => currentStageIndex;

    public void ProcessToNextStage()
    {
        currentStageIndex++;
    }

    public void LoadCurrentStage()
    {
        stages[currentStageIndex].GetCurrentStagePlatform().SpawnBrick(currentStageIndex);
    }

    public void PreLoadPool()
    {
        PoolControl.PreLoadPool(currentStageIndex);
    }
}
