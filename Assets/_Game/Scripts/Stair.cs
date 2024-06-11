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
    private Vector3 originalPosition;

    private void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.position;
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



    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag(Constants.TAG_PLAYER) || other.CompareTag(Constants.TAG_BOT))
        //{
        //    Character character = other.GetComponent<Character>();
        //    if (character.GetCurrentTotalBricks() > 0)
        //    {
        //        if (currentColorType != character.GetCurrentColor())
        //        {
        //            Brick brick = character.GetLastBrick();
        //            ChangeColor(character.GetCurrentColor());
        //            ChangeMaterial(character.GetCurrentMeshMaterial());
        //            Vector3 pos = character.platformBricks[brick];
        //            Quaternion rot = Quaternion.identity;
        //            BrickPool.Despawn(brick);
        //            character.RemoveBrick(brick);
        //            bridge.IncreaseStairActive();
        //            StartCoroutine(ReSpawnBrick(character.GetCurrentColor(), pos, rot));
        //        }
        //        currentMeshRenderer.enabled = true;

        //        if (!bridge.IsEnoughStairForBridge() && character.GetCurrentTotalBricks() == 0)
        //        {
        //            // player or bot can not move 
        //            //bridge.EnableBarrierBox(bridge.GetStairIndex(this));
        //            bridge.EnableWall(bridge.GetStairIndex(this));
        //            if (other.CompareTag(Constants.TAG_BOT))
        //            {
        //                other.GetComponent<Bot>().ChangeState(new PatrolState());
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (currentColorType != character.GetCurrentColor())
        //        {
        //            //bridge.EnableBarrierBox(bridge.GetStairIndex(this));
        //            bridge.EnableWall(bridge.GetStairIndex(this));
        //            if (other.CompareTag(Constants.TAG_BOT))
        //            {
        //                other.GetComponent<Bot>().ChangeState(new PatrolState());
        //            }
        //        }
        //    }
        //}

        if (other.CompareTag(Constants.TAG_PLAYER))
        {
            Player character = other.GetComponent<Player>();
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
                    //bridge.EnableWall(bridge.GetStairIndex(this));
                    bridge.EnableBarrierBox(bridge.GetStairIndex(this));
                }
            }
            else
            {
                if (currentColorType != character.GetCurrentColor())
                {
                    bridge.ResetCurrentBarrier(bridge.GetStairIndex(this));
                    bridge.EnableBarrierBox(bridge.GetStairIndex(this));
                    //bridge.ResetCurrentStair(this);
                    //bridge.EnableWall(bridge.GetStairIndex(this));
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
                //bridge.ResetCurrentStair(this);
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
                    other.GetComponent<Bot>().ChangeState(new PatrolState());
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
