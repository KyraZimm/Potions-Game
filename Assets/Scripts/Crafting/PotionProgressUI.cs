using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionProgressUI : MonoBehaviour
{
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private int scaleCanvasUnit;

    private List<string> ingredients = new List<string>();
    private List<GameObject> nodes = new List<GameObject>();

    private Vector2 canvasOrigin;

    private void Awake() {
        canvasOrigin = transform.position;
    }

    public void StartNewRecipe(Ingredient baseIngredient) {
        ingredients.Clear();
        ingredients.Add(baseIngredient.ID);

        //render ingredient nodes
        IngredientData ingData = IngredientLibrary.GetData(baseIngredient.ID);
        foreach (PotionEffectNode node in ingData.EffectNodes) {
            Vector2 posOnCanvas = canvasOrigin + (node.NodePos * scaleCanvasUnit);
            GameObject nodeGO = Instantiate(nodePrefab, transform);
            nodes.Add(nodeGO);

            RectTransform nodeRect = nodeGO.GetComponent<RectTransform>();
            nodeRect.position = posOnCanvas;
        }
    }
}

[System.Serializable]
public struct PotionEffectNode {
    public Vector2Int NodePos;
}

//can prob collapse these enums into 1 axis type later, keeping them seperate for now for naming convenience
public enum PotionEffectHorizontal { Left = -1, None = 0, Right = 1 }
public enum PotionEffectVertical { Down = -1, None = 0, Up = 1 }