//Copyright 2012 Maximilian Wolfgang Maroe, feel free to use or modify this script at your leisure
//This script sets it up so that when you click or touch, it tries to find an object in the scene
//that is in the direction of where you clicked, and sends a "Clicked()" message to the object.
//Supports touch (And multitouch!)

using UnityEngine;
using System.Collections;

public class DetectClicksAndTouches : MonoBehaviour
{
    //This variable is optional; if not set it will default to the main camera
    //This is so that you can detect clicks/touches on a separate UI Camera
    //This variable does NOT update in real time
    new public Camera camera;
    public GameObject plane = null;

    public LayerMask mask = -1;

    //This variable adds a Debug.Log call to show what was touched
    public bool debug = false;

    //This is the actual camera we reference in the update loop, set in Start()
    private Camera _camera;

    Vector3 dragStartPos = Vector3.zero;
    Vector3 cameraStartPos = Vector3.zero;

    Vector2 worldStartPoint;

    void Start()
    {
        if (camera != null)
        {
            _camera = camera;
        }
        else {
            _camera = Camera.main;
        }
    }

    void Update()
    {
        Ray ray;
        RaycastHit hit;

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    ray = _camera.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                    {
                        if (debug)
                        {
                            Debug.Log("You touched " + hit.collider.gameObject.name, hit.collider.gameObject);
                        }

                        //hit.transform.gameObject.SendMessage("Clicked", SendMessageOptions.DontRequireReceiver);
                        hit.transform.gameObject.SendMessage("Clicked", hit.point, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
        else {
            if (Input.GetMouseButtonDown(0)) // we are on a computer (more than likely)
            {
                // set up our ray from screen to scene
                ray = _camera.ScreenPointToRay(Input.mousePosition); // check to see if we've clicked

                // if we hit
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
                {
                    // tell the system what we clicked if in debug
                    if (debug)
                    {
                        Debug.Log("You clicked " + hit.collider.gameObject.name, hit.collider.gameObject);
                    }

                    // run the Clicked() function on the clicked object
                    //hit.transform.gameObject.SendMessage("Clicked", SendMessageOptions.DontRequireReceiver);
                    hit.transform.gameObject.SendMessage("Clicked", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }
#if _abcavse_
            else if (Input.GetMouseButtonDown(1))
            {
                // right: drag camera
                dragStartPos = Input.mousePosition;
                cameraStartPos = _camera.transform.position;
            }
            else if (Input.GetMouseButtonUp(1))
                dragStartPos = Vector3.zero;
            else if (Input.GetMouseButton(1)) // (dragStart != Vector3.zero)
            {
                var diff = Input.mousePosition - dragStartPos;
                //if (plane == null)
                //diff.z = -10;
                diff = _camera.ScreenToViewportPoint(diff);
                //else
                //    diff = plane.
                //diff = _camera.ScreenToWorldPoint(diff);
                var scale = 1f;
                //var scale = _camera.transform.position.y - 0; // 0: y of plane
                //scale *= 1.5f;
                var scalex = scale;
                var scaley = scale;
                //var scaley = scale * (1 - Mathf.Cos(80 * Mathf.Deg2Rad * 80));
                //var scaley = -scale * Mathf.Sin(80 * Mathf.Deg2Rad * 80); // _camera.transform.rotation.x);
                diff.z = diff.y * scaley;
                diff.y = 0;
                diff.x = diff.x * scalex;
                _camera.transform.position = cameraStartPos - diff;
            }
#endif
            // http://stackoverflow.com/a/27836741/586754
            else if (Input.GetMouseButtonDown(1))
            {
                this.worldStartPoint = this.getWorldPoint(Vector3to2(Input.mousePosition));
            }
            //else if (Input.GetMouseButtonUp(1))
            //    dragStartPos = Vector3.zero;
            else if (Input.GetMouseButton(1)) // (dragStart != Vector3.zero)
            {
                try
                {
                Vector2 worldDelta = this.getWorldPoint(Vector3to2(Input.mousePosition)) - this.worldStartPoint;

                Camera.main.transform.Translate(
                    -worldDelta.x,
                    -worldDelta.y,
                    0
                );

                }
                catch (System.Exception)
                {
                }
            }
        }
    }

    Vector2 Vector3to2(Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }

    // convert screen point to world point
    private Vector2 getWorldPoint(Vector2 screenPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(screenPoint), out hit))
            return new Vector2(hit.point.x, hit.point.z);
        else
            throw new System.Exception();
            //return Vector2.;
    }
}