using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaceOnPlane : MonoBehaviour //처음 실행 시, 오목판을 위치 시키기
{
    public ARRaycastManager arRaycaster;
    public GameObject placeObj;
    private bool isDo;
    void Start()
    {
        isDo = false;
    }

    void Update()
    {
        if (Input.touchCount > 0 && isDo == false)
        {
            PlaceBoardByTouch();
        }

    }
    private void PlaceBoardByTouch()
    {
        Touch touch = Input.GetTouch(0);
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (arRaycaster.Raycast(touch.position, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;
            Instantiate(placeObj, hitPose.position, hitPose.rotation);
            isDo = true;

        }
    }
    
}