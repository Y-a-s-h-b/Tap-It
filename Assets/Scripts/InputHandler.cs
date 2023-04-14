using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour
{
    public Vector2 InputVector { get; private set; }
    public Vector3 MousePosition { get; private set; }

    void Update()
    {
        InputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        MousePosition = Input.mousePosition;
    }
}
