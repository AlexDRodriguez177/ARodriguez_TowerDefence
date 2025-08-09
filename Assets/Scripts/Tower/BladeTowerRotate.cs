using UnityEngine;

public class BladeTowerRotate : MonoBehaviour
{
    public Transform bladeRotatePoint;
    [SerializeField] private float rotationSpeed = 90f;
    void Update()
    {
        transform.RotateAround(bladeRotatePoint.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
