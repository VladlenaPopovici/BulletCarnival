using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraFollowPosition;
    [SerializeField] private AxisState xAxis;
    [SerializeField] private AxisState yAxis;
    
    void Update()
    {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);
    }

    private void LateUpdate()
    {
        var localEulerAngles = cameraFollowPosition.localEulerAngles;
        cameraFollowPosition.localEulerAngles = new Vector3(yAxis.Value, localEulerAngles.y, localEulerAngles.z);;
        
        var eulerAngles = transform.eulerAngles;
        transform.eulerAngles = new Vector3(eulerAngles.x, xAxis.Value, eulerAngles.z);
    }
}
