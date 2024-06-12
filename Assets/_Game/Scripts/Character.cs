using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour
{
    //[SerializeField] private List<Material> materials = new List<Material>();
    [SerializeField] private DataSO data;
    [SerializeField] private Animator animator;
    [SerializeField] private MeshRenderer currentMeshRenderer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform characterImage;
    [SerializeField] private float firstBrickY = -0.5f;
    [SerializeField] private float firstBrickZ = -0.6f;
    [SerializeField] private Brick correspondBrickPrefab;
    private List<Brick> bricks = new List<Brick>();
    private List<Transform> brickPositions = new List<Transform>();
    public Dictionary<Brick, Vector3> platformBricks = new Dictionary<Brick, Vector3>();

    private Vector3 currentTargetPosition = Vector3.zero;

    //public bool isAI;

    //private List<Brick> platformBricks = new List<Brick>();
    

    private string currentAnimationName;
    private CommonEnum.ColorType currentColorType;
    //private CommonEnum.Direction currentDirection;
    //private MeshRenderer currentMeshRenderer;

    private int currentStageIndex = 0;

    //public bool isMoving;

    //public bool canMove = true;

    //private float timer = 0f;


    private void Awake()
    {
        OnInit();
    }

    private void FixedUpdate()
    {
        //timer += Time.deltaTime;
        //if (timer >= 5f)
        //{
        //    Debug.LogError("timer xDDDDD");
        //    for (int i = 0; i < platformBricks.Count; i++)
        //    {
        //        Debug.LogError("loop spawn brick!!!   " + platformBricks[i]);
        //        if (platformBricks[i] != null) continue;
        //        else
        //        {
        //            Debug.LogError("spawn brick xDDDDDDDDD!!!!");
        //            Brick brick = BrickPool.Spawn<Brick>(GetCurrentColor(), platformBricks[i].transform.position, platformBricks[i].transform.rotation);
        //            //AddBrickPosition(brick.transform);
        //            platformBricks[i] = brick;
        //        }
        //        //platformBricks[i].gameObject.SetActive(true);
        //    }
        //    timer = 0f;
        //}
    }

    private void Start()
    {
        //currentTargetPosition = brickPositions[0];
        //SetTargetBrickPosition();
    }

    public virtual void OnInit()
    {
        //currentDirection = CommonEnum.Direction.None;
        GetData();
    }

    public virtual void OnDespawn()
    {
        
    }

    //public void AddPlatformBrick(Brick platformBrick)
    //{
    //    platformBricks.Add(platformBrick);
    //}

    public float GetMoveSpeed() => moveSpeed;

    public void GetData()
    {
        currentColorType = data.color;
        currentMeshRenderer.material = data.GetMaterial(currentColorType);
        correspondBrickPrefab.ChangeColor(data.color);
        correspondBrickPrefab.ChangeMaterial(data.GetMaterial(data.color));
    }

    public CommonEnum.ColorType GetCurrentColor() => currentColorType;

    public Brick GetCorrespondBrick() => correspondBrickPrefab;

    public Material GetCurrentMeshMaterial() => currentMeshRenderer.material;

    public int GetCurrentStageIndex() => currentStageIndex; 

    public void SetCurrentStageIndex(int index)
    {
        currentStageIndex = index;
    }

    public void ChangeAnimation(string animationName)
    {
        if (currentAnimationName != animationName)
        {
            animator.ResetTrigger(animationName);
            currentAnimationName = animationName;
            animator.SetTrigger(currentAnimationName);
        }
    }

    public void AddBrick(Brick brick)
    {
        brick.gameObject.SetActive(true);
        bricks.Add(brick);
        StackBrick();
    }

    public void RemoveBrick(Brick brick)
    {
        if (bricks.Count > 0)
        {
            bricks.Remove(brick);
            //brick.gameObject.SetActive(false);
            brick.transform.parent = null;
            BrickPool.Despawn(brick);
        }
    }

    public void ClearBrick()
    {

    }

    public void AddPlatformBrick(Brick brick, Vector3 pos)
    {
        platformBricks[brick] = pos;
    }

    //public int GetPlatformBrickIndex(Brick brick)
    //{
    //    return platformBricks.IndexOf(brick);
    //}

    //public Vector3 GetPlatformBrickPosition(int index)
    //{
    //    return brickPositions[index].position;
    //}

    public Brick GetLastBrick() => bricks.Count > 0 ? bricks[bricks.Count - 1] : null;

    public int GetCurrentTotalBricks() => bricks.Count;


    public int GetCurrentTotalPlatformBrick()
    {
        return brickPositions.Count;
    }

    public void StackBrick()
    {
        Transform brickTransform = bricks[bricks.Count - 1].transform;
        brickTransform.SetParent(transform);
        brickTransform.rotation = transform.rotation;
        if (bricks.Count == 1)
        {
            brickTransform.localPosition = new Vector3(0f, firstBrickY, firstBrickZ);
        }
        else
        {
            brickTransform.localPosition = new Vector3(0f, firstBrickY + (bricks.Count - 1) * 0.3f, firstBrickZ);
        }
        //Debug.Log("is set active: " + bricks[bricks.Count - 1].gameObject.activeSelf);
    }

    public void AddBrickPosition(Transform pos)
    {
        brickPositions.Add(pos);
    }

    public void SetTargetBrickPosition()
    {
        if (brickPositions.Count > 0)
        {
            int index = Random.Range(0, brickPositions.Count);
            //if (brickPositions[index] != null)
            //{
            //    currentTargetPosition = brickPositions[index].position;
            //}
            currentTargetPosition = brickPositions[index].position;
        }
    }

    public Vector3 GetTargetBrickPosition()
    {
        return currentTargetPosition;
    }

    public bool IsCharacterReachTarget()
    {
        return (Vector3.Distance(transform.position, currentTargetPosition) < 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_BRICK))
        {
            Brick brick = other.GetComponent<Brick>();
            if ((int)currentColorType == (int)brick.GetColorType())
            {
                AddBrick(brick);
                brickPositions.Remove(brick.transform);
                //platformBricks.Remove(brick);
            }
        }
    }
}
