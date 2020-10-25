using UnityEngine;

public class Screen : MonoBehaviour
{
    public virtual void Show(bool state)
    {
        gameObject.SetActive(state);
    }
}
