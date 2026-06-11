using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private Vector3 cam_offset;
    public Transform playerPos;

    private void Start()
    {
        cam_offset = transform.position;
    }

    private void Update()
    {
        Vector3 normalPos = new Vector3(playerPos.position.x,0,playerPos.position.z);
        transform.position = normalPos + cam_offset;
    }

}
