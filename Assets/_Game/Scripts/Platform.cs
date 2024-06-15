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
    [SerializeField] private int brickAmount;

    private List<Vector3> listPos = new List<Vector3>();    
    private Dictionary<Character, List<Vector3>> platformBrickPos = new Dictionary<Character, List<Vector3>>();

    private void Awake()
    {
        InitPosForPlatform();
    }

    public void SpawnBrick(int currentStageIndex, Character character)
    {
        int totalPosCount = brickAmount;
        if (!platformBrickPos.ContainsKey(character))
        {
            platformBrickPos.Add(character, new List<Vector3>());
            if (character.GetCurrentStageIndex() == currentStageIndex)
            {
                while (totalPosCount > 0)
                {
                    int index = Random.Range(0, listPos.Count);
                    Brick brick = BrickPool.Spawn<Brick>(character.GetCurrentColor(), listPos[index], Quaternion.identity);
                    platformBrickPos[character].Add(listPos[index]);
                    listPos.RemoveAt(index);
                    totalPosCount--;
                }
            }
        }
    }

    //public Dictionary<Character, List<Vector3>> GetPlatformBrickPos() => platformBrickPos;

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

    public Dictionary<Character, List<Vector3>> GetPlatformBrickPos() => platformBrickPos;

    public int GetBrickAmount() => brickAmount;

}
