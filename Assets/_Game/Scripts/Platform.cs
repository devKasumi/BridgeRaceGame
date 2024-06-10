using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    //[SerializeField] private Player player;
    [SerializeField] private Character[] characters;
    [SerializeField] private List<Brick> bricks = new List<Brick>();

    //private List<Vector3> brickPositions = new List<Vector3>();

    private int count = 0;

    private int minX = -5;
    private int minZ = 1;
    //private float maxX = 8.5f;
    private int maxX = 7;
    //private int maxZ = 12;
    private int maxZ = 11;
    private const float yPos = -0.8f;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minZ; j <= maxZ; j++)
            {
                int randomIndex = Random.Range(0, characters.Length);
                //if (characters[randomIndex].GetCurrentTotalPlatformBrick() == BrickPool.GetAmount())
                //{
                //    int currentIndex = randomIndex;
                //    randomIndex = Random.Range(0, characters.Length);
                //}
                Vector3 pos = new Vector3(i, yPos, j);
                Brick brick = BrickPool.Spawn<Brick>(characters[randomIndex].GetCurrentColor(), pos, transform.rotation);
                characters[randomIndex].AddBrickPosition(brick.transform);
                characters[randomIndex].AddPlatformBrick(brick, pos);
                //brickPositions.Add(new Vector3(i, yPos, j));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;
        //if (timer >= 5)
        //{
        //    Debug.LogError("timer xDDDDD");
        //    for (int i = 0; i < bricks.Count; i++)
        //    {
        //        Debug.LogError("loop spawn brick!!!");
        //        if (bricks[i] != null) continue;
        //        else
        //        {
        //            Debug.LogError("spawn brick!!!!");
        //            Brick brick = BrickPool.Spawn<Brick>(GetCurrentColor(), brickPositions[i].position, Quaternion.identity);
        //            AddBrickPosition(brick.transform);
        //        }
        //    }
        //    timer = 0f;
        //}
    }

    public void SpawnBrick()
    {
        
    }
}
