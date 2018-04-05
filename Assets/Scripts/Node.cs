using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    [Header("Attributes")]
    public Color hoverColor;

    private Renderer rend;
    private Color startColor;

    void Start() {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

	// hover animation
    void OnMouseEnter() {
        rend.material.color = hoverColor;
    }

    void OnMouseExit() {
        rend.material.color = startColor;
    }
}
