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
    //[SerializeField] private Color 

    private List<Brick> bricks = new List<Brick>();

    private string currentAnimationName;
    private CommonEnum.ColorType currentColorType;
    private CommonEnum.Direction currentDirection;
    //private MeshRenderer currentMeshRenderer;

    [SerializeField] private Brick correspondBrick;


    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        currentDirection = CommonEnum.Direction.None;
        GetData();
    }

    public virtual void OnDespawn()
    {

    }

    public void GetData()
    {
        currentColorType = data.color;
        currentMeshRenderer.material = data.GetMaterial(currentColorType);
        correspondBrick.ChangeColor(currentColorType);
        correspondBrick.ChangeMaterial(currentMeshRenderer.material);
        correspondBrick.SetPoolColorType(currentColorType);
    }

    public Brick GetCorrespondBrick()
    {
        return correspondBrick;
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
        bricks.Add(brick);
    }

    public void RemoveBrick(Brick brick)
    {
        bricks.Remove(brick);
    }

    public void ClearBrick()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_BRICK))
        {
            Brick brick = other.GetComponent<Brick>();
            if (currentColorType == brick.GetColorType())
            {
                AddBrick(brick);
            }
        }
    }
}
