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
    //[SerializeField] private Color 

    private List<Brick> bricks = new List<Brick>();

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

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void GetData()
    {
        currentColorType = data.color;
        currentMeshRenderer.material = data.GetMaterial(currentColorType);
        correspondBrickPrefab.ChangeColor(data.color);
        correspondBrickPrefab.ChangeMaterial(data.GetMaterial(data.color));
        //correspondBrick.SetPoolColorType(currentColorType);
    }

    public CommonEnum.ColorType GetCurrentColor()
    {
        return currentColorType;
    }

    public Brick GetCorrespondBrick()
    {
        Debug.Log(correspondBrickPrefab.GetColorType());
        return correspondBrickPrefab;
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

    //public void ChangeColor(CommonEnum.ColorType colorType)
    //{
    //    this.currentColorType = colorType;
    //    this.currentMeshRenderer.material = materials[(int)colorType];
    //}

    public void AddBrick(Brick brick)
    {
        brick.gameObject.SetActive(true);
        bricks.Add(brick);
        StackBrick();
    }

    public void RemoveBrick(Brick brick)
    {
        bricks.Remove(brick);
    }

    public void ClearBrick()
    {

    }

    public void StackBrick()
    {
        Transform brickTransform = bricks[bricks.Count - 1].transform;
        brickTransform.SetParent(transform);
        if (bricks.Count == 1)
        {
            brickTransform.localPosition = new Vector3(0f, firstBrickY, firstBrickZ);
        }
        else
        {
            brickTransform.localPosition = new Vector3(0f, firstBrickY + (bricks.Count - 1) * 0.3f, firstBrickZ);
        }
        Debug.Log("is set active: " + bricks[bricks.Count - 1].gameObject.activeSelf);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_BRICK))
        {
            Brick brick = other.GetComponent<Brick>();
            if (currentColorType == brick.GetColorType())
            {
                AddBrick(brick);
                //Destroy(other.gameObject);
                //BrickPool.Despawn(brick);
            }
        }
    }
}
