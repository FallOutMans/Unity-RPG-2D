using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;
    
    [Header("Flash FX")]
    [SerializeField] private Material hitMat;
    [SerializeField] private Material originalMat;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalMat = sr.material;
    }
}
