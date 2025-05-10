using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Interactable
{
    [SerializeField] Animator anim;
    [SerializeField] string hoverBoolAnimName;
    [SerializeField][WritableDropdown(typeof(IngredientLibrary), "GetAllIngredientIDs_EditorOnly")] string ingredientID;

    public string ID { get { return ingredientID; } }

    private new void Awake() {
        base.Awake();
        rb.isKinematic = true;
    }

    private void OnMouseEnter() {
        if (anim != null)
            anim.SetBool(hoverBoolAnimName, true);
    }

    private void OnMouseExit() {
        if (anim != null)
        anim.SetBool(hoverBoolAnimName, false);
    }

    private new void OnMouseDown() {
        base.OnMouseDown();
        base.rb.isKinematic = false;
    }
}

[System.Serializable]
public struct IngredientData {
    public string ID;
    public PotionNodeData[] EffectNodes;
    public PotionEffectHorizontal HorizontalDir;
    public PotionEffectVertical VerticalDir;
}