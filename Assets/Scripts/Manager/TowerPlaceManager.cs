using UnityEngine;
using UnityEngine.InputSystem;

public class TowerPlaceManager : MonoBehaviour
{
    public Camera MainCamera;
    public LayerMask TileLayer;
    public InputAction PlaceTowerAction;
    [SerializeField] private float towerPlacementHeight = 0.5f;
    [SerializeField] private bool isPlacingTower = false;
    private GameObject currentTowerToSpawn;
    private GameObject towerPreview;
    private Vector3 towerPlacmentPosition;
    private bool isTileSelected=false;
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
            currentTowerToSpawn.GetComponent<MonoBehaviour>().enabled = false;
        }

    }

    private void OnPlaceTower(InputAction.CallbackContext context)
    {
        if (isPlacingTower && isTileSelected)
        {
            isPlacingTower = false;
            Instantiate(currentTowerToSpawn, towerPreview.transform.position, Quaternion.identity);
            Destroy(towerPreview);
            currentTowerToSpawn = null;
            currentTowerToSpawn.GetComponent<MonoBehaviour>().enabled = true;
        }
    }
}
