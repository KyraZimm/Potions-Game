using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public static Interactable HeldItem { get; private set; }
    private static Camera m_mainCam = null;

    private void Awake() {
        if (m_mainCam == null) m_mainCam = Camera.main;
        HeldItem = null;
    }
    private void OnMouseDown() {
        if (HeldItem != null) return;
        HeldItem = this;
    }

    private void OnMouseDrag() {
        if (HeldItem == this) {
            Vector2 targetPos = m_mainCam.ScreenToWorldPoint(Input.mousePosition);
            targetPos = Vector2.Lerp(rb.position, targetPos, 0.5f);
            rb.MovePosition(targetPos);
        }
    }

    private void OnMouseUp() {
        if (HeldItem == this) {
            HeldItem = null;
        }
    }
}
