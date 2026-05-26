using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using System;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ItemBox : MonoBehaviour
{
    public Action OnBoxDepleted;
    private bool itemTaken = false;

    void Start()
    {
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
        StartCoroutine(DisappearAndNotify());
    }

    IEnumerator DisappearAndNotify()
    {
        yield return new WaitForSeconds(0.5f);
        
        OnBoxDepleted?.Invoke();

        Destroy(gameObject);
    }
}