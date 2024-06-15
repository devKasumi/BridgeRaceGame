using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private List<Character> characters = new List<Character>();
    [SerializeField] private Stage[] stages;
    [SerializeField] private PoolControl PoolControl;

    public Vector3 originPos;

    //private int currentStageIndex = 0;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        characters.Add(LevelManager.GetInstance.GetPlayer());
        originPos = transform.position;
        for (int i = 0; i < characters.Count; i++)
        {
            LoadStage(characters[i], 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PoolControl GetPoolControl() => PoolControl;

    public void LoadStage(Character character, int characterStageIndex)
    {
        //Debug.LogError("load stage index:    " + characterStageIndex);
        Platform currentPlatform = stages[characterStageIndex].GetCurrentStagePlatform();
        int brickAmount = currentPlatform.GetBrickAmount();
        PoolControl.PreLoadPool(character, brickAmount);
        currentPlatform.SpawnBrick(characterStageIndex, character);
    }

    public Platform GetCurrentStagePlatform(int characterStageIndex)
    {
        return stages[characterStageIndex].GetCurrentStagePlatform();
    }

}
