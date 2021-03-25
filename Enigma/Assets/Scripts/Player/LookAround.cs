//source: https://craftgames.co/unity-3d-fps-movement/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    public Interactable focus;

    public Transform cam;
    public new Camera camera;

    float sensitivity = 300f;
    float headRotation = 0f;
    float headRotationLimit = 90f;

    // Update is called once per frame

    void Start()
    {
        camera = Camera.main;
    }
    void Update()
    {
        float x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime * -1f;
        transform.Rotate(0f, x, 0f);
        headRotation += y;
        headRotation = Mathf.Clamp(headRotation, -headRotationLimit, headRotationLimit);
        cam.localEulerAngles = new Vector3(headRotation, 0f, 0f);


        if (Input.GetMouseButtonDown(0))
        {
            //create a ray 
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //
            if(Physics.Raycast(ray, out hit, 100))
            {
                //Debug.Log("Ray Hit");
                //check if we hit an interacbtable 
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    //Debug.Log("Setting the focus");
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        if(newFocus != focus)
        {
            if(focus != null)
            {
                focus.OnDefocused();
            }

            focus = newFocus;

        }

        //Debug.Log("focus set");
        focus = newFocus;
        newFocus.OnFocused(transform);
    }
}
