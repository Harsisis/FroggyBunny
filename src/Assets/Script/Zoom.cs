
using UnityEngine;
using System.Collections;
public class Zoom : MonoBehaviour
{
    [SerializeField]
    // Entre 0.1 et 2
    private float zoom = 1.0f;
    [SerializeField]
    // Nombre de frames que durera le zoom
    private int zoomFrames = 10;
    [SerializeField]
    private bool switchToInitialSize = false;

    private float targetOrtho;
    private bool triggered = false;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    private int maxLoop = 0;
    private float performedZoom;

    private void Awake()
    {
        targetOrtho = Camera.main.orthographicSize;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(smoothZoom());
        }
    }
    private IEnumerator smoothZoom()
    {
        if (triggered)
        {
            if (!switchToInitialSize)
            {
             
                switchToInitialSize = true;
                while (maxLoop != zoomFrames)
                {
                    if (targetOrtho > zoom)
                    {
                        performedZoom += targetOrtho * (zoom / zoomFrames / 5);
                    }
                    else
                    {
                        performedZoom -= targetOrtho * (zoom / zoomFrames / 5);
                    }
                    

                    Camera.main.orthographicSize += performedZoom;
                    maxLoop++;
                    yield return waitForFixedUpdate;
                }

                performedZoom = 0;
            }
            else
            {
                switchToInitialSize = false;
                while (maxLoop != zoomFrames)
                {
                    if (targetOrtho > zoom)
                    {
                        performedZoom -= targetOrtho * (zoom / zoomFrames / 5);
                    }
                    else
                    {
                        performedZoom += targetOrtho * (zoom / zoomFrames / 5);
                    }

                    // On s'assure qu'à la fin on retrouve bien exactement la valeur initiale de la caméra à la dernière frame de la boucle

                    if (maxLoop == (zoomFrames - 1))
                        Camera.main.orthographicSize = targetOrtho;
                    else
                        Camera.main.orthographicSize += performedZoom;
                   
                    maxLoop++;
                    yield return waitForFixedUpdate;
                }
            }

            performedZoom = 0;
            maxLoop = 0;
            triggered = false;
        }
    }
}
