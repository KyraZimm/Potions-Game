using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Interactable
{
    public IngredientData Data { get; private set; }
}

[System.Serializable]
public struct IngredientData {
    public string ID;
}