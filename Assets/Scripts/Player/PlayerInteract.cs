using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 3f;

    public LayerMask mask;
    private PlayerUI playerUI;

    private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        playerManager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInteractText();
        // Eventually add other interactable elements here
    }

    private void UpdateInteractText()
    {
        playerUI.UpdateText(string.Empty);
        //  Ray cast from center of camera
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo; // Store collision info

        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMsg);
                if (playerManager.onFoot.Interact.triggered)
                {
                    Debug.Log("Interacted with something interactable!");
                    interactable.BaseInteract();
                }
            }
        }
    }

}
