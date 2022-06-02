using UnityEngine;

public class CameraLock : MonoBehaviour
{
    [SerializeField]
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(0, player.position.y + 2, Camera.main.transform.position.z);
    }
}
