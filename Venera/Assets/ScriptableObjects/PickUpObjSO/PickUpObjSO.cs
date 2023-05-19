using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PickUpObjSO", menuName = "ScriptableObjects/PickUpObject")]
public class PickUpObjSO : ScriptableObject
{
    public Transform prefab;
    public string objectName;
    public float connectedMassScale = 0.5f;
}
