using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoProvider : MonoBehaviour
{
    [SerializeField][WritableDropdown(typeof(HUD), "GetItemInfoIDs_EditorOnly")] private string itemID;

    private bool isMouseOver = false;
    private bool sentInfoToHUD = false;

    private void OnMouseEnter() {
        isMouseOver = true;
    }

    private void OnMouseExit() {
        isMouseOver = false;
        if (sentInfoToHUD) {
            HUD.HideInfoPanel();
            sentInfoToHUD = false;
        }
    }

    private void Update() {
        if (!isMouseOver) return;

        if (Input.GetButton("Info")) {
            HUD.SetInfoPanelData(itemID);
            sentInfoToHUD = true;
        }
    }
}
