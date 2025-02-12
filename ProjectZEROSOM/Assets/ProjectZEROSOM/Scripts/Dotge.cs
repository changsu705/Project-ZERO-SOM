//���ӵ� ������

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Dotge : MonoBehaviour
{
    private InputDevice hmdDevice;

    void Start()
    {
        // HMD ��ġ�� ã���ϴ�. (��Ʈ�ѷ��� �ٸ� ����̽��� ���̷� �����͸� ���Ѵٸ� �ش� Ư������ ���͸� ����)
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeadMounted, devices);
        // GetDevicesWithCharacteristics ��ġ�� ã���ϴ�.(InputDeviceCharacteristics.HeadMounted ��ǲ ����̽� �����ġ�߿� �ص� ����Ƽ��(����)��
        if (devices.Count > 0)
        {
            hmdDevice = devices[0];
        }
    }

    void Update()
    {
        if (hmdDevice.isValid)
        {
            // ���̷ν������� �ش��ϴ� ȸ�� �ӵ� �����͸� �����ɴϴ�.
            if (hmdDevice.TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out Vector3 angularVelocity))
            // hmdDevice.TryGetFeatureValue hmdDevice���� ���� �޾ƿ��°��� �õ��մϴ�. CommonUsages.deviceAngularVelocity ��ġ�� ���ӵ��� out Vector3 angularVelocity �װ��� ���⿡ �����մϴ�. �ش� TryGetFeatureValue ���� true false�� ��ȯ�ϱ⶧���� ���� ���θ� �ľ� �� �� �ִ�.
            {
                Debug.Log("Angular Velocity: " + angularVelocity);
                //���� ���⿡ ���� ���� ������ �� �������� ���� �߰��ϴ� ���� �־����.
            }
        }
    }
}
