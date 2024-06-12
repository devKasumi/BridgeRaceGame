using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using UnityEngine.TextCore.Text;

public class Stair : MonoBehaviour
{
    [SerializeField] private MeshRenderer currentMeshRenderer;
    [SerializeField] private CommonEnum.ColorType currentColorType = CommonEnum.ColorType.None;
    [SerializeField] private Bridge bridge;
    [SerializeField] private BoxCollider boxCollider;
    private Vector3 originalScale;
    private Vector3 originalPosition;
    //private Vector3 originalBoxSize;
    //private float framerate = 5f;
    //private float time = 0;
    //private Character character;
    //private Vector3 pos;
    //private Quaternion rot;

    private void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.position;
        //originalBoxSize = boxCollider.size;
    }

    private void FixedUpdate()
    {
        
    }

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

    public Vector3 GetOriginalScale()
    {
        return originalScale;
    }

    public void EnableWall()
    {
        transform.localScale = new Vector3(originalScale.x, originalScale.y * 10f, originalScale.z);
        boxCollider.isTrigger = false;
        Debug.LogError("adasdasd:  " + transform.localScale);
    }

    public void ResetStairToNormal()
    {

        transform.localScale = originalScale;
        transform.position = originalPosition;
        //boxCollider.isTrigger = true;
        GetComponent<BoxCollider>().isTrigger = true;
        Debug.LogError("origin localScale:  " + originalScale + "  istrigger:  " + boxCollider.isTrigger);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_PLAYER))
        {
            Player character = other.GetComponent<Player>();
            if (character.GetCurrentTotalBricks() > 0)
            {
                if (currentColorType != character.GetCurrentColor())
                {
                    NormalStairChecking(character, bridge);
                }
                currentMeshRenderer.enabled = true;
                if (!bridge.IsEnoughStairForBridge() && character.GetCurrentTotalBricks() == 0)
                {
                    bridge.EnableBarrierBox(bridge.GetStairIndex(this));
                }
            }
            else
            {
                if (currentColorType != character.GetCurrentColor())
                {
                    bridge.EnableBarrierBox(bridge.GetStairIndex(this));
                }
            }
        }
        else if (other.CompareTag(Constants.TAG_BOT))
        {
            Bot character = other.GetComponent<Bot>();
            if (character.GetCurrentTotalBricks() > 0)
            {
                if (currentColorType != character.GetCurrentColor())
                {
                    NormalStairChecking(character, bridge);
                }
                bridge.ResetCurrentBarrier(bridge.GetStairIndex(this));
                currentMeshRenderer.enabled = true;
                if (!bridge.IsEnoughStairForBridge() && character.GetCurrentTotalBricks() == 0)
                {
                    character.ChangeState(new PatrolState());
                }
            }
            else
            {
                if (currentColorType != character.GetCurrentColor())
                {
                    character.ChangeState(new PatrolState());
                }
            }
        }
    }

    private void NormalStairChecking(Character character, Bridge bridge)
    {
        Brick brick = character.GetLastBrick();
        ChangeColor(character.GetCurrentColor());
        ChangeMaterial(character.GetCurrentMeshMaterial());
        Vector3 pos = character.platformBricks[brick];
        Quaternion rot = Quaternion.identity;
        character.RemoveBrick(brick);
        bridge.IncreaseStairActive();
        StartCoroutine(ReSpawnBrick(character, character.GetCurrentColor(), pos, rot));
    }

    public IEnumerator ReSpawnBrick(Character character, CommonEnum.ColorType colorType, Vector3 pos, Quaternion ros)
    {
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        Brick brick = BrickPool.Spawn<Brick>(colorType, pos, ros);
        character.AddBrickPosition(brick.transform);
        character.AddPlatformBrick(brick, pos);
        //yield return new WaitForSeconds(Random.Range(5f, 7f));
    }
}
