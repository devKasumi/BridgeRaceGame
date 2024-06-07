using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private MeshRenderer currentMeshRenderer;
    [SerializeField] private CommonEnum.ColorType currentColorType;

    private Transform tf;

    public void ChangeColor(CommonEnum.ColorType colorType)
    {
        this.currentColorType = colorType;
    }

    public void ChangeMaterial(Material material)
    {
        this.currentMeshRenderer.material = material;
    }

    public CommonEnum.ColorType GetColorType()
    {
        return currentColorType;
    } 

    public Transform TF
    {
        get
        {
            if (!tf)
            {
                tf = transform;

            }

            return tf;
        }
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
