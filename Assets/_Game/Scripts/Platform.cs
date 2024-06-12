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
    [SerializeField] private const float yPos = -0.8f;
    [SerializeField] private Character[] characters;
    [SerializeField] private int brickAmount;
    
    //private List<int> indexNums = new List<int>();
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        //SpawnBrick();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SpawnBrick(int currentStageIndex)
    {
        if (currentStageIndex == 0)
        {
            count = 0;
            for (int i = minX; i <= maxX; i++)
            {
                for (int j = minZ; j <= maxZ; j++)
                {
                    //int randomIndex = Random.Range(0, characters.Length);
                    Vector3 pos = new Vector3(i, yPos, j);
                    Brick brick = BrickPool.Spawn<Brick>(characters[count].GetCurrentColor(), pos, transform.rotation);
                    characters[count].AddBrickPosition(brick.transform);
                    characters[count].AddPlatformBrick(brick, pos);

                    if (characters[count].GetCurrentTotalPlatformBrick() == brickAmount)
                    {
                        count++;
                    }

                }
            }
        }
        else
        {
            count = 0;

        }
        
    }

    public int GetBrickAmount() => brickAmount;

}
