using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [Header("InteractableText")] [SerializeField]
    public string text;

    [Header("Interactable DEBUG")] [SerializeField]
    public bool isPlayerInTrigger = false;

    private PlayerController playerControls;
    private PlayerController.InteractionActions abilitiesActions;
    protected TextMeshProUGUI textMesh;

    public void Start()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        textMesh.text = text;
        OnStart();
    }

    public virtual void OnStart()
    {
        
    }
    private void OnEnable()
    {
        playerControls = new PlayerController();
        playerControls.Enable();
        abilitiesActions = playerControls.Interaction;
        abilitiesActions.Enable();
        abilitiesActions.Interact.performed += OnInteraction;
    }

    private void OnDisable()
    {
        abilitiesActions.Disable();
        playerControls.Disable();
    }

    public void OnPlayerTriggerChange(bool state)
    {
        isPlayerInTrigger = state;
        textMesh.enabled = state;
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (!isPlayerInTrigger)
        {
            return;
        }

        HandleInteraction();
    }

    public virtual void HandleInteraction()
    {
    }
}