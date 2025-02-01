using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health health;
    
    [SerializeField] private Image spriteRenderer;
    
    public void UpdateView()
    {
        spriteRenderer.fillAmount = (float)health.Value / (float)health.MaxValue;    
    }
}
