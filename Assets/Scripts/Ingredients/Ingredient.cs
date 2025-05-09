using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Interactable
{
    [SerializeField][WritableDropdown(typeof(IngredientLibrary), "GetAllIngredientIDs_EditorOnly")] string ingredientID;

    public string ID { get { return ingredientID; } }
}

[System.Serializable]
public struct IngredientData {
    public string ID;
    public PotionNodeData[] EffectNodes;
    public PotionEffectHorizontal HorizontalDir;
    public PotionEffectVertical VerticalDir;
}