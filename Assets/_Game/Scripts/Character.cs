using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private List<Brick> bricks = new List<Brick>();
    private List<Vector3> brickPositions = new List<Vector3>();

    private string currentAnimationName;
    private CommonEnum.ColorType currentColorType;
    private CommonEnum.Direction currentDirection;
    //private MeshRenderer currentMeshRenderer;

    [SerializeField] private Brick correspondBrickPrefab;

    private void Awake()
    {
        OnInit();
    }

    private void Start()
    {
        
    }

    public virtual void OnInit()
    {
        currentDirection = CommonEnum.Direction.None;
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
        correspondBrickPrefab.ChangeColor(data.color);
        correspondBrickPrefab.ChangeMaterial(data.GetMaterial(data.color));
    }

    public CommonEnum.ColorType GetCurrentColor() => currentColorType;

    public Brick GetCorrespondBrick() => correspondBrickPrefab;

    public Material GetCurrentMeshMaterial() => currentMeshRenderer.material;

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
            brick.transform.parent = null;
            BrickPool.Despawn(brick);
        }
    }

    public void ClearBrick()
    {

    }

    public Brick GetLastBrick()
    {
        return bricks.Count > 0 ? bricks[bricks.Count - 1] : null;
    }

    public int GetCurrentTotalBricks()
    {
        return bricks.Count;
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

    public void AddBrickPosition(Vector3 pos)
    {
        brickPositions.Add(pos);
    }

    public Vector3 GetTargetBrickPosition()
    {
        int index = Random.Range(0, brickPositions.Count);
        //return brickPositions[index] != vec ? brickPositions[index] : Vector3.zero;
        return brickPositions[index];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_BRICK))
        {
            Brick brick = other.GetComponent<Brick>();
            if ((int)currentColorType == (int)brick.GetColorType())
            {
                AddBrick(brick);
            }
        }
    }
}
