using UnityEngine;

public class CarColorSwap : MonoBehaviour
{
    public Material Blue;
    public Material Purpur;
    public Material Red;


    private Renderer playerRenderer;
    private Material originalMaterial;
    private bool usingBlue = false;
    private bool usingPurpur = false;
    private bool usingRed = false;

    void Start()
    {
        playerRenderer = GetComponent<Renderer>();

        if (playerRenderer != null)
            originalMaterial = playerRenderer.material;
    }

    public void SwapMaterialBlue()
    {
        if (playerRenderer == null || Blue == null) return;

        if (!usingBlue)
        {
            playerRenderer.material = Blue;
            usingBlue = true;
            usingRed = false;
            usingPurpur = false; 
        }
        else
        {
            playerRenderer.material = originalMaterial;
            usingBlue = false;
        }
    }

    public void SwapMaterialPurpur()
    {
        if (playerRenderer == null || Purpur == null) return;

        if (!usingPurpur)
        {
            playerRenderer.material = Purpur;
            usingPurpur = true;
            usingRed = false;
            usingBlue = false; 
        }
        else
        {
            playerRenderer.material = originalMaterial;
            usingPurpur = false;
        }
    }

    public void SwapMaterialRed()
    {
        if (playerRenderer == null || Red == null) return;

        if (!usingRed)
        {
            playerRenderer.material = Red;
            usingRed = true;
            usingPurpur = false;
            usingBlue = false;
        }
        else
        {
            playerRenderer.material = originalMaterial;
            usingRed = false;
        }
    }
}
