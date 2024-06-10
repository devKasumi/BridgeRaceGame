using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private Stair stair;
    [SerializeField] private Barrier barrier;
    [SerializeField] private int totalStairNumbers;
    private List<Stair> stairs = new List<Stair>();
    private List<Barrier> barriers = new List<Barrier>();
    private Vector3 firstStairPos;
    private Vector3 firstBarrierPos;
    private int count = 1;
    private int totalStairsActive = 0;


    // Start is called before the first frame update
    void Start()
    {
        firstStairPos = stair.transform.position;
        firstBarrierPos = barrier.transform.position;

        for (int i = 0; i < totalStairNumbers; i++)
        {
            stairs.Add(Instantiate(stair, new Vector3(
                firstStairPos.x,
                firstStairPos.y + count * Constants.STAIR_DISTANCE_Y,
                firstStairPos.z + count * Constants.STAIR_DISTANCE_Z), stair.transform.rotation));

            barriers.Add(Instantiate(barrier, new Vector3(
                firstBarrierPos.x,
                firstBarrierPos.y + count * Constants.STAIR_DISTANCE_Y,
                firstBarrierPos.z + count * Constants.STAIR_DISTANCE_Z), barrier.transform.rotation));

            count++;    
        }
    }

    public int GetStairIndex(Stair stair)
    {
        return stairs.IndexOf(stair);
    }

    public void EnableBarrierBox(int index)
    {
        barriers[index + 1].GetComponent<BoxCollider>().enabled = true;
    }

    public void IncreaseStairActive()
    {
        totalStairsActive++;
    }

    public bool IsEnoughStairForBridge()
    {
        return totalStairsActive == totalStairNumbers;
    }

}
