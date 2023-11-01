using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject Gun;
    public float range = 100f; // How far the bullet can travel
    

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
            // For now, we'll just log what we hit. You can expand on this.
            Debug.Log("Hit: " + hit.transform.name);

            // Display the bullet's path
            ShowBulletTrajectory(playerCamera.transform.position, hit.point);
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
