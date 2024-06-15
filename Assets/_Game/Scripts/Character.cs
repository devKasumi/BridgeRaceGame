using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private DataSO data;
    [SerializeField] private Animator animator;
    [SerializeField] private MeshRenderer currentMeshRenderer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float firstBrickY = -0.5f;
    [SerializeField] private float firstBrickZ = -0.6f;
    [SerializeField] private CharacterBrick characterBrickPrefab;

    private List<CharacterBrick> bricks = new List<CharacterBrick>();

    private Vector3 currentTargetPosition = Vector3.zero;
    

    private string currentAnimationName;
    private CommonEnum.ColorType currentColorType;

    private int currentStageIndex = 0;


    private void Awake()
    {
        OnInit();
    }

    private void Start()
    {
        //currentTargetPosition = brickPositions[0];
        //SetTargetBrickPosition();
        
    }

    public virtual void OnInit()
    {
        //currentDirection = CommonEnum.Direction.None;
        //correspondBrickPrefab = 
        GetData();
    }

    public virtual void OnDespawn()
    {
        
    }

    public float GetMoveSpeed() => moveSpeed;

    public void GetData()
    {
        currentColorType = data.color;
        currentMeshRenderer.material = data.GetMaterial(currentColorType);
        //correspondBrickPrefab.ChangeColor(data.color);
        //correspondBrickPrefab.ChangeMaterial(data.GetMaterial(data.color));
    }

    public CommonEnum.ColorType GetCurrentColor() => currentColorType;

    //public Brick GetCorrespondBrick() => correspondBrickPrefab;

    public Material GetCurrentMeshMaterial() => currentMeshRenderer.material;

    public int GetCurrentStageIndex() => currentStageIndex; 

    public void ProcessToNextStage()
    {
        currentStageIndex++;
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

    public void AddBrick(CharacterBrick brick)
    {
        //brick.gameObject.SetActive(true);
        bricks.Add(brick);
        StackBrick();
    }

    public void RemoveBrick(CharacterBrick brick)
    {
        if (bricks.Count > 0)
        {
            bricks.Remove(brick);
            Destroy(brick.gameObject);
        }
    }

    public void ClearBrick()
    {

    }

    public CharacterBrick GetLastBrick() => bricks.Count > 0 ? bricks[bricks.Count - 1] : null;

    public int GetCurrentTotalBricks() => bricks.Count;


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

    public void SetTargetBrickPosition()
    {
        List<Vector3> bricksPos = LevelManager.GetInstance.GetCurrentLevel().GetCurrentStagePlatform(currentStageIndex).GetPlatformBrickPos()[this];
        currentTargetPosition = bricksPos[Random.Range(0, bricksPos.Count)];
        Debug.LogError("current stage:  " + currentStageIndex + "   pos:  " + currentTargetPosition);
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
            if (currentColorType == brick.GetColorType())
            {
                CharacterBrick characterBrick = Instantiate(characterBrickPrefab);
                characterBrick.ChangeColor(currentColorType);
                characterBrick.ChangeMaterial(GetCurrentMeshMaterial());
                AddBrick(characterBrick);
                BrickPool.Despawn(brick);
                StartCoroutine(ReSpawnBrick(this.GetCurrentColor(), brick.transform.position, brick.transform.rotation));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.TAG_DOOR))
        {
            if (other.GetComponent<Door>().IsNextStageDoor())
            {
                Debug.LogError("process next stage!!!");
                ProcessToNextStage();
                LevelManager.GetInstance.GetCurrentLevel().LoadStage(this, currentStageIndex);
                SetTargetBrickPosition();
            }
        }
    }

    public IEnumerator ReSpawnBrick(CommonEnum.ColorType colorType, Vector3 pos, Quaternion ros)
    {
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        Brick brick = BrickPool.Spawn<Brick>(colorType, pos, ros);
    }
}
