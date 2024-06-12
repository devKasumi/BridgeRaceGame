using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
    [SerializeField] private List<Character> characters = new List<Character>();
    [SerializeField] private List<Platform> platforms = new List<Platform>();

    private void Awake()
    {
        PreLoadPool(0);
    }

    public void PreLoadPool(int currentStageIndex)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].GetCurrentStageIndex() == currentStageIndex)
            {
                characters[i].GetData();
                BrickPool.PreLoad(characters[i].GetCorrespondBrick(), 
                                  platforms[currentStageIndex].GetBrickAmount(), 
                                  new GameObject(characters[i].GetCurrentColor().ToString() + "_" + (characters[i].GetCurrentStageIndex()+1)).transform);
            } 
        }
    }

}

[System.Serializable]
public class PoolAmount
{
    public Brick prefab;
    public Transform parent;
    public float amount;
}

//public enum PoolColorType
//{
//    None = 0,
//    Red = 1,
//    Blue = 2,
//    Green = 3,
//    Yellow = 4,
//    Orange = 5,
//    Purple = 6,
//}

