using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{


    public float speed;

    public float tolerance = 0.01f;

    private MeshRenderer meshRenderer;
    private Rigidbody rb;

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.AddForce(Vector3.forward * -1 * speed * Time.deltaTime);
    }

    public void ChangeColor(Color color)
    {
        meshRenderer.material.color = color;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("CannonBall"))
        {
            if (AreColorsSimilar(collisionInfo.gameObject.GetComponent<MeshRenderer>().material.color, meshRenderer.material.color))
            {
                Debug.Log(AreColorsSimilar(collisionInfo.gameObject.GetComponent<MeshRenderer>().material.color, meshRenderer.material.color));
                Debug.Log($"{collisionInfo.gameObject.GetComponent<MeshRenderer>().material.color} = {meshRenderer.material.color}");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log($"{collisionInfo.gameObject.GetComponent<MeshRenderer>().material.color} != {meshRenderer.material.color}");
            }
        }
    }

    bool AreColorsSimilar(Color c1, Color c2)
    {
        return Vector4.Distance(new Vector4(c1.r, c1.g, c1.b, c1.a), new Vector4(c2.r, c2.g, c2.b, c2.a)) <= tolerance;
    }
}
