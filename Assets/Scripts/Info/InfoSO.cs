using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Info", menuName = "ScriptableObjects/Item Info")]
public class InfoSO : ScriptableObject
{
    public InfoPanelData[] ItemInfo;
}