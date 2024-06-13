using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    //[SerializeField] private Player player;
    [SerializeField] private List<Character> characters = new List<Character>();
    [SerializeField] private Stage[] stages;
    [SerializeField] private PoolControl PoolControl;
    private Dictionary<Character, int> currentStageCharacter = new Dictionary<Character, int>();

    public Vector3 originPos;

    //private int currentStageIndex = 0;

    private void Awake()
    {
        characters.Add(LevelManager.GetInstance.GetPlayer());

        for (int i = 0; i < characters.Count; i++)
        {
            Debug.LogError("pokemon xDDDDD");
            currentStageCharacter[characters[i]] = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
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

    public PoolControl GetPoolControl() => PoolControl;

    public int GetCurrentStageIndex(Character character) => currentStageCharacter[character];

    public void ProcessToNextStage(Character character)
    {
        //currentStageIndex++;
        currentStageCharacter[character]++;
    }

    public void LoadCurrentStage(Character character)
    {
        stages[currentStageCharacter[character]].GetCurrentStagePlatform().SpawnBrick(currentStageCharacter[character], character);
    }

    public void PreLoadPool(Character character)
    {
        Debug.LogError("current stage: " + currentStageCharacter[character] + "  player:  " + character);
        PoolControl.PreLoadPool(currentStageCharacter[character], character);
    }
}
