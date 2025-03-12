using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class XRDeviceCheck : MonoBehaviour
{
    void Update()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeadMounted, devices);

        if (devices.Count > 0)
        {
            Debug.Log("HMD ����̽� ������: " + devices[0].name);
        }
        else
        {
            Debug.Log("HMD ����̽��� �������� ����.");
        }
    }
}
