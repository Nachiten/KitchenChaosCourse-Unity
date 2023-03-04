using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateVisualPrefab;

    private List<GameObject> plateVisualsGameObjects;

    private void Awake()
    {
        plateVisualsGameObjects = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += OnPlateSpawned;
        platesCounter.OnPlateRemoved += OnPlateRemoved;
    }

    private void OnPlateRemoved()
    {
        // Remove the last plate visual
        GameObject plateGameObject = plateVisualsGameObjects[^1];
        plateVisualsGameObjects.Remove(plateGameObject);
        
        Destroy(plateGameObject);
    }

    private void OnPlateSpawned()
    {
        // Spawn plate visual on top of the last one
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        
        const float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualsGameObjects.Count, 0);
        plateVisualsGameObjects.Add(plateVisualTransform.gameObject);
    }
}
