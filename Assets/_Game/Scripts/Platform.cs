using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private int minX;
    [SerializeField] private int minZ;
    [SerializeField] private int maxX;
    [SerializeField] private int maxZ;
    [SerializeField] private float yPos = -0.8f;
    [SerializeField] private List<Character> characters = new List<Character>();
    [SerializeField] private int brickAmount;

    private List<Vector3> listPos = new List<Vector3>();    

    private void Awake()
    {
        characters.Add(LevelManager.GetInstance.GetPlayer());

        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minZ; j <= maxZ; j++)
            {
                listPos.Add(new Vector3(i, yPos, j));
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //SpawnBrick();
        
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SpawnBrick(int currentStageIndex, Character character)
    {
        //if (currentStageIndex == 0)
        //{

        //    int totalPosCount = brickAmount;
        //    //Debug.Log(totalPosCount);
        //    if (character.GetCurrentStageIndex() == currentStageIndex)
        //    {
        //        while (totalPosCount > 0)
        //        {
        //            int index = Random.Range(0, listPos.Count);
        //            Brick brick = BrickPool.Spawn<Brick>(character.GetCurrentColor(), listPos[index], Quaternion.identity);
        //            character.AddBrickPosition(brick.transform);
        //            character.AddPlatformBrick(brick, listPos[index]);
        //            totalPosCount--;
        //            listPos.RemoveAt(index);
        //            Debug.LogError(totalPosCount);
        //        }
        //    }

        //}
        //else
        //{
        //    for (int i = minX; i <= maxX; i++)
        //    {
        //        for (int j = minZ; j <= maxZ; j++)
        //        {
        //            listPos.Add(new Vector3(i, yPos, j));
        //        }
        //    }
        //}
        //if (currentStageIndex != 0)
        //{
        //    InitPosForPlatform();

        //}
        //InitPosForPlatform();
        int totalPosCount = brickAmount;
        //Debug.Log(totalPosCount);
        //character.ResetPlatformBrick();
        if (character.GetCurrentStageIndex() == currentStageIndex)
        {
            while (totalPosCount > 0)
            {
                //Debug.Log()
                int index = Random.Range(0, listPos.Count);
                Brick brick = BrickPool.Spawn<Brick>(character.GetCurrentColor(), listPos[index], Quaternion.identity);
                character.AddBrickPosition(brick.transform);
                character.AddPlatformBrick(brick, listPos[index]);
                listPos.RemoveAt(index);
                totalPosCount--;
                //Debug.LogError(totalPosCount);
            }
        }


    }

    public void InitPosForPlatform()
    {
        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minZ; j <= maxZ; j++)
            {
                listPos.Add(new Vector3(i, yPos, j));
            }
        }
    }

    public int GetBrickAmount() => brickAmount;

}
