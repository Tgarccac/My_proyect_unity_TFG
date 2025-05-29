using UnityEngine;
using UnityEngine.XR;

public class VRPlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public Transform playerCamera; // Cámara VR
    public XRNode inputSource = XRNode.LeftHand; // Controlador izquierdo para movimiento
    public XRNode teleportSource = XRNode.RightHand; // Controlador derecho para teleport
    public float speed = 2.5f;
    public float teleportRange = 10f;
    public LayerMask teleportLayer;
    public GameObject teleportMarker; // Opcional: marcador de teletransporte

    private bool isTeleportMode = false;
    private Vector2 inputAxis;

    void Update()
    {
        if (isTeleportMode)
        {
            HandleTeleport();
        }
        else
        {
            HandleJoystickMovement();
        }
    }

    void HandleJoystickMovement()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis))
        {
            Vector3 move = playerCamera.forward * inputAxis.y + playerCamera.right * inputAxis.x;
            move.y = 0; // Evita inclinación
            characterController.Move(move * speed * Time.deltaTime);
        }
    }

    void HandleTeleport()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(teleportSource);
        if (device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool teleportPressed) && teleportPressed)
        {
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, teleportRange, teleportLayer))
            {
                if (teleportMarker != null)
                {
                    teleportMarker.SetActive(true);
                    teleportMarker.transform.position = hit.point;
                }

                if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool confirmTeleport) && confirmTeleport)
                {
                    characterController.enabled = false;
                    transform.position = hit.point;
                    characterController.enabled = true;
                }
            }
        }
        else if (teleportMarker != null)
        {
            teleportMarker.SetActive(false);
        }
    }

    public void ToggleMovementMode()
    {
        isTeleportMode = !isTeleportMode;
        if (teleportMarker != null)
            teleportMarker.SetActive(false);
    }
}
