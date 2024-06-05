using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SimplePool
{
    private static Dictionary<PoolColorType, Pool> poolInstance = new Dictionary<PoolColorType, Pool>();

    // khoi tao pool moi
    public static void PreLoad(Brick prefab, int amount, Transform parent)
    {
        if (!prefab)
        {
            Debug.LogError("PREFAB IS EMPTY !");
            return;
        }

        if (!poolInstance.ContainsKey(prefab.PoolColorType) || poolInstance[prefab.PoolColorType] == null)
        {
            Pool p = new Pool();
            p.PreLoad(prefab, amount, parent);
            poolInstance[prefab.PoolColorType] = p;
        }
    }

    // lay phan tu ra tu pool
    public static T Spawn<T>(PoolColorType poolColorType, Vector3 pos, Quaternion rot) where T : Brick
    {
        if (!poolInstance.ContainsKey(poolColorType))
        {
            Debug.LogError(poolColorType + " IS NOT PRELOAD!");
            return null;
        }

        return poolInstance[poolColorType].Spawn(pos, rot) as T;
    }

    // tra pha tu vao trong pool
    public static void Despawn(Brick brick)
    {
        if (!poolInstance.ContainsKey(brick.PoolColorType))
        {
            Debug.LogError(brick.PoolColorType + " IS NOT PRELOAD!");
        }
        poolInstance[brick.PoolColorType].Despawn(brick);
    }

    // thu thap phan tu
    public static void Collect(PoolColorType poolColorType)
    {
        if (!poolInstance.ContainsKey(poolColorType))
        {
            Debug.LogError(poolColorType + " IS NOT PRELOAD!");
        }
        poolInstance[poolColorType].Collect();
    }

    // thu thap tat ca phan tu
    public static void CollectAll()
    {
        foreach(var item in poolInstance.Values)
        {
            item.Collect();
        }
    }

    // destroy 1 pool
    public static void Release(PoolColorType poolColorType)
    {
        if (!poolInstance.ContainsKey(poolColorType))
        {
            Debug.LogError(poolColorType + " IS NOT PRELOAD!");
        }
        poolInstance[poolColorType].Release();
    }

    // destroy tat ca pools
    public static void ReleaseAll()
    {
        foreach(var item in poolInstance.Values)
        {
            item.Release();
        }
    }
}

public class Pool
{
    Transform parent;
    Brick prefab;

    Stack<Brick> inactives = new Stack<Brick>();    

    List<Brick> actives = new List<Brick>();

    // khoi tao pool
    public void PreLoad(Brick prefab, int amount, Transform parent)
    {
        this.parent = parent;
        this.prefab = prefab;

        for (int i = 0; i < amount; i++)
        {
            Despawn(GameObject.Instantiate(prefab, parent));
        }
    }

    // lay phan tu ra tu pool
    public Brick Spawn(Vector3 pos, Quaternion rot)
    {
        Brick brick;

        if (inactives.Count <= 0)
        {
            brick = GameObject.Instantiate(prefab, parent);
        }
        else
        {
            brick = inactives.Pop();
        }

        brick.TF.SetPositionAndRotation(pos, rot);
        actives.Add(brick); 
        brick.gameObject.SetActive(true);

        return brick;
    }

    // tra phan tu vao trong pool
    public void Despawn(Brick brick)
    {
        if (brick != null && brick.gameObject.activeSelf)
        {
            actives.Remove(brick);
            inactives.Push(brick);
            brick.gameObject.SetActive(false);
        }
    }

    // thu thap tat ca cac phan tu dang dung ve pool
    public void Collect()
    {
        while (actives.Count > 0)
        {
            Despawn(actives[0]);
        }
    }

    // destroy tat ca phan tu
    public void Release()
    {
        Collect();

        while (inactives.Count > 0)
        {
            GameObject.Destroy(inactives.Peek().gameObject);
        }
        inactives.Clear();
    }
}
