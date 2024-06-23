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

    public MeshRenderer StairMeshRenderer() => currentMeshRenderer;

    public Bridge Bridge() => bridge;

    private void OnTriggerEnter(Collider other)
    {
        Character character = Cache.GetCharacter(other);
        if (character is Player)
        {
            Player player = (Player)character;
            player.CheckStair(this);
        }
        else
        {
            Bot bot = (Bot)character;
            bot.CheckStair(this);
        }
    }
}
