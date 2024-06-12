using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool isNextStageDoor;
    //[SerializeField] private Stage Stage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER) || other.CompareTag(Constants.TAG_BOT))
        {
            if (isNextStageDoor)
            {
                Character character = other.GetComponent<Character>();
                LevelManager.GetInstance.GetCurrentLevel().ProcessToNextStage();
                LevelManager.GetInstance.GetCurrentLevel().LoadCurrentStage();
                character.SetCurrentStageIndex(LevelManager.GetInstance.GetCurrentLevel().GetCurrentStageIndex());
                LevelManager.GetInstance.GetCurrentLevel().PreLoadPool();
            }
        }
    }
}
