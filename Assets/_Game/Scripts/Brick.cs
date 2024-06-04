using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER))
        {
            switch (other.tag)
            {
                case Constants.TAG_PLAYER:
                    other.GetComponent<Player>().AddBrick(this);
                    break;
                case Constants.TAG_BOT:
                    other .GetComponent<Bot>().AddBrick(this);   
                    break;
                default:
                    break;
            }
        }
    }
}
