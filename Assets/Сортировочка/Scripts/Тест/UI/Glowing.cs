using UnityEngine;
using UnityEngine.UI;
public class Glowing : MonoBehaviour
{
    [SerializeField] Material Glow;
    private Material spriteMaterial;
    void Awake() 
    {
        if (TryGetComponent(out SpriteRenderer sprite))
            spriteMaterial = sprite.material;
    }
    public void AddGlow() 
    {
        if (TryGetComponent(out Image image))
            image.material = Glow;
        else if (TryGetComponent(out SpriteRenderer sprite)) 
            sprite.material = Glow;
            
    }

    public void RemoveGlow() 
    {
        if (TryGetComponent(out Image image))
            image.material = null;
        else if (TryGetComponent(out SpriteRenderer sprite))
            sprite.material = spriteMaterial;
    }
}
