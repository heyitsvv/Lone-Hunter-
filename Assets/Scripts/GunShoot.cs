using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject Gun;
    public float range = 100f; // How far the bullet can travel
<<<<<<< Updated upstream
    public int bulletDamage = 50;
=======
    public float damage = 10f; // Damage to deal to the AI
    public float aiMaxHealth = 100f; // AI's maximum health

>>>>>>> Stashed changes


    // For visualizing the bullet's path
    public LineRenderer bulletTrajectory;
    public float trajectoryDisplayDuration = 1f; // Duration for which the trajectory is shown

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, range))
        {
            AIHealth aiHealth = hit.transform.GetComponent<AIHealth>();

            if (aiHealth != null)
            {
                // Reduce the AI's health
                aiHealth.TakeDamage(damage);

                // Log the AI's remaining health
                Debug.Log("AI has been shot! Remaining health: " + aiHealth.GetCurrentHealth() + "/" + aiMaxHealth);

                // You can also check if the AI is dead and perform additional actions here
                if (aiHealth.GetCurrentHealth() <= 0)
                {
                    // AI is dead, you can play death animations, destroy the AI, etc.
                    Debug.Log("AI is dead!");
                }

            }

            // For now, we'll just log what we hit. You can expand on this.
            Debug.Log("Hit: " + hit.transform.name);

            // Display the bullet's path
            ShowBulletTrajectory(playerCamera.transform.position, hit.point);

            // Check if the object hit has a "BasicAI" script
            BasicAI ai = hit.transform.GetComponent<BasicAI>();
            if (ai != null)
            {
                // Apply damage to the AI using the Line Renderer
                ai.TakeDamageFromLineRenderer(bulletDamage);
            }
        }
        else
        {
            // If we don't hit anything, show the bullet's path going to its maximum range
            ShowBulletTrajectory(playerCamera.transform.position, playerCamera.transform.position + playerCamera.transform.forward * range);
        }
        //Gun.GetComponent<Animator>().Play("Recoil");
    }

    void ShowBulletTrajectory(Vector3 startPoint, Vector3 endPoint)
    {
        bulletTrajectory.SetPosition(0, startPoint);
        bulletTrajectory.SetPosition(1, endPoint);
        bulletTrajectory.enabled = true;
        StopAllCoroutines(); // In case this is called multiple times in quick succession
        StartCoroutine(HideBulletTrajectory());
    }

    System.Collections.IEnumerator HideBulletTrajectory()
    {
        yield return new WaitForSeconds(trajectoryDisplayDuration);
        bulletTrajectory.enabled = false;
    }
}