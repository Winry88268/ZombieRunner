using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] float defaultFOV = 60f;
    [SerializeField] float zoomFOV = 25f;
    [SerializeField] float zoomInSpeed = 0.5f;
    [SerializeField] float zoomOutSpeed = 0.5f;
    [SerializeField] float zoomOutSensitivity = 2f;
    [SerializeField] float zoomInSensitivity = 0.75f;

    [SerializeField] Canvas reticle;
    [SerializeField] Camera playerCamera;

    [SerializeField] GameObject weapon;
    [SerializeField] GameObject zoomed;

    Vector3 originPos;
    Quaternion originRot;
    RigidbodyFirstPersonController fpsController;

    private void Start() 
    {
        this.originPos = this.weapon.transform.localPosition;
        this.originRot = this.weapon.transform.rotation;
        this.fpsController = GetComponent<RigidbodyFirstPersonController>();
    }

    void Update() 
    {
        if(Input.GetMouseButton(1))
        {
            AimDownSights();
        }
        else
        {
            DoNotAimDownSights();
        }
    }

    private void AimDownSights()
    {
        if(Mathf.Approximately(this.playerCamera.fieldOfView, this.zoomFOV)) { return; }

        this.reticle.enabled = false;
        this.playerCamera.fieldOfView = Mathf.Lerp(this.playerCamera.fieldOfView, this.zoomFOV, 
                                                    Time.deltaTime * this.zoomInSpeed);
        this.weapon.transform.localPosition = this.zoomed.transform.localPosition;
        this.weapon.transform.localRotation = this.zoomed.transform.localRotation;
        this.fpsController.mouseLook.XSensitivity = this.zoomInSensitivity;
        this.fpsController.mouseLook.YSensitivity = this.zoomInSensitivity;
    }

    private void DoNotAimDownSights()
    {
        if(Mathf.Approximately(this.playerCamera.fieldOfView, this.defaultFOV)) { return; }

        this.reticle.enabled = true;
        this.playerCamera.fieldOfView = Mathf.Lerp(this.playerCamera.fieldOfView, this.defaultFOV,
                                                    Time.deltaTime * this.zoomOutSpeed);
        this.weapon.transform.localPosition = this.originPos;
        this.weapon.transform.localRotation = this.originRot;
        this.fpsController.mouseLook.XSensitivity = this.zoomOutSensitivity;
        this.fpsController.mouseLook.YSensitivity = this.zoomOutSensitivity;
    }
}
