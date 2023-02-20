using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaceOnPlane : MonoBehaviour
{
    public ARRaycastManager arRaycaster;
    public GameObject placeObj;
    private GameObject spawnObject;
    private bool isDo;
    private bool isSpawned; 
    private bool whatColor;
    public GameObject black;
    public GameObject white;

    void Start()
    {
        isDo = false;
        whatColor = false;
    }

    void Update()
    {
        if (Input.touchCount > 0 && isDo == false)
        {
            PlaceBoardByTouch();

        }
        else if (Input.touchCount > 0 && isDo == true)
        {
            PlaceBWByTouch();
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
    private void PlaceBWByTouch()
    {

       /* Touch touch = Input.GetTouch(0);
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (arRaycaster.Raycast(touch.position, hits, TrackableType.Planes))
        {
            Pose hitPose = hits[0].pose;
            if (whatColor)
            {
                Instantiate(black, hitPose.position, hitPose.rotation);
                whatColor = false;
            }
            else
            {
                Instantiate(white, hitPose.position, hitPose.rotation);
                whatColor = true;
            }

        }*/
       /* RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);

        if (hit.collider != null)
        {
            GameObject CurrentTouch = hit.transform.gameObject;

            if (whatColor)
            {
                Instantiate(black, CurrentTouch.transform);
                whatColor = false;
            }
            else
            {
                Instantiate(white, CurrentTouch.transform);
                whatColor = true;
            }
           
        }*/

    }
    
}