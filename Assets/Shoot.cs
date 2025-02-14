using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float shootForce;
    public Transform bulletOrigin;
    public GameObject bulletPrefab;

    public KeyCode shootButton;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(shootButton))
        {
            Shooting();
        }
        
    }

    private void Shooting()
    {
        Instantiate(bulletPrefab, bulletOrigin);
    }


}
