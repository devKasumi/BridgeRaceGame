using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField] private MeshRenderer currentMeshRenderer;
    [SerializeField] private CommonEnum.ColorType currentColorType;
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
            ChangeColor(character.GetCurrentColor());
            ChangeMaterial(character.GetCurrentMeshMaterial());
            currentMeshRenderer.enabled = true;
            character.RemoveBrick(character.GetLastBrick());
            bridge.IncreaseStairActive();
            if (!bridge.IsEnoughStairForBridge() && character.GetCurrentTotalBricks() == 0)
            {
                //character.GetComponent<Player>().StopClimbSatir();
            }
        }
    }
}
