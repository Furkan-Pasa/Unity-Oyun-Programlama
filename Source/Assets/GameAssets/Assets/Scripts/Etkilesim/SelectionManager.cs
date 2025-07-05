using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; set; }

    public InteractableObject selectedObject;
    public NPC_Interact selectedNPC;

    public bool onTarget;

    public GameObject interaction_Info_UI;
    TextMeshProUGUI interaction_text;   //Text interaction_text;

    private float raycastInterval = 0.1f;  // 0.1 saniyede bir
    private float nextRaycast = 0f;
    private Camera mainCamera;             // Camera.main cache'i

    private void Start()
    {
        mainCamera = Camera.main;
        onTarget = false;
        //interaction_text = interaction_Info_UI.GetComponent<Text>();
        interaction_text = interaction_Info_UI.GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (Time.time >= nextRaycast)
        {
            nextRaycast = Time.time + raycastInterval;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var selectionTransform = hit.transform;

                // Önce InteractableObject kontrol et
                InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();

                if (interactable != null)
                {
                    float distanceToObject = hit.distance;

                    // Mesafe kontrolü
                    if (distanceToObject <= interactable.interactionDistance)
                    {
                        onTarget = true;
                        selectedObject = interactable;
                        selectedNPC = null;  // NPC deðil
                        interaction_text.text = interactable.GetItemName();
                        interaction_Info_UI.SetActive(true);
                    }
                    else
                    {
                        ResetSelection();
                    }
                }
                else
                {
                    // NPC kontrol et
                    NPC_Interact npc = selectionTransform.GetComponent<NPC_Interact>();
                    if (npc != null)
                    {
                        float distanceToNPC = hit.distance;
                        if (distanceToNPC <= npc.interactionDistance)
                        {
                            onTarget = true;
                            selectedObject = null;  // Item deðil
                            selectedNPC = npc;
                            interaction_text.text = npc.Get_NPC_Ismi();
                            interaction_Info_UI.SetActive(true);
                        }
                        else
                        {
                            ResetSelection();
                        }
                    }
                    else
                    {
                        ResetSelection();
                    }
                }
            }
            else
            {
                ResetSelection();
            }
        }
    }

    void ResetSelection()
    {
        onTarget = false;
        selectedObject = null;
        selectedNPC = null;
        interaction_Info_UI.SetActive(false);
    }
}