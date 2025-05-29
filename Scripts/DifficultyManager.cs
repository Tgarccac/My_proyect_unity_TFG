using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DifficultyManager : MonoBehaviour
{
    public GameObject[] easyObjects;
    public GameObject[] mediumObjects;
    public GameObject[] hardObjects;

    public GameObject startGameCanvas;
    public GameObject endGameCanvas;
    public Transform jugador;

    private Coroutine gameCoroutine;

    private void Start()
    {
        
    }
    public void StartEasyMode()
    {
        if (gameCoroutine != null) StopCoroutine(gameCoroutine);
        gameCoroutine = StartCoroutine(StartGame(100f, easyObjects));
    }

    public void StartMediumMode()
    {
        if (gameCoroutine != null) StopCoroutine(gameCoroutine);
        gameCoroutine = StartCoroutine(StartGame(90f, mediumObjects));
    }

    public void StartHardMode()
    {
        if (gameCoroutine != null) StopCoroutine(gameCoroutine);
        gameCoroutine = StartCoroutine(StartGame(60f, hardObjects));
    }

    IEnumerator StartGame(float duration, GameObject[] objectsToActivate)
    {
        
        DisableAllObjects();
        startGameCanvas.SetActive(false);
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }

        yield return new WaitForSeconds(duration);

        EndGame();
    }

    void DisableAllObjects()
    {
        foreach (GameObject obj in easyObjects) obj.SetActive(false);
        foreach (GameObject obj in mediumObjects) obj.SetActive(false);
        foreach (GameObject obj in hardObjects) obj.SetActive(false);
    }

    void EndGame()
    {
        DisableAllObjects();
        Vector3 direccionFrente = jugador.forward * 0.8f; 
        Vector3 posicionFrenteXZ = jugador.position + direccionFrente;

        Vector3 posicionFinal = new Vector3(posicionFrenteXZ.x,-0.2f,posicionFrenteXZ.z);

        endGameCanvas.SetActive(true);
        endGameCanvas.transform.position = posicionFinal;

        Vector3 rotacionActual = endGameCanvas.transform.rotation.eulerAngles;
        Vector3 rotacionJugador = jugador.rotation.eulerAngles;
        endGameCanvas.transform.rotation = Quaternion.Euler(rotacionActual.x,rotacionJugador.y,rotacionActual.z);
    }
    public void RestartGame()
    {
        endGameCanvas.SetActive(false);
        Vector3 direccionFrente = jugador.forward * 0.8f;
        Vector3 posicionFrenteXZ = jugador.position + direccionFrente;

        Vector3 posicionFinal = new Vector3(posicionFrenteXZ.x, -0.2f, posicionFrenteXZ.z);

        startGameCanvas.SetActive(true);
        startGameCanvas.transform.position = posicionFinal;

        Vector3 rotacionActual = startGameCanvas.transform.rotation.eulerAngles;
        Vector3 rotacionJugador = jugador.rotation.eulerAngles;
        startGameCanvas.transform.rotation = Quaternion.Euler(rotacionActual.x, rotacionJugador.y, rotacionActual.z);

        DisableAllObjects();
    }
}
