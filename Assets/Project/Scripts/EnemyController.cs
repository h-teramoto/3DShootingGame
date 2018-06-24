using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    void Start()
    {

    }

    public void Damage(int point)
    {
        Destroy(this.gameObject);
    }

    void Update()
    {
        transform.Rotate(new Vector3(2, 1, 5));
    }

}
