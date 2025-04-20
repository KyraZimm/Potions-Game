using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IngredientLibrary
{
    private static Dictionary<string, IngredientData> _allIngredientData = new Dictionary<string, IngredientData>();
    public static void Init(ConfigSO config) {
        ReadDataFromConfig(config.IngredientDataSO.IngredientsData);
    }
    
    private static void ReadDataFromConfig(IngredientData[] configData) {
        _allIngredientData.Clear();
        foreach (IngredientData data in configData) {
            _allIngredientData.Add(data.ID, data);
            Debug.Log($"Deserialized Ingredient: {data.ID}");
        }
    }
}
