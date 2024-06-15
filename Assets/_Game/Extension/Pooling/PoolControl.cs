using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
    //[SerializeField] private List<Character> characters = new List<Character>();
    //[SerializeField] private List<Platform> platforms = new List<Platform>();
    [SerializeField] private Brick brick;
    [SerializeField] Transform PoolParent;

    private void Awake()
    {

    }

    public void PreLoadPool(Character character, int platformBrickAmount)
    {
        brick.ChangeColor(character.GetCurrentColor());
        brick.ChangeMaterial(character.GetCurrentMeshMaterial());
        GameObject pool = new GameObject(character.GetCurrentColor().ToString() + "_" + (character.GetCurrentStageIndex() + 1));
        BrickPool.PreLoad(brick,
                          platformBrickAmount,
                          pool.transform);
        pool.transform.SetParent(PoolParent);
    }

}

[System.Serializable]
public class PoolAmount
{
    public Brick prefab;
    public Transform parent;
    public float amount;
}



