using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private Stair stair;
    [SerializeField] private int totalStairNumbers;
    private List<Stair> stairs = new List<Stair>();
    private Vector3 firstStairPos;
    private int count = 1;
    private int totalStairsActive = 0;


    // Start is called before the first frame update
    void Start()
    {
        firstStairPos = stair.transform.position;

        for (int i = 0; i < totalStairNumbers; i++)
        {
            stairs.Add(Instantiate(stair, new Vector3(
                firstStairPos.x,
                firstStairPos.y + count * Constants.STAIR_DISTANCE_Y,
                firstStairPos.z + count * Constants.STAIR_DISTANCE_Z), stair.transform.rotation));
            count++;    
        }
    }

    public void IncreaseStairActive()
    {
        totalStairsActive++;
    }

    //public int GetTotalStairsActive()
    //{
    //    return totalStairsActive;
    //}

    //public int GetTotalStairs()
    //{
    //    return totalStairNumbers;
    //}

    public bool IsEnoughStairForBridge()
    {
        return totalStairsActive == totalStairNumbers;
    }

}
