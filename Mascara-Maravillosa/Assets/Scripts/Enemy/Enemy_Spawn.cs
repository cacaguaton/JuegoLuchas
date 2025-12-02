using UnityEngine;


public class Enemy_Spawn : MonoBehaviour
{
 // Arrastra tu Prefab aquí desde el Inspector de Unity
    [SerializeField]
    private GameObject Enemy; 
    [SerializeField]
    public float tiempo = 3f;
    public float rangoDeSpawn =6f;

    [SerializeField]
    public int cantidadDeEnemigos = 3;

    public GameObject[] _Enemy;

    // Define un punto de spawn (opcional, si no, usa la posición del Spawner)
    public Transform puntoDeSpawn; 

    // Método que puedes llamar para generar el objeto
    public void GenerarObjeto()
    {
        // Instancia una copia del prefab en la posición y rotación especificadas.

        Instantiate(Enemy, puntoDeSpawn.position, puntoDeSpawn.rotation);


        void SpawnearObjetosConFor()
        {
        // El bucle 'for' se ejecuta desde 0 hasta la cantidadDeObjetos definida.
            for (int i = 0; i < cantidadDeEnemigos; i++)
            {
            // 1. Calcular una posición aleatoria dentro del rango
                Vector3 posicionAleatoria = new Vector3(
                Random.Range(-rangoDeSpawn, rangoDeSpawn), // Posición X
                0f,                                        // Posición Y (puedes ajustarla)
                Random.Range(-rangoDeSpawn, rangoDeSpawn)  // Posición Z
               // ,EsperarYActivar()
            );

            }

        }

    }
/*

    public IEnumerator EsperarYActivar()
    {
        // Espera 3 segundos
        yield return new WaitForSeconds(tiempo);

        // Llama a la función deseada
        
    }*/
}

