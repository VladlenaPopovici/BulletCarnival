using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] private Transform recoilFollowPosition;
    [SerializeField] private float kickBackAmount = -1;
    [SerializeField] private float kickBackSpeed = 10;
    [SerializeField] private float returnSpeed = 20;

    private float _currentRecoilPosition;
    private float _finalRecoilPosition;
    
    void Update()
    {
        _currentRecoilPosition = Mathf.Lerp(_currentRecoilPosition, 0, returnSpeed * Time.deltaTime);
        _finalRecoilPosition = Mathf.Lerp(_finalRecoilPosition, _currentRecoilPosition, kickBackSpeed * Time.deltaTime);
        recoilFollowPosition.localPosition = new Vector3(0, 0, _finalRecoilPosition);
    }

    public void TriggerRecoil() => _finalRecoilPosition += kickBackAmount;
}
