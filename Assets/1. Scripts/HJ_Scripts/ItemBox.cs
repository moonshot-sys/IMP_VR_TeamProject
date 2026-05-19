using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ItemBox : MonoBehaviour
{
    [HideInInspector]
    public BoxSpawner boxSpawner;
    private bool itemTaken = false;

    void Start()
    {
        if (boxSpawner == null)
        {
            boxSpawner = FindObjectOfType<BoxSpawner>();
        }
        
        XRGrabInteractable[] items = GetComponentsInChildren<XRGrabInteractable>();

        foreach (XRGrabInteractable item in items)
        {
            item.selectEntered.AddListener(OnItemGrabbed);
        }
    }

    void OnItemGrabbed(SelectEnterEventArgs args)
    {
        if (itemTaken) return;
        itemTaken = true;

        args.interactableObject.transform.SetParent(null);
        StartCoroutine(DisappearAndRespawn());
    }

    IEnumerator DisappearAndRespawn()
    {
        yield return new WaitForSeconds(0.5f);
        
        if (boxSpawner != null)
            boxSpawner.SpawnNewBox();

        Destroy(gameObject);
    }

}