using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionProgressUI : MonoBehaviour
{
    [SerializeField] private GameObject nodePrefab;
    [SerializeField] private float scaleCanvasUnit;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform progressChainOrigin;

    private List<string> ingredients = new List<string>();
    private Dictionary<Vector2Int, PotionEffectNode> nodesByCoords = new Dictionary<Vector2Int, PotionEffectNode>();

    private Vector2Int canvasOrigin;
    private Vector2Int currNodeCoords;
    private List<PotionEffectNode> potionProgress = new List<PotionEffectNode>();

    private void Awake() {
        canvasOrigin = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        currNodeCoords = new Vector2Int((int)progressChainOrigin.transform.position.x, (int)progressChainOrigin.transform.position.y);
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
            Vector2 posOnCanvas = canvasOrigin + ((Vector2)coords * scaleCanvasUnit);
            nodeRect.position = posOnCanvas;
        }

        //destroy added ingredient
        Destroy(ingredient.gameObject);
    }

    public void ResetPotion() {
        potionProgress.Clear();
        lineRenderer.positionCount = 0;

        List<PotionEffectNode> tmp = new List<PotionEffectNode>(nodesByCoords.Values);
        for (int i = tmp.Count - 1; i >= 0; i--) {
            Destroy(tmp[i].gameObject);
        }
        nodesByCoords.Clear();

        currNodeCoords = new Vector2Int((int)progressChainOrigin.transform.position.x, (int)progressChainOrigin.transform.position.y);
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