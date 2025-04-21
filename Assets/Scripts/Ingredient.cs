using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Interactable
{
    [SerializeField][WritableDropdown(typeof(IngredientLibrary), "GetAllIngredientIDs_EditorOnly")] string ingredientID;
}

[System.Serializable]
public struct IngredientData {
    public string ID;
}