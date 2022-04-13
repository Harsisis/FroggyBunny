using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// Rigidbody2D obligatoire. 
public class DragAndDrop : MonoBehaviour
{
    private Camera gameCamera;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    private Vector2 velocity = Vector2.zero;
    private bool tryToCheat = false;

    private float mouseDragPhysicSpeed = 10f;
    private float mouseDragSpeed = 0.5f;

    [SerializeField]
    private InputAction mouseClick;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Draggable")
        {
            tryToCheat = true;
        }
        else
        {
            tryToCheat = false;
        }
    }

    private void Awake()
    {
        gameCamera = Camera.main;
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }

    private void MousePressed(InputAction.CallbackContext context)
    {
        Ray ray = gameCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D hit2d = Physics2D.GetRayIntersection(ray);

        if (null != hit2d.collider && hit2d.collider.gameObject.tag == "Draggable" && !tryToCheat)
        {
            StartCoroutine(DragUpdate(hit2d.collider.gameObject));
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        if (clickedObject != null)
        {
            clickedObject.TryGetComponent<Rigidbody2D>(out var rigidbody);

            float distance = Vector2.Distance(clickedObject.transform.position, gameCamera.transform.position);
            Vector2 actualPosition;

            while (mouseClick.ReadValue<float>() != 0f)
            {
                Ray ray = gameCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

                if (rigidbody != null)
                {
                    mouseDragSpeed = 0f;
                    actualPosition = ray.GetPoint(distance) - clickedObject.transform.position;
                    rigidbody.velocity = actualPosition * mouseDragPhysicSpeed;

                    yield return waitForFixedUpdate;
                }
                else
                {
                    mouseDragPhysicSpeed = 0f;
                    actualPosition = clickedObject.transform.position;

                    clickedObject.transform.position = Vector2.SmoothDamp(actualPosition, ray.GetPoint(distance), ref velocity, mouseDragSpeed);

                    yield return null;
                }
            }
        }
    }
}
