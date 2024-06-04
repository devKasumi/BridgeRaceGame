using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private List<Material> materials = new List<Material>();

    [SerializeField] private Animator animator;

    private List<Brick> bricks = new List<Brick>();

    private string currentAnimationName;
    private ColorType currentColorType;
    private Direction currentDirection;
    private MeshRenderer currentMeshRenderer;

    public enum Direction
    {
        None = 0,
        Forward = 1,
        Backward = 2,
        Left = 3,
        Right = 4,
    }

    public enum ColorType
    {
        None = 0,
        Red = 1,
        Blue = 2,
        Green = 3,
        Yellow = 4,
        Orange = 5,
        Purple = 6,
    }

    private void Start()
    {
        
    }

    public virtual void OnInit()
    {
        currentDirection = Direction.None;
    }

    public virtual void OnDespawn()
    {

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

    public void ChangeColor(ColorType colorType)
    {
        this.currentColorType = colorType;
        this.currentMeshRenderer.material = materials[(int)colorType];
    }

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
}
