using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // Add or remove component to gameobject
    public bool useEvents;

    [SerializeField]
    public string promptMsg;

    public virtual string OnLook() { return promptMsg; }

    public void BaseInteract()
    {
        if(useEvents)
        {
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        }
        Interact();
    }

    protected virtual void Interact()
    {

    }
}
