using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private CommonEnum.ColorType currentColorType;
    private MeshRenderer currentMeshRenderer;

    public void ChangeColor(CommonEnum.ColorType colorType)
    {
        this.currentColorType = colorType;
        //this.currentMeshRenderer = 
    }

    public CommonEnum.ColorType GetColorType()
    {
        return currentColorType;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag(Constants.TAG_PLAYER))
        //{
        //    switch (other.tag)
        //    {
        //        case Constants.TAG_PLAYER:
        //            {
        //                Player player = other.GetComponent<Player>();
        //                player.AddBrick(this);
        //            }
        //            break;
        //        case Constants.TAG_BOT:
        //            {   
        //                Bot bot = other.GetComponent<Bot>();
        //                bot.GetComponent<Bot>().AddBrick(this);   
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }
}
