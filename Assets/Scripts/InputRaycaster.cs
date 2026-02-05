using UnityEngine;

public class InputRaycaster : MonoBehaviour
{
    public static Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
