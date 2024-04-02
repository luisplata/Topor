using TMPro;
using UnityEngine;

public class DebugCustom : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI debugText;
    
    public void DebugText(string text)
    {
        debugText.text += text + "\n";
        Debug.Log(text);
    }
}
