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
        IngredientData ingData = IngredientLibrary.GetData(ingredient.ID);

        //if this is not the first ingredient in a chain, add effect based on dir of ingredient selected
        if (nodesByCoords.Count > 0) {
            Vector2Int selectedNodeCoords = currNodeCoords + new Vector2Int((int)ingData.HorizontalDir, (int)ingData.VerticalDir);
            
            //if these coords do not map to an existing node, fail potion
            //DESIGN: room for experimenting with some mechnanics here!
            if (!nodesByCoords.ContainsKey(selectedNodeCoords)) {
                Debug.Log($"Oops! Your potion didn't turn out!");
                ResetPotion();
                return;
            }

            potionProgress.Add(nodesByCoords[selectedNodeCoords]);
            currNodeCoords = selectedNodeCoords;
            RedrawProgressLine();
        }

        //render ingredient nodes
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

        //destroy added ingredient
        Destroy(ingredient.gameObject);
    }

    public void ResetPotion() {
        potionProgress.Clear();
        lineRenderer.positionCount = 0;
        foreach (Vector2Int key in nodesByCoords.Keys) {
            PotionEffectNode node = nodesByCoords[key];
            GameObject.Destroy(node.gameObject);
            nodesByCoords[key] = null;
        }
        nodesByCoords.Clear();

        currNodeCoords = Vector2Int.zero;
    }

    private void RedrawProgressLine() {
        lineRenderer.positionCount = potionProgress.Count;
        for (int i = 0; i < potionProgress.Count; i++){
            lineRenderer.SetPosition(i, potionProgress[i].transform.position);
            potionProgress[i].Highlight(true);
        }
    }

    public void FinishPotion(){

    }

}

//can prob collapse these enums into 1 axis type later, keeping them seperate for now for naming convenience
public enum PotionEffectHorizontal { Left = -1, None = 0, Right = 1 }
public enum PotionEffectVertical { Down = -1, None = 0, Up = 1 }