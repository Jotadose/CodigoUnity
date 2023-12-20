using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackCooldown = 2f;

    private bool isMoving = false;
    private bool isAttacking = false;

    private void Start()
    {
        // Iniciar el comportamiento del NPC
        InvokeRepeating("RandomMovement", 0f, Random.Range(1f, 5f));
    }

    private void Update()
    {
        if (!isMoving && !isAttacking)
        {
            // Si no está en movimiento ni atacando, moverse aleatoriamente
            RandomMovement();
        }
    }

    void RandomMovement()
    {
        // Generar una posición aleatoria
        Vector3 randomDirection = Random.insideUnitSphere * 10f;
        randomDirection += transform.position;

        // Mover al NPC hacia la posición aleatoria
        StartCoroutine(MoveTo(randomDirection));
    }

    IEnumerator MoveTo(Vector3 targetPosition)
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;

        // Esperar antes de permitir otro movimiento
        yield return new WaitForSeconds(1f);

        // Realizar un ataque aleatorio después de esperar
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        // Simular la lógica de ataque, puedes personalizar según tus necesidades

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }
}