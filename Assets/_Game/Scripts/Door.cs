using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool isNextStageDoor;

    public bool IsNextStageDoor() => isNextStageDoor;
}
