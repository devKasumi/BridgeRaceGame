using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField] private MeshRenderer currentMeshRenderer;
    [SerializeField] private CommonEnum.ColorType currentColorType = CommonEnum.ColorType.None;
    [SerializeField] private Bridge bridge;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER) || other.CompareTag(Constants.TAG_BOT))
        {
            Character character = other.GetComponent<Character>();
            if (character.GetCurrentTotalBricks() > 0)
            {
                if (currentColorType != character.GetCurrentColor())
                {
                    ChangeColor(character.GetCurrentColor());
                    ChangeMaterial(character.GetCurrentMeshMaterial());
                    character.RemoveBrick(character.GetLastBrick());
                    bridge.IncreaseStairActive();
                }
                currentMeshRenderer.enabled = true;

                if (!bridge.IsEnoughStairForBridge() && character.GetCurrentTotalBricks() == 0)
                {
                    // player or bot can not move 
                    bridge.EnableBarrierBox(bridge.GetStairIndex(this));
                }
            }
            else
            {
                
            }
        }
    }
}
