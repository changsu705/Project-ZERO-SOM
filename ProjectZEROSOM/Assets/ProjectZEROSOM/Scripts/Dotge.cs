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
    public Transform transformMainCamera;

    [Header("Dodge Settings")]
    public float dodgeSpeed = 5f; // 닷지 속도
    public float threshold = 0.5f; // 최소 각속도 값 (이 값 이상일 때 닷지)
    public float dodgeCooldown = 3f; // 닷지 쿨타임 (초)
    private bool canDodge = true; // 닷지 가능 여부
    public float dodgeDuration = 0.5f; // 닷지 지속시간
    private float currentSpeed = 0f;

    private UnityEngine.XR.InputDevice hmdDevice;
    private ContinuousMoveProviderBase locomotionSystem; // 로코모션 시스템
    public CharacterController characterController;
    private bool isdevices = false;

    void Start()
    {
        locomotionSystem = FindObjectOfType<ContinuousMoveProviderBase>();
        if (locomotionSystem == null)
        {
            Debug.LogWarning("Continuous Move Provider가 씬에서 발견되지 않음. 로코모션 없이 작동할 수도 있음.");
        }
        Debug.Log("모든기능 테스트");
    }

    void Update()
    {
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.HeadMounted, devices);
        if (isdevices == false)
        {
            if (devices.Count > 0)
            {
                Debug.Log("HMD 디바이스 감지됨: " + devices[0].name);
                hmdDevice = devices[0];
                Debug.Log("hmdDevice name: " + hmdDevice);
                isdevices = true;
            }
        }

        if (leftTriggerAction.action?.ReadValue<float>() > 0.8f)
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
                    StartCoroutine(ApplyDodge(angularVelocity));
                    Debug.Log($"닷지 성공! 각속도 검출값 X: {angularVelocity.x}, Y: {angularVelocity.y}, Z: {angularVelocity.z}");
                }
                else
                {
                    Debug.Log($"각속도 검출값 X: {angularVelocity.x}, Y: {angularVelocity.y}, Z: {angularVelocity.z}");
                }
            }
        }
    }

    IEnumerator ApplyDodge(Vector3 angularVelocity)
    {
        canDodge = false;
        Vector3 LocalangularVelocity = transformMainCamera.TransformDirection(angularVelocity);

        // angularVelocity y 축 삭제
        LocalangularVelocity.y = 0;
        // forceDirection 힘의 방향 각속도를 정규화하여 사용
        Vector3 forceDirection = LocalangularVelocity.normalized;
        Debug.Log($"닷지 방향 X: {forceDirection.x}, Y: {forceDirection.y}, Z: {forceDirection.z}");
        forceDirection = Quaternion.Euler(0, -90, 0) * forceDirection;


        float elapsedTime = 0f;
        while (elapsedTime < dodgeDuration)
        {
            elapsedTime += Time.deltaTime;

            float progress = elapsedTime / dodgeDuration;
            float speedFactor = Mathf.Lerp(0, 1, Mathf.Sin(progress * Mathf.PI));

            currentSpeed = dodgeSpeed * speedFactor;
            Vector3 moveDirection = forceDirection * currentSpeed * Time.deltaTime;
            characterController.Move(moveDirection);
            yield return null;

            CollisionFlags flags = characterController.Move(moveDirection);
            if ((flags & CollisionFlags.Sides) != 0) // 벽에 충돌했는지 확인
            {
                break;
            }
        }

        yield return new WaitForSeconds(dodgeCooldown);
        canDodge = true; // 쿨타임 후 다시 닷지 가능
    }
}
