using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    //[SerializeField] private Player player;
    [SerializeField] private List<Character> characters = new List<Character>();
    [SerializeField] private Stage[] stages;
    [SerializeField] private PoolControl PoolControl;

    public Vector3 originPos;

    private int currentStageIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        characters.Add(LevelManager.GetInstance.GetPlayer());
        for (int i = 0; i < characters.Count; i++)
        {
            //PreLoadPool(characters[i]);
            LoadCurrentStage(characters[i]);
        }
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

    public void LoadCurrentStage(Character character)
    {
        stages[currentStageIndex].GetCurrentStagePlatform().SpawnBrick(currentStageIndex, character);
    }

    public void PreLoadPool(Character character)
    {
        PoolControl.PreLoadPool(currentStageIndex, character);
    }
}
