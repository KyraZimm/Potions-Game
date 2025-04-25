using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionProgressUI : MonoBehaviour
{
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private int scaleCanvasUnit;

    private List<string> ingredients = new List<string>();
    private List<PotionEffectNode> nodes = new List<PotionEffectNode>();

    private Vector2 canvasOrigin;

    private void Awake() {
        canvasOrigin = transform.position;
    }

    public void AddIngredient(Ingredient ingredient) {
        ingredients.Add(ingredient.ID);

        //render ingredient nodes
        IngredientData ingData = IngredientLibrary.GetData(ingredient.ID);
        foreach (PotionEffectData nodeData in ingData.EffectNodes) {
            Vector2 posOnCanvas = canvasOrigin + (nodeData.NodePos * scaleCanvasUnit);
            PotionEffectNode node = Instantiate(nodePrefab, transform).GetComponent<PotionEffectNode>();
            node.Init(nodeData.EffectType);

            nodes.Add(node);

            RectTransform nodeRect = node.gameObject.GetComponent<RectTransform>();
            nodeRect.position = posOnCanvas;
        }
    }

}

//can prob collapse these enums into 1 axis type later, keeping them seperate for now for naming convenience
public enum PotionEffectHorizontal { Left = -1, None = 0, Right = 1 }
public enum PotionEffectVertical { Down = -1, None = 0, Up = 1 }