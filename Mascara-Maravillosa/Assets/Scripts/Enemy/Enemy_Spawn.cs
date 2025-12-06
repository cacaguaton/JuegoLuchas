using UnityEngine;
using System.Collections; // Necesario para usar Corrutinas

public class Enemy_Spawn : MonoBehaviour
{
    // Arrastra tu Prefab aquí desde el Inspector de Unity
    [SerializeField]
    private GameObject enemyPrefab;

    [Header("Configuración de Spawn")]
    [SerializeField]
    public float tiempoEntreSpawns = 3f;
    [SerializeField]
    public float rangoDeSpawn = 6f;
    [SerializeField]
    public int cantidadTotalDeEnemigos = 3;

    // Define un punto de spawn (opcional, si no, usa la posición del Spawner)
    // Si no asignas nada aquí, usará la posición del objeto que tiene este script.
    public Transform puntoDeSpawn;

    void Start()
    {
        // Iniciamos la corrutina que maneja todo el proceso de spawn.
        StartCoroutine(SpawnearEnemigosConRetardo());
    }

    // Corrutina para manejar el spawn con esperas
    IEnumerator SpawnearEnemigosConRetardo()
    {
        // Asegúrate de que el punto de spawn esté definido, si no, usa la posición actual del objeto.
        Vector3 basePosition = (puntoDeSpawn != null) ? puntoDeSpawn.position : transform.position;

        for (int i = 0; i < cantidadTotalDeEnemigos; i++)
        {
            // 1. Calcular una posición aleatoria dentro del rango
            Vector3 posicionAleatoria = basePosition + new Vector3(
                Random.Range(-rangoDeSpawn, rangoDeSpawn), // Posición X
                0f, // Posición Y (ajusta si tus enemigos necesitan spawnear más alto)
                Random.Range(-rangoDeSpawn, rangoDeSpawn)  // Posición Z
            );

            // 2. Instanciar el enemigo en la posición aleatoria
            Instantiate(enemyPrefab, posicionAleatoria, Quaternion.identity); // Quaternion.identity = sin rotación específica

            // 3. Esperar el tiempo definido antes del próximo spawn
            yield return new WaitForSeconds(tiempoEntreSpawns);
        }
    }
}


