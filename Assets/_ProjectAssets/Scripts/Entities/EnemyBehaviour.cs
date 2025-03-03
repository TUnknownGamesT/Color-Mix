using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Animator animator;
    public float speed;
    public float tolerance = 0.01f;

    [Header("Jump Attributes")]

    public Vector2 xValues;
    public Vector2 zValues;

    public float jumpHeight = 10f; // Maximum height of the jump
    public float jumpDuration = 1f; // Duration of the jump

    [SerializeField]
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private string colorProperty = "Color";
    private Rigidbody rb;
    private Color _color = Color.black;

    private bool _keepWalking = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (_keepWalking)
            rb.linearVelocity = Vector3.back * speed;
    }

    public void ChangeColor(Color color)
    {
        if (skinnedMeshRenderer == null)
        {
            Debug.LogError("SkinnedMeshRenderer is not assigned!");
            return;
        }

        // Get all materials assigned to the SkinnedMeshRenderer
        Material[] materials = skinnedMeshRenderer.materials;

        for (int i = 0; i < materials.Length; i++)
        {
            if (materials[i].HasProperty(colorProperty)) // Check if the property exists
            {
                materials[i].SetColor(colorProperty, color);
            }
            else
            {
                Debug.LogWarning($"Material {materials[i].name} does not have property {colorProperty}");
                materials[i].color = color;
            }
        }
        _color = color;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("CannonBall"))
        {
            if (AreColorsSimilar(collisionInfo.gameObject.GetComponent<MeshRenderer>().material.GetColor("_color"), _color))
            {
                Debug.Log(AreColorsSimilar(collisionInfo.gameObject.GetComponent<MeshRenderer>().material.GetColor("_color"), _color));
                Debug.Log($"{collisionInfo.gameObject.GetComponent<MeshRenderer>().material.GetColor("_color")} = {_color}");
                Destroy(gameObject);
            }
            else if (collisionInfo.gameObject.CompareTag("Floor"))
            {
            }
            else
            {
                Debug.Log($"{collisionInfo.gameObject.GetComponent<MeshRenderer>().material.color} != {_color}");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("EnemyJumper"))
        {
            animator.enabled = false;
            _keepWalking = false;
            rb.linearVelocity = Vector3.zero;
            Jump(new Vector3(Random.Range(xValues.x, xValues.y), transform.position.y, Random.Range(zValues.x, zValues.y)));
        }
    }

    public void Jump(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        Vector3 direction = targetPosition - startPosition;
        direction.y = 0; // Ignore vertical distance for horizontal velocity calculation

        float verticalDistance = targetPosition.y - startPosition.y;

        float initialVerticalVelocity = Mathf.Sqrt(2 * Physics.gravity.magnitude * jumpHeight);
        float timeToApex = initialVerticalVelocity / Physics.gravity.magnitude;
        float totalJumpTime = timeToApex + Mathf.Sqrt(2 * (jumpHeight - verticalDistance) / Physics.gravity.magnitude);

        Vector3 initialVelocity = direction / totalJumpTime;
        initialVelocity.y = initialVerticalVelocity;

        rb.linearVelocity = initialVelocity;
    }

    bool AreColorsSimilar(Color c1, Color c2)
    {
        return Vector4.Distance(new Vector4(c1.r, c1.g, c1.b, c1.a), new Vector4(c2.r, c2.g, c2.b, c2.a)) <= tolerance;
    }
}
