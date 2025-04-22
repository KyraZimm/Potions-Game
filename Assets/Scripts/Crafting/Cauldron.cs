using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [SerializeField] PotionProgressUI progressUI;
    public void AddIngredient(Ingredient ingredient) {
        Debug.Log($"Added ingredient {ingredient.gameObject.name}!");
        progressUI.StartNewRecipe(ingredient);
    }
}
