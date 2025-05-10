using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct InfoPanelData {
    public string DisplayName;
    public string Description;
    public Sprite Image;
}

public class HUD : MonoBehaviour
{
    [Header("Info Panel")]
    [SerializeField] private GameObject infoPanelRoot;
    [SerializeField] private TMP_Text infoName;
    [SerializeField] private TMP_Text infoDesc;
    [SerializeField] private Image infoImg;

    public static HUD Instance { get; private set; }

    private static InfoPanelData currInfoPanelData;
    private static Dictionary<string, InfoPanelData> itemInfo = new Dictionary<string, InfoPanelData>();

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning($"Destroying an instance of {nameof(HUD)} on {gameObject.name} to preserve an earlier instance on {Instance.gameObject.name}.");
            DestroyImmediate(this);
            return;
        }
        Instance = this;

        HideInfoPanel();
    }

    public static void SetInfoPanelData(string infoKey) {
        currInfoPanelData = itemInfo[infoKey];

        Instance.infoPanelRoot.gameObject.SetActive(true);
        Instance.infoName.SetText(currInfoPanelData.DisplayName);
        Instance.infoDesc.SetText(currInfoPanelData.Description);

        Instance.infoImg.enabled = currInfoPanelData.Image != null;
        if (currInfoPanelData.Image != null) Instance.infoImg.sprite = currInfoPanelData.Image;
    }

    public static void HideInfoPanel() {
        Instance.infoPanelRoot.SetActive(false);
    }

    public static void InitItemInfo(InfoSO itemInfoFile) {
        Instance.ReadDataFromConfig(itemInfoFile.ItemInfo);
    }
    private void ReadDataFromConfig(InfoPanelData[] configData) {
        itemInfo.Clear();
        foreach (InfoPanelData info in configData) {
            itemInfo.Add(info.DisplayName, info);
        }
    }

#if UNITY_EDITOR
    public static string[] GetItemInfoIDs_EditorOnly() {
        List<string> ids = new List<string>();
        foreach (InfoPanelData info in ConfigSO.Instance.ItemInfoData.ItemInfo) {
            ids.Add(info.DisplayName);
        }

        ids.Sort();
        return ids.ToArray();
    }
#endif

}
