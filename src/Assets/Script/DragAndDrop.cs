using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{

    private Camera gameCamera;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    private Vector2 velocity = Vector2.zero;

    // Permet d'assigner l'ecoute de l'evenement sur un bouton
    [SerializeField]
    private InputAction mouseClick;
    // SI rigidbody2D. Entre 0 et 100.
    [SerializeField]
    private float mouseDragPhysicSpeed = 0f;
    // SI pas de rigidbody2D. Entre 0 et 1.
    [SerializeField]
    private float mouseDragSpeed = 0f;
    [SerializeField]
    private bool Xaxis = true;
    [SerializeField]
    private bool Yaxis = true;

    private void Awake()
    {
        gameCamera = Camera.main;
    }

    private void OnEnable()
    {
        Debug.Log("clic activé");
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }

    // Sera extremement utile quand on devra bloquer ou non le clic selon si on est J1 ou J2
    private void OnDisable()
    {
        Debug.Log("clic désactivé");
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();
    }

    private void MousePressed(InputAction.CallbackContext context)
    {
        Ray ray = gameCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D clic2D = Physics2D.GetRayIntersection(ray);

        StartCoroutine(DragUpdate(clic2D.collider.gameObject));
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        clickedObject.TryGetComponent<Rigidbody2D>(out var rigidbody);
        float distance = Vector2.Distance(clickedObject.transform.position, gameCamera.transform.position);
        Vector2 initialPosition = clickedObject.transform.position;
        Vector2 actualPosition;

        while (mouseClick.ReadValue<float>() != 0)
        {
            Ray ray = gameCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (rigidbody != null)
            {
                mouseDragSpeed = 0;
                actualPosition = ray.GetPoint(distance) + clickedObject.transform.position;
                if (!Xaxis)
                    actualPosition.x = initialPosition.x;
                if (!Yaxis)
                    actualPosition.y = initialPosition.y;

                rigidbody.velocity = actualPosition * mouseDragPhysicSpeed;
            }
            else
            {
                mouseDragPhysicSpeed = 0;
                actualPosition = clickedObject.transform.position;

                // On ajuste la position avec un lossyscale pour manipuler l'objet plutot par le dessous que par le milieu. Utile nottamment pour les sprites tres grands.
                actualPosition.y += (clickedObject.transform.lossyScale.y / 2);

                if (!Xaxis)
                    actualPosition.x = initialPosition.x;
                if (!Yaxis)
                    actualPosition.y = initialPosition.y;

                clickedObject.transform.position = Vector2.SmoothDamp(actualPosition, ray.GetPoint(distance), ref velocity, mouseDragSpeed);
            }

            /* Pour que la boucle ne devienne pas incontrolable, on attend la prochaine frame "physique" et non "matérielle" pour update (voir doc) */
            yield return waitForFixedUpdate;
        }

    }


}
