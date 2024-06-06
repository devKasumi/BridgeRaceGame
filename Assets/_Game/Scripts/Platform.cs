using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Player player;

    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (count < BrickPool.GetAmount())
        {
            SpawnBrick();

        }
    }

    public void SpawnBrick()
    {
        while (count < BrickPool.GetAmount())
        {
            Brick brick = BrickPool.Spawn<Brick>(player.GetCurrentColor(), transform.position, transform.rotation);
            count++;
        }
    }
}
