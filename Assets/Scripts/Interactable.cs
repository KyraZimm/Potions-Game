using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D rb;

    public static Interactable HeldItem { get; private set; }
    private static Camera m_mainCam = null;

    protected void Awake() {
        if (m_mainCam == null) m_mainCam = Camera.main;
        HeldItem = null;
    }
    protected void OnMouseDown() {
        if (HeldItem != null) return;
        HeldItem = this;
        Debug.Log("holding item");
    }

    protected void OnMouseDrag() {
        if (HeldItem == this) {
            Vector2 targetPos = m_mainCam.ScreenToWorldPoint(Input.mousePosition);
            targetPos = Vector2.Lerp(rb.position, targetPos, 0.5f);
            rb.MovePosition(targetPos);
        }
    }

    protected void OnMouseUp() {
        if (HeldItem == this) {
            HeldItem = null;
        }
    }
}
