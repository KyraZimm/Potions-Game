using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PotionEffectType { Health, Poison }
public class PotionEffectNode : MonoBehaviour
{
    [SerializeField] Image img;
    [SerializeField] Image highlight;

    PotionEffectType effectType;

    private static Dictionary<PotionEffectType, PotionEffectData> effectDataByType = new Dictionary<PotionEffectType, PotionEffectData>();
    private static bool EffectDictionaryInitialized(){ return effectDataByType != null && effectDataByType.Count > 0; }

    public void Init(PotionEffectType initAsType) {
        effectType = initAsType;

        //if effect data dictionary has not been initialized yet, do so now
        if (!EffectDictionaryInitialized()) {
            ReadDataFromConfig(ConfigSO.Instance.PotionEffectData.EffectData);
        }

        //safety check: if there was an issue reading the config file, abort load
        if (!EffectDictionaryInitialized()) {
            Debug.LogError($"CRITICAL: Could not read potion effect data from config!" +
                $"Cannot render visual data on {nameof(PotionEffectNode)}:{gameObject.name} until a {nameof(PotionEffectSO)} is filled and assigned in {nameof(ConfigSO)}.");
            return;
        }

        RenderEffectVisuals();
        highlight.enabled = false;
    }

    private void ReadDataFromConfig(PotionEffectData[] configData) {
        effectDataByType.Clear();
        foreach (PotionEffectData effectData in configData) {
            effectDataByType.Add(effectData.Effect, effectData);
        }
    }

    private void RenderEffectVisuals() {
        PotionEffectData effectData = effectDataByType[effectType];
        if (effectData.Equals(default(PotionEffectData))) {
            Debug.LogError($"No potion effect data exists for the effect: {effectType}. Could not render.");
            return;
        }

        img.color = effectData.Color;
    }

    public void Highlight(bool active){ highlight.enabled = active; }
}

[System.Serializable]
public struct PotionNodeData {
    public PotionEffectType EffectType;
    public Vector2Int NodePos;
}

[System.Serializable]
public struct PotionEffectData {
    public PotionEffectType Effect;
    public Sprite Icon;
    public Color Color;
}