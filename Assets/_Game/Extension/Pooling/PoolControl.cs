using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
    [SerializeField] private Brick brick;
    //[SerializeField] Transform PoolParent;
    private List<GameObject> pools = new List<GameObject>();

    public void PreLoadPool(Character character, int platformBrickAmount)
    {
        brick.ChangeColor(character.GetCurrentColor());
        brick.ChangeMaterial(character.GetCurrentMeshMaterial());
        GameObject pool = new GameObject(character.GetCurrentColor().ToString() + "_" + (character.GetCurrentStageIndex() + 1));
        BrickPool.PreLoad(brick,
                          platformBrickAmount,
                          pool.transform);
        pools.Add(pool);
        //pool.transform.SetParent(PoolParent);
    }

    public void ResetPool()
    {
        for (int i =0;i<pools.Count;i++)
        {
            Destroy(pools[i]);
        }
        pools.Clear();
    }

}




