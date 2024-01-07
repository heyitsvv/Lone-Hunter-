using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject Gun;
    public float range = 15f;
    public float damage = 15f;
    public float aiMaxHealth = 100f;

    public LineRenderer bulletTrajectory;
    public float trajectoryDisplayDuration = 1f;

    public float shootCooldown = 0.5f;
    private float lastShotTime = -Mathf.Infinity;

    public AudioSource gunshotSound; // Drag and drop your gunshot sound asset to this field in the Unity Inspector

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time - lastShotTime >= shootCooldown)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Check if enough time has passed since the last shot
        if (Time.time - lastShotTime < shootCooldown)
        {
            // Still in cooldown, don't shoot
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            AIHealth aiHealth = hit.transform.GetComponent<AIHealth>();

            if (aiHealth != null)
            {
                aiHealth.TakeDamage(damage);

                Debug.Log("AI has been shot! Remaining health: " + aiHealth.GetCurrentHealth() + "/" + aiMaxHealth);

                if (aiHealth.GetCurrentHealth() <= 0)
                {
                    Debug.Log("AI is dead!");
                }
            }

            Debug.Log("Hit: " + hit.transform.name);

            ShowBulletTrajectory(playerCamera.transform.position, hit.point);

            // Play gunshot sound
            if (gunshotSound != null)
            {
                gunshotSound.Play();
            }

            // Update the last shot time
            lastShotTime = Time.time;
        }
        else
        {
            ShowBulletTrajectory(playerCamera.transform.position, playerCamera.transform.position + playerCamera.transform.forward * range);
        }
        //Gun.GetComponent<Animator>().Play("Recoil");
    }

    void ShowBulletTrajectory(Vector3 startPoint, Vector3 endPoint)
    {
        bulletTrajectory.SetPosition(0, startPoint);
        bulletTrajectory.SetPosition(1, endPoint);
        bulletTrajectory.enabled = true;
        StopAllCoroutines();
        StartCoroutine(HideBulletTrajectory());
    }

    System.Collections.IEnumerator HideBulletTrajectory()
    {
        yield return new WaitForSeconds(trajectoryDisplayDuration);
        bulletTrajectory.enabled = false;
    }
}
