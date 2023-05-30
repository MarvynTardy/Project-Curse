using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayMiniMap : MonoBehaviour
{
    public bool isDisplaying = true;
    public GameObject minimap;
    public Image miniMapBorder;
    public RawImage miniMapUI;
    public Image decoUn;
    public Image decoDeux;
    private bool isActive = false;
    public float fadeDuration = 1;
    public Collider colliderTrigger;

    public void Start()
    {
        // colliderTrigger = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && !isActive)
        {
            if (isDisplaying)
            {
                minimap.SetActive(true);

                miniMapBorder.enabled = true;

                decoUn.enabled = true;

                decoDeux.enabled = true;

                miniMapUI.enabled = true;

                isActive = true;
            }
        }
    }
}
