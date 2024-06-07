using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonEnum;

[CreateAssetMenu(menuName = "Data/Color Data", fileName = "Color Data")]

public class DataSO : ScriptableObject
{
    [SerializeField] private List<Material> materials = new List<Material>();
    //[SerializeField] private CommonEnum.ColorType colorType;
    //private Material currentMat;

    //public Material GetMaterial()
    //{
    //    return currentMat;
    //}

    //public void SetCurrentMaterial(CommonEnum.ColorType colorType)
    //{
    //    currentMat = materials[(int)colorType];
    //}

    public CommonEnum.ColorType color;

    public Material GetMaterial(CommonEnum.ColorType color) => materials[(int)color];
    //{
    //    return materials[(int)color];
    //}
    //public 

}


