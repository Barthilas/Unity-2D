using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{

    [SerializeField] Color32 hasPackageColor = new Color32(1, 1, 1, 1);
    [SerializeField] Color32 noPackageColor = new Color32(1, 1, 1, 1);

    bool hasPackage = false;
    [SerializeField] float timeToDestroy = 0.5f;

    SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Bumped into " + other.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Triggered " + other.gameObject.name);
        if(other.tag == "Package" && !hasPackage)
        {
            Debug.Log("-------Package Picked------");
            hasPackage = true;
            spriteRenderer.color = hasPackageColor;
            Destroy(other.gameObject, timeToDestroy);
        }
        if(other.tag == "DeliveryPoint" && hasPackage){
            Debug.Log("----Packege delivered-----");
            hasPackage = !hasPackage;
            spriteRenderer.color = noPackageColor;
        }
    }
}
