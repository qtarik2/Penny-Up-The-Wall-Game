using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;        //Public variable to store a reference to the player game object
    public float lerpSpeed;
    public Vector3 offsetVU;
    private Vector3 offset;            //Private variable to store the offset distance between the player and camera
    GameObject _mainCamera;
    Vector3 _startPosition;
    public float FallingThreshold = -20f;
    public bool startAnim;

    void Start()
    {
        //  Invoke("Finding", 3);
        _mainCamera = Camera.main.gameObject;
        this.transform.position = _mainCamera.transform.position;
        this.transform.rotation = _mainCamera.transform.rotation;
        offset = transform.position - player.transform.position;
        offset += offsetVU;
        _startPosition = this.transform.position;
    }
    void LateUpdate()
    {

        if (player != null)
        {
            transform.position = Vector3.MoveTowards(_startPosition, player.transform.position + offset, lerpSpeed);
            transform.LookAt(player.transform);

        }

    }
}
