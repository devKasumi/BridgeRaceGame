using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
    [SerializeField] private List<Character> characters = new List<Character>();

    private void Awake()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            SimplePool.PreLoad(characters[i].GetCorrespondBrick(), 5, new GameObject("Brick_" + i).transform);
        }
    }
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

