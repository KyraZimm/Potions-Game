using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CauldronCollider : MonoBehaviour
{
    [SerializeField] private Cauldron parentCauldron;

    private void OnTriggerEnter2D(Collider2D col) {
        col.TryGetComponent<Ingredient>(out Ingredient ingredient);
        if (ingredient != null) {
            parentCauldron.AddIngredient(ingredient);
        }
    }
}