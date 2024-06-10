using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
                    Brick brick = character.GetLastBrick();
                    ChangeColor(character.GetCurrentColor());
                    ChangeMaterial(character.GetCurrentMeshMaterial());
                    Vector3 pos = character.platformBricks[brick];
                    Quaternion rot = Quaternion.identity;
                    BrickPool.Despawn(brick);
                    character.RemoveBrick(brick);
                    bridge.IncreaseStairActive();
                    StartCoroutine(ReSpawnBrick(character.GetCurrentColor(), pos, rot));
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

    public IEnumerator ReSpawnBrick(CommonEnum.ColorType colorType, Vector3 pos, Quaternion ros)
    {
        yield return new WaitForSeconds(Random.Range(5f, 7f));
        Brick brick = BrickPool.Spawn<Brick>(colorType, pos, ros);
        yield return new WaitForSeconds(Random.Range(2f, 5f));
    }
}
