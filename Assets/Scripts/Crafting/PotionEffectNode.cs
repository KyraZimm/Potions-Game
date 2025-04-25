using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionEffectType { Health, Poison }
public class PotionEffectNode : MonoBehaviour
{
    PotionEffectType effectType;

    public void Init(PotionEffectType initAsType) {
        effectType = initAsType;

        //TO-DO: visual changes, rendering different icons/colors based on effect type
    }
}

[System.Serializable]
public struct PotionEffectData {
    public PotionEffectType EffectType;
    public Vector2Int NodePos;
}