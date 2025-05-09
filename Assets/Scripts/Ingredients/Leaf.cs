using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : Ingredient
{
    [SerializeField] Animator anim;
    [SerializeField] string hoverBoolAnimName;


    private void OnMouseEnter() {
        anim.SetBool(hoverBoolAnimName, true);
    }

    private void OnMouseExit() {
        anim.SetBool(hoverBoolAnimName, false);
    }

    private new void OnMouseDown() {
        base.OnMouseDown();
        base.rb.isKinematic = false;
    }
}
