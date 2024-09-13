using UnityEngine;

public class DeplacementObjet : MonoBehaviour
{
    public float vitesse = 5f;
    public float forceSaut = 7f;
    private bool estAuSol = true;
    private Rigidbody rb;
    [SerializeField] private Animator animator;
    public Transform mechant; // Référence au méchant
    public Transform cameraTransform; // Référence à la caméra principale

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("Aucun Animator trouvé dans les enfants de Player");
        }

        if (mechant == null)
        {
            mechant = GameObject.FindGameObjectWithTag("Mechant").transform;
        }

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform; // Trouver la caméra principale si aucune n'est assignée
        }
    }

    void Update()
    {
        // Gestion des mouvements
        float deplacementHorizontal = Input.GetAxis("Horizontal");
        float deplacementVertical = Input.GetAxis("Vertical");

        // Obtenir la direction de la caméra en ignorant la rotation sur l'axe Y
        Vector3 forward = cameraTransform.forward;
        forward.y = 0f; // Ignore l'inclinaison verticale de la caméra
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0f; // Ignore l'inclinaison verticale de la caméra
        right.Normalize();

        // Créer un vecteur de mouvement relatif à la direction de la caméra
        Vector3 mouvement = (forward * deplacementVertical + right * deplacementHorizontal).normalized;

        if (mouvement.magnitude > 0.1f)
        {
            // Faire tourner le personnage pour qu'il fasse face à la direction du mouvement
            transform.rotation = Quaternion.LookRotation(mouvement);

            // Appliquer le mouvement
            rb.MovePosition(transform.position + mouvement * vitesse * Time.deltaTime);

            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        // Gestion du saut
        if (Input.GetKeyDown(KeyCode.Space) && estAuSol)
        {
            rb.AddForce(Vector3.up * forceSaut, ForceMode.Impulse);
            animator.SetBool("IsJumping", true);
            estAuSol = false;
        }

        // Gestion du combat
        if (Input.GetKeyDown(KeyCode.E)) // Le joueur appuie sur la touche 'E'
        {
            animator.SetBool("IsFighting", true);

            // Vérifier si le méchant est à portée
            if (Vector3.Distance(transform.position, mechant.position) < 1.5f)
            {
                // Infliger des dégâts au méchant
                mechant.GetComponent<CharacterStats>().TakeDamage(20); // Ajuste les dégâts à 20
            }
        }
        else if (Input.GetKeyUp(KeyCode.E)) // Le joueur relâche la touche 'E'
        {
            animator.SetBool("IsFighting", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            estAuSol = true;
            animator.SetBool("IsJumping", false); // Réinitialise l'état de saut lorsque le personnage atterrit
        }
    }
}
