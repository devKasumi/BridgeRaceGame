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
                //character.ResetPlatformBrick();
                LevelManager.GetInstance.GetCurrentLevel().ProcessToNextStage(character);
                character.SetCurrentStageIndex(LevelManager.GetInstance.GetCurrentLevel().GetCurrentStageIndex(character));
                LevelManager.GetInstance.GetCurrentLevel().PreLoadPool(character);
                LevelManager.GetInstance.GetCurrentLevel().LoadCurrentStage(character);
            }
        }
    }
}
