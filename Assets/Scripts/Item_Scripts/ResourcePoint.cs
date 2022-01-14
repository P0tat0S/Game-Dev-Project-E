using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourcePoint : MonoBehaviour, IInteractable {
    public GameObject DroppedItem;
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    public void Interact(Interactor interactor, out bool interactSuccsseful) {
        Instantiate(DroppedItem, transform.position - new Vector3(0f, 2f, 0f), Quaternion.identity);
        interactSuccsseful = true;
        Destroy(gameObject);
    }

    public void EndInteraction() {

    }

    private void OnCollisionEnter(Collision other) {
        Destroy(this);
    }
}
