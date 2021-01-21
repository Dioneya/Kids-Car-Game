using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[AddComponentMenu("Rendering/SetRenderQueue")]

public class RenderQueue : MonoBehaviour
{

    [SerializeField]
    protected int[] m_queues = new int[] { 3000 };

    protected void Awake()
    {
        SpriteRenderer[] rend = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < rend.Length && i < m_queues.Length; ++i)
        {
            rend[i].material.renderQueue = m_queues[i];
        }
    }
}
