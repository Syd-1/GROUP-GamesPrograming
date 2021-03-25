// sourced from: https://www.youtube.com/watch?v=9tePzyL6dgc
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform interactableTramsform;

    bool isFocus = false;
    public Transform player;

    bool hasInteracted = false;

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + transform.name);
    }

    void Update()
    {
        // if we are currnelty being focused
        //and we havnt already interacted with the object 
        if(isFocus && !hasInteracted)
        {
            //Debug.Log("Curretly focused on object");
            //if we are close enough
            float distance = Vector3.Distance(player.position, interactableTramsform.position);
            if (distance <= radius)
            {
                //Debug.Log("Interact");
                //interact with the object 
                Interact();
                hasInteracted = true;
            }
        }
    }
    //called whne the objevt starts being focused 
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }
    //called whne the object is no longer focused 
    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
    //draws a radius in the editor 
    void OnDrawGizmosSelected ()
    {
        if(interactableTramsform == null)
        {
            interactableTramsform = transform;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactableTramsform.position, radius);
    }

}
