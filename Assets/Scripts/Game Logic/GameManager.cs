using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField] ConfigSO configFile;
    public static GameManager Instance { get; private set; }

    private void Awake() {
        if (Instance != null) {
            Debug.LogWarning($"Destroyed a newer instance of {nameof(GameManager)} on {gameObject.name} to preserve an earlier one on {Instance.gameObject.name}.");
            DestroyImmediate(this);
            return;
        }

        Instance = this;

        InitializeGameRun(); //NOTE: will move this to a LoadGame method after home & loading screens are implemented
    }

    private static void InitializeGameRun() {

        ConfigSO configInstance = ConfigSO.Instance;

        //initialize game logic static classes & singletons
        IngredientLibrary.Init(configInstance);
    }
}
