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
            characters[i].GetData();
            BrickPool.PreLoad(characters[i].GetCorrespondBrick(), new GameObject(characters[i].GetCurrentColor().ToString()).transform);
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

