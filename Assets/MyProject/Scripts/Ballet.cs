using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Ballet : MonoBehaviour
{
    [SerializeField]
    private string balletPoolName;

    [SerializeField]
    private float lifeTime;

    private ObjectPooling pool;

    // Start is called before the first frame update
    void Start()
    {
        var pool = GameObject.Find(balletPoolName).GetComponent<ObjectPooling>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        StartCoroutine(DestroyMe(gameObject));
    }

    private IEnumerator DestroyMe(GameObject myself)
    {
        yield return new WaitForSeconds(lifeTime);
        
        myself.SetActive(false);

        yield return null;
    }
}
