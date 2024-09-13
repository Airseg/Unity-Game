using System.Collections; // Ajoute ceci
using UnityEngine;

public class MechantScript : MonoBehaviour
{
    public Transform player;
    public float vitesse = 5f;
    public float distanceAttaque = 1.5f;
    public float hauteurSol = 0.1f; // Hauteur au-dessus du sol pour éviter de tomber
    public int attackDamage = 20; // Assure-toi que cette ligne est présente

    private Animator animator;
    private Rigidbody rb;
    private bool isAttacking = false;
    private bool canAttack = true;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        // Verrouiller la rotation sur l'axe Y pour éviter que le méchant se penche
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        if (animator.GetBool("IsDead"))
        {
            return;
        }

        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        if (direction.magnitude > distanceAttaque)
        {
            animator.SetBool("IsAttacking", false);
            isAttacking = false;

            if (direction.magnitude > 0.1f)
            {
                Quaternion nouvelleRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, nouvelleRotation, 0.15f);

                // Appliquer le mouvement au personnage, en évitant les mouvements verticaux
                transform.Translate(Vector3.forward * vitesse * Time.deltaTime);
                // S'assurer que le méchant reste à une hauteur correcte
                if (transform.position.y < hauteurSol)
                {
                    transform.position = new Vector3(transform.position.x, hauteurSol, transform.position.z);
                }

                animator.SetBool("IsRunning", true);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }
        }
        else
        {
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsAttacking", true);
            isAttacking = true;

            if (canAttack)
            {
                StartCoroutine(AttackCoroutine());
                canAttack = false;
            }
        }
    }

    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(1.1f);

        if (isAttacking)
        {
            player.GetComponent<CharacterStats>().TakeDamage(attackDamage);
            Debug.Log("Player Health after attack: " + player.GetComponent<CharacterStats>().currentHealth);
        }

        yield return new WaitForSeconds(2.6f-1f);

        canAttack = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Méchant est en collision avec le joueur.");
        }
    }
}
