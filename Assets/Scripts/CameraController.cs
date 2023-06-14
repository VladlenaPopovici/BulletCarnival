using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraFollowPosition;
    [SerializeField] private float mouseSense = 1f;
    private float _xAxis;
    private float _yAxis;
    
    void Update()
    {
        _xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
        _yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;

        _yAxis = Mathf.Clamp(_yAxis, -80, 80);
    }

    private void LateUpdate()
    {
        var localEulerAngles = cameraFollowPosition.localEulerAngles;
        cameraFollowPosition.localEulerAngles = new Vector3(_yAxis, localEulerAngles.y, localEulerAngles.z);;
        
        var eulerAngles = transform.eulerAngles;
        transform.eulerAngles = new Vector3(eulerAngles.x, _xAxis, eulerAngles.z);
    }
}
