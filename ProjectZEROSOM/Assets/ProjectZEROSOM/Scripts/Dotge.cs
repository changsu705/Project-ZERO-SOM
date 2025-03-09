using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Dotge : MonoBehaviour
{
    [Header("Input Settings")]
    [SerializeField]
    private InputActionProperty leftTriggerAction; // 왼쪽 트리거 입력

    [Header("Dodge Settings")]
    public float forceAmount = 5f; // 추가할 힘의 크기
    public float threshold = 0.5f; // 최소 각속도 값 (이 값 이상일 때 힘 추가)
    public float dodgeCooldown = 1f; // 닷지 쿨타임 (초)
    private bool canDodge = true; // 닷지 가능 여부

    private UnityEngine.XR.InputDevice hmdDevice;
    public Rigidbody rb;
    private ContinuousMoveProviderBase locomotionSystem; // 로코모션 시스템
    private float locomotionSpeed; // 로코모션 이동 속도

    void Start()
    {
        // Rigidbody 가져오기
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            GameObject xrOrigin = GameObject.Find("XR Origin (XR Rig)");
            if (xrOrigin != null)
            {
                rb = xrOrigin.GetComponent<Rigidbody>();
            }
            Debug.Log(xrOrigin);
            return;
        }

        // HMD 장치 찾기
        var devices = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeadMounted, devices);

        if (devices.Count > 0)
        {
            hmdDevice = devices[0];
            if (!hmdDevice.isValid)
            {
                Debug.Log("헤드업 마운티드 찾을수 없음");
            }
        }
        else
        {
            Debug.Log("연결된 장치 찾을수 없음");
        }

        locomotionSystem = FindObjectOfType<ContinuousMoveProviderBase>();
        if (locomotionSystem == null)
        {
            Debug.LogWarning("Continuous Move Provider가 씬에서 발견되지 않음. 로코모션 없이 작동할 수도 있음.");
        }
        Debug.Log("모든기능 테스트");
    }

    void Update()
    {
        if(leftTriggerAction.action?.ReadValue<float>() > 0.8f)
        {
            Debug.Log("레프트 컨트롤러 트리거눌림");
        }
            if (canDodge && leftTriggerAction.action?.ReadValue<float>() > 0.8f)
            {
            if (hmdDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceAngularVelocity, out Vector3 angularVelocity))
            {
                // 특정 값 이상일 때만 힘 추가
                if (angularVelocity.magnitude > threshold)
                {
                    StartCoroutine(ApplyForce(angularVelocity));
                    Debug.Log($"닷지 성공! 각속도 검출값 X: {angularVelocity.x}, Y: {angularVelocity.y}, Z: {angularVelocity.z}");
                }
            }
            else
            {
                Debug.Log("해드셋 각속도값 받아올 수 없음.");
            }
            }
    }

    IEnumerator ApplyForce(Vector3 angularVelocity)
    {
        canDodge = false;
        if (rb == null) yield break;

        if (locomotionSystem != null)
        {
            locomotionSystem.enabled = false;
            rb.isKinematic = false;
        }

        angularVelocity.y = 0;

        // forceDirection 힘의 방향 각속도를 정규화하여 사용
        Vector3 forceDirection = angularVelocity.normalized;

        // Rigidbody에 힘 추가
        rb.AddForce(forceDirection * forceAmount, ForceMode.Impulse);
        Debug.Log($"닷지 성공! 닷지 방향 X: {forceDirection.x}, Y: {forceDirection.y}, Z: {forceDirection.z} \n닷지 힘 {forceAmount}");

        locomotionSpeed = locomotionSystem.moveSpeed;
        float elapsedTime = 0f;
        while (elapsedTime < dodgeCooldown)
        {
            elapsedTime += Time.deltaTime;

            // 만약 닷지 속도가 로코모션 속도보다 느려지면 즉시 로코모션 활성화
            if (rb.velocity.magnitude <= locomotionSpeed)
            {
                if (locomotionSystem != null && !locomotionSystem.enabled)
                {
                    locomotionSystem.enabled = true;
                    rb.isKinematic = true;
                }
                else
                {
                    locomotionSystem.enabled = true;
                    rb.isKinematic = true;
                    Debug.Log("로코모션오류발생 작동안함 비상 시퀸스로 로코모션으로 강제변경 .");
                }
                break; // 루프 탈출
            }
            yield return null;
        }

        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true; // 쿨타임 후 다시 닷지 가능
    }
}
