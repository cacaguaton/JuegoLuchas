using UnityEngine;

public class Botiquin : MonoBehaviour
{
    public HealthSystem healthSystem;

    void OnTriggerEnter(Collider other)
    {
        // Se ejecuta cuando otro collider entra en el trigger
        if (other.gameObject.CompareTag("Player"))
        {
           // Debug.Log("¡El jugador entró al trigger!");

            healthSystem.RegenerateHealth();
            // Aquí puedes ejecutar acciones como encender un audio, activar un objeto, etc.

        }
    }

    void OnTriggerExit(Collider other)
    {
        // Se ejecuta cuando otro collider sale del trigger
        if (other.gameObject.CompareTag("Player"))
        {
           // Debug.Log("¡El jugador salió del trigger!");
        }
    }

}
