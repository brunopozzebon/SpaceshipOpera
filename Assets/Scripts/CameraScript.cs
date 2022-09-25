using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position;
        transform.rotation = target.transform.rotation;
    }
}
