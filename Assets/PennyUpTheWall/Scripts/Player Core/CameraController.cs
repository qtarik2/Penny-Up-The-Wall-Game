using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;        //Public variable to store a reference to the player game object
    public GameObject playerModel;        //Public variable to store a reference to the player game object
    public float lerpSpeed;
    private Vector3 offset;            //Private variable to store the offset distance between the player and camera
    public Transform followPoint;

    GameObject _mainCamera;

    Vector3 _startPosition;

    public bool isBoss;
    public bool lookAtBoss;

    void Start()
    {
        //  Invoke("Finding", 3);
        _mainCamera = Camera.main.gameObject;
        this.transform.position = _mainCamera.transform.position;
        this.transform.rotation = _mainCamera.transform.rotation;
        offset = transform.position - player.transform.position;
        _startPosition = this.transform.position;
        this.transform.parent = null;
        playerModel = Spawner.instance.playerModel;
    }
    void LateUpdate()
    {
        if (player != null && !isBoss)
        {
            transform.position = Vector3.MoveTowards(_startPosition, player.transform.position + offset, lerpSpeed);
            transform.LookAt(player.transform);
        }
        if (isBoss)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition,LevelSelection.instance.camF.transform.localPosition,Mathf.Sin(0.1f));
            transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, LevelSelection.instance.camF.transform.localEulerAngles, Mathf.Sin(0.1f));
            if (lookAtBoss) { transform.LookAt(player.transform); }
        }
        /*if (player != null && LevelSelection.instance.canRotate)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + new Vector3(0,0.6f,0),Mathf.Sin(0.1f));
            transform.localEulerAngles = Vector3.zero;
            transform.LookAt(playerModel.transform);
        }*/
    }
}
