using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    //[SerializeField] private Player player;
    [SerializeField] private Character[] characters;

    //private List<Vector3> brickPositions = new List<Vector3>();

    private int count = 0;

    private int minX = -5;
    private int minZ = 1;
    private int maxX = 7;
    private int maxZ = 11;
    private const float yPos = -0.8f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = minX; i < maxX; i++)
        {
            for (int j = minZ; j < maxZ; j++)
            {
                int randomIndex = Random.Range(0, characters.Length);
                Vector3 pos = new Vector3(i, yPos, j);
                Brick brick = BrickPool.Spawn<Brick>(characters[randomIndex].GetCurrentColor(), pos, transform.rotation);
                characters[randomIndex].AddBrickPosition(brick.transform);
                //brickPositions.Add(new Vector3(i, yPos, j));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnBrick()
    {
        
    }
}
