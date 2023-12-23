using UnityEngine;

public class ShootingObject : MonoBehaviour
{
    [SerializeField] Vector3 dir;
    [SerializeField] Bullet bulletTemplate;
    [SerializeField] float totalCooldown;
    [SerializeField] float bulletSpeed;
    [SerializeField] bool shouldNotBeFiring;

    float currentCooldown;
    private void Update()
    {

        if (shouldNotBeFiring) return;
        if(currentCooldown > totalCooldown)
        {
            currentCooldown = 0;
            ShootBullet();
        }
        else
        {
            currentCooldown += Time.deltaTime;
        }
    }

    void ShootBullet()
    {
        Bullet newObject = Instantiate(bulletTemplate, transform.position, Quaternion.identity);
       
        newObject.SetUp("", dir, new DamageClass(5), bulletSpeed);
        newObject.SetDestroySelf(10);

        int[] layers = new int[1];
        layers[0] = 3;

        newObject.MakeLayer(layers);
        
    }


}
