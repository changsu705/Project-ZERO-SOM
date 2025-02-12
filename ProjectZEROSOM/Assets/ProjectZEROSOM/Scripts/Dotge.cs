//각속도 데이터

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Dotge : MonoBehaviour
{
    private InputDevice hmdDevice;

    void Start()
    {
        // HMD 장치를 찾습니다. (컨트롤러나 다른 디바이스의 자이로 데이터를 원한다면 해당 특성으로 필터링 가능)
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeadMounted, devices);
        // GetDevicesWithCharacteristics 장치를 찾습니다.(InputDeviceCharacteristics.HeadMounted 인풋 디바이스 사용장치중에 해드 마운티드(헤드셋)을
        if (devices.Count > 0)
        {
            hmdDevice = devices[0];
        }
    }

    void Update()
    {
        if (hmdDevice.isValid)
        {
            // 자이로스코프에 해당하는 회전 속도 데이터를 가져옵니다.
            if (hmdDevice.TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out Vector3 angularVelocity))
            // hmdDevice.TryGetFeatureValue hmdDevice에서 값을 받아오는것을 시도합니다. CommonUsages.deviceAngularVelocity 장치의 각속도를 out Vector3 angularVelocity 그것을 여기에 저장합니다. 해당 TryGetFeatureValue 문은 true false를 반환하기때문에 성공 여부를 파악 할 수 있다.
            {
                Debug.Log("Angular Velocity: " + angularVelocity);
                //이후 여기에 벡터 방향 검출후 그 방향으로 포스 추가하는 구문 넣어야함.
            }
        }
    }
}
