using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/Config")]
public class ConfigSO : ScriptableObject
{
    private const string INSTANCE_FILE_PATH = "Config";

    private static ConfigSO m_instance;
    public static ConfigSO Instance {
        get {
            if (m_instance == null) {
                m_instance = Resources.Load<ConfigSO>(INSTANCE_FILE_PATH);
            }
            return m_instance;
        } 
    }


    public IngredientSO IngredientData;
    public PotionEffectSO PotionEffectVisualData;
    public InfoSO ItemInfoData;
}
