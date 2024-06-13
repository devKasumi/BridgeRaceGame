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
        characters.Add(LevelManager.GetInstance.GetPlayer());
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SpawnBrick(int currentStageIndex, Character character)
    {
        if (currentStageIndex == 0)
        {
            //for (int i = minX; i <= maxX; i++)
            //{
            //    for (int j = minZ; j <= maxZ; j++)
            //    {
            //        //int randomIndex = Random.Range(0, characters.Length);
            //        Vector3 pos = new Vector3(i, yPos, j);
            //        Brick brick = BrickPool.Spawn<Brick>(characters[count].GetCurrentColor(), pos, transform.rotation);
            //        characters[count].AddBrickPosition(brick.transform);
            //        characters[count].AddPlatformBrick(brick, pos);

            //        if (characters[count].GetCurrentTotalPlatformBrick() == brickAmount)
            //        {
            //            count++;
            //        }

            //    }
            //}
            //Debug.LogError("spawn brick 0");
            
            int totalPosCount = brickAmount;
            //Debug.Log(totalPosCount);
            while (totalPosCount > 0)
            {
                //for (int i = 0; i < characters.Length; i++)
                //{
                //    int index = Random.Range(0, listPos.Count);
                //    Brick brick = BrickPool.Spawn<Brick>(characters[i].GetCurrentColor(), listPos[index], transform.rotation);
                //    characters[i].AddBrickPosition(brick.transform);
                //    characters[i].AddPlatformBrick(brick, listPos[index]);
                //    totalPosCount--;
                //    listPos.RemoveAt(index);
                //}
                int index = Random.Range(0, listPos.Count);
                Brick brick = BrickPool.Spawn<Brick>(character.GetCurrentColor(), listPos[index], Quaternion.identity);
                character.AddBrickPosition(brick.transform);
                character.AddPlatformBrick(brick, listPos[index]);
                totalPosCount--;
                listPos.RemoveAt(index);
                Debug.LogError(totalPosCount);
            }
        }
        else
        {
            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minZ; j <= maxZ; j++)
                {
                    listPos.Add(new Vector3(i, yPos, j));
                }
            }
        }
        
    }

    public int GetBrickAmount() => brickAmount;

}
