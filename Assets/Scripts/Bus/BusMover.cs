using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusMover : MonoBehaviour
{
    [SerializeField]
    private WheelCollider leftFrontWheelCollider, rightFrontWheelCollider, leftBackWheelCollider, rightBackWheelCollider;
    [SerializeField]
    private Transform leftFrontWheelTransform, rightFrontWheelTransform, leftBackWheelTransform, rightBackWheelTransform;

    [SerializeField]
    private float motorForce;
    [SerializeField]
    private float breakForce;
    [SerializeField]
    private float maxSteeringAngle;

    private float horizontalInput;
    private float verticalInput;
    private float currentBerakForce;
    private float steeringAngle;

    private bool isBreaking;
    private void FixedUpdate()
    {
        GetInputs();
        HandleMotor();
        HandleSteering();
        UpdateWheelVisual();
    }
    private void GetInputs()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }
    private void HandleMotor()
    {
        leftFrontWheelCollider.motorTorque = verticalInput * motorForce;
        rightFrontWheelCollider.motorTorque = verticalInput * motorForce;
        currentBerakForce = isBreaking ? breakForce : 0;

        ApplyBreakForce();

    }

    private void ApplyBreakForce()
    {
        leftFrontWheelCollider.brakeTorque = currentBerakForce;
        leftBackWheelCollider.brakeTorque = currentBerakForce;
        rightBackWheelCollider.brakeTorque = currentBerakForce;
        rightFrontWheelCollider.brakeTorque = currentBerakForce;
    }
    private void HandleSteering()
    {
        steeringAngle = horizontalInput * maxSteeringAngle;
        leftFrontWheelCollider.steerAngle = steeringAngle;
        rightFrontWheelCollider.steerAngle = steeringAngle;
    }

    private void UpdateWheelVisual()
    {
        UpdateSingleWheelVisual(leftFrontWheelCollider, leftFrontWheelTransform);
        UpdateSingleWheelVisual(leftBackWheelCollider, leftBackWheelTransform);
        UpdateSingleWheelVisual(rightFrontWheelCollider, rightFrontWheelTransform);
        UpdateSingleWheelVisual(rightBackWheelCollider, rightBackWheelTransform);
    }

    private void UpdateSingleWheelVisual(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
}
