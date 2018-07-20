using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWheelController : MonoBehaviour
{
    [Header("Wheel Information")]
    [SerializeField] private List<AxleInfo> axleInfos;
    [SerializeField] public float maxMotorTorque;
    [SerializeField] private float maxSteeringAngle;
    [SerializeField] public float turnRate = 80f;
    [SerializeField] public float lineFollowDeviation = 0.1f;
    [SerializeField] private List<Transform> wheelModels;

    private float currSteerAngle = 0.0f;


    public void SetSteeringAngle(float angle)
    {
        if (angle != 0f)
            currSteerAngle = Mathf.Clamp(currSteerAngle + angle * Time.deltaTime, -maxSteeringAngle, maxSteeringAngle);
        else
            currSteerAngle = 0.0f;
        int i = 0;
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = currSteerAngle;
                axleInfo.rightWheel.steerAngle = currSteerAngle;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel, i++);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel, i++);
        }
    }

    public void SetMotorTorque(float motorTorque)
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motorTorque;
                axleInfo.rightWheel.motorTorque = motorTorque;
            }
        }
    }

    public void ApplyLocalPositionToVisuals(WheelCollider collider, int wheelIndex)
    {

        Transform visualWheel = wheelModels[wheelIndex];

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
}
