using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    [SerializeField] private MeshRenderer currentMeshRenderer;
    [SerializeField] private CommonEnum.ColorType currentColorType = CommonEnum.ColorType.None;
    [SerializeField] private Bridge bridge;
    [SerializeField] private BoxCollider boxCollider;
    private Vector3 originalScale;

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

    //public Vector3 GetOriginalScale()
    //{
    //    return originalScale;
    //}

    public void EnableWall()
    {
        transform.localScale = new Vector3(originalScale.x, originalScale.y * 10, originalScale.z);
        boxCollider.isTrigger = false;
    }

    public void ResetStairToNormal()
    {
        transform.localScale = originalScale;
        boxCollider.isTrigger = true;
    }

    private void Start()
    {
        originalScale = transform.localScale;
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
                    BrickPool.Despawn(character.GetLastBrick());
                }
                currentMeshRenderer.enabled = true;

                if (!bridge.IsEnoughStairForBridge() && character.GetCurrentTotalBricks() == 0)
                {
                    // player or bot can not move 
                    //bridge.EnableBarrierBox(bridge.GetStairIndex(this));
                    bridge.EnableWall(bridge.GetStairIndex(this));
                }
            }
            else
            {
                if (currentColorType != character.GetCurrentColor())
                {
                    //bridge.EnableBarrierBox(bridge.GetStairIndex(this));
                    bridge.EnableWall(bridge.GetStairIndex(this));
                }
            }
        }
    }
}
