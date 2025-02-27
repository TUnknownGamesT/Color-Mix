using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{


    public float speed;

    public float tolerance = 0.01f;
    
    [SerializeField]
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private string colorProperty = "Color";
    private Rigidbody rb;
    private Color _color = Color.black;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.AddForce(Vector3.forward * -1 * speed * Time.deltaTime);
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
            if (AreColorsSimilar(collisionInfo.gameObject.GetComponent<MeshRenderer>().material.color, _color))
            {
                Debug.Log(AreColorsSimilar(collisionInfo.gameObject.GetComponent<MeshRenderer>().material.color, _color));
                Debug.Log($"{collisionInfo.gameObject.GetComponent<MeshRenderer>().material.color} = {_color}");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log($"{collisionInfo.gameObject.GetComponent<MeshRenderer>().material.color} != {_color}");
            }
        }
    }

    bool AreColorsSimilar(Color c1, Color c2)
    {
        return Vector4.Distance(new Vector4(c1.r, c1.g, c1.b, c1.a), new Vector4(c2.r, c2.g, c2.b, c2.a)) <= tolerance;
    }
}
