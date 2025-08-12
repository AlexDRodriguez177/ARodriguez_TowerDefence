using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TowerPlaceManager : MonoBehaviour
{
    public Camera MainCamera;
    public LayerMask TileLayer;
    public InputAction PlaceTowerAction;
    [SerializeField] private float towerPlacementHeight = -1f;
    [SerializeField] private bool isPlacingTower = false;
    private GameObject currentTowerToSpawn;
    private GameObject towerPreview;
    private Vector3 towerPlacmentPosition;
    private bool isTileSelected = false;

    [SerializeField] private int towerCost;
    private void Update()
    {
        if (isPlacingTower)
        {
            Ray ray = MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, TileLayer))
            {
                towerPlacmentPosition = hitInfo.transform.position + Vector3.up * towerPlacementHeight;
                towerPreview.transform.position = towerPlacmentPosition;
                towerPreview.SetActive(true);
                isTileSelected = true;
            }
            else
            {
                towerPreview.SetActive(false);
                isTileSelected = false;
            }

        }
    }
    private void OnEnable()
    {
        PlaceTowerAction.Enable();
        PlaceTowerAction.performed += OnPlaceTower;
    }

    private void OnDisable()
    {
        PlaceTowerAction.performed -= OnPlaceTower;
        PlaceTowerAction.Disable();
    }

    public void StartPlacingTower(GameObject towerPrefab)
    {

        if ((currentTowerToSpawn != towerPrefab))
        {
            isPlacingTower = true;
            currentTowerToSpawn = towerPrefab;
            if ((towerPreview != null))
            {
                Destroy(towerPreview);
            }

            towerPreview = Instantiate(currentTowerToSpawn);

            var baseTowerScript = towerPreview.GetComponent<Tower>();
            if (baseTowerScript != null)
            {
                baseTowerScript.enabled = false;
            }

            var bladeTowerScript = towerPreview.GetComponent<BladeTower>();
            if (bladeTowerScript != null)
            {
                bladeTowerScript.enabled = false;
            }

        }


    }


    private void OnPlaceTower(InputAction.CallbackContext context)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (GameManager.Instance.coins < towerCost)
        {
            Debug.Log("Not enough coins to place tower.");
            return;
        }

        else if (isPlacingTower && isTileSelected)
        {
            GameManager.Instance.SpendCoins(towerCost);
            isPlacingTower = false;
            Vector3 spawnPosition = towerPreview.transform.position;
            Instantiate(currentTowerToSpawn, spawnPosition, Quaternion.identity);
            Destroy(towerPreview);
            currentTowerToSpawn = null;
        }
    }
}


