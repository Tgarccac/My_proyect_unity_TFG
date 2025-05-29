using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableObject : MonoBehaviour
{
    private Vector3 originalScale;
    public float enlargedScale = 1.2f; 
    public float scaleSpeed = 5f; 

    private bool isHovered = false;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        
        Vector3 targetScale = isHovered ? originalScale * enlargedScale : originalScale;
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * scaleSpeed);
    }

    public void OnHoverEnter()
    {
        isHovered = true;
    }

    public void OnHoverExit()
    {
        isHovered = false;
    }

    public void OnSelectEnter()
    {
        SceneManager.LoadScene("TerrainScene");
    }
}
