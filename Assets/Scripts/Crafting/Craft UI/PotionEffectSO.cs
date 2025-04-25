using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Potion Effects", menuName = "ScriptableObjects/Potion Effects")]
public class PotionEffectSO : ScriptableObject
{
    public PotionEffectData[] EffectData;
}