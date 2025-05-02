using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionProgressUI : MonoBehaviour
{
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private int scaleCanvasUnit;
    [SerializeField] private LineRenderer lineRenderer;

    private List<string> ingredients = new List<string>();
    private Dictionary<Vector2Int, PotionEffectNode> nodesByCoords = new Dictionary<Vector2Int, PotionEffectNode>();

    private Vector2Int canvasOrigin;
    private Vector2Int currNodeCoords;
    private List<PotionEffectNode> potionProgress = new List<PotionEffectNode>();

    private void Awake() {
        canvasOrigin = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        currNodeCoords = Vector2Int.zero;
    }

    public void AddIngredient(Ingredient ingredient) {
        ingredients.Add(ingredient.ID);

        //render ingredient nodes
        IngredientData ingData = IngredientLibrary.GetData(ingredient.ID);
        foreach (PotionNodeData nodeData in ingData.EffectNodes) {
            Vector2Int coords = currNodeCoords + nodeData.NodePos;

            //only render node if these coords have not been taken by another node
            if (nodesByCoords.ContainsKey(coords)) {
                continue;
            }

            //else, generate node & add to dictionary
            PotionEffectNode node = Instantiate(nodePrefab, transform).GetComponent<PotionEffectNode>();
            node.Init(nodeData.EffectType);
            nodesByCoords.Add(coords, node);

            RectTransform nodeRect = node.gameObject.GetComponent<RectTransform>();
            Vector2Int posOnCanvas = canvasOrigin + (coords * scaleCanvasUnit);
            nodeRect.position = new Vector2(posOnCanvas.x, posOnCanvas.y);
        }
    }

    public void ResetPotion() {
        potionProgress.Clear();
    }

}

//can prob collapse these enums into 1 axis type later, keeping them seperate for now for naming convenience
public enum PotionEffectHorizontal { Left = -1, None = 0, Right = 1 }
public enum PotionEffectVertical { Down = -1, None = 0, Up = 1 }