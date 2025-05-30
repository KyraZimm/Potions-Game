using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IngredientLibrary
{
    private static Dictionary<string, IngredientData> _allIngredientData = new Dictionary<string, IngredientData>();

    public static void Init(ConfigSO config) {
        ReadDataFromConfig(config.IngredientData.IngredientsData);
    }
    
    private static void ReadDataFromConfig(IngredientData[] configData) {
        _allIngredientData.Clear();
        foreach (IngredientData data in configData) {
            _allIngredientData.Add(data.ID, data);
        }
    }

    public static IngredientData GetData(string id) {
        return _allIngredientData[id];
    }

#if UNITY_EDITOR
    public static string[] GetAllIngredientIDs_EditorOnly() {
        IngredientData[] data = ConfigSO.Instance.IngredientData.IngredientsData;
        ReadDataFromConfig(data);

        List<string> allIDs = new List<string>();
        allIDs.AddRange(_allIngredientData.Keys);
        allIDs.Sort(); //alphabetize

        return allIDs.ToArray();
    }
#endif
}
