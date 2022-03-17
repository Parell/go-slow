using UnityEngine;

public class GunTrigger : MonoBehaviour
{
    //public PlayerManager playerManager;
    public Gun[] guns;
    public Transform GimbalTarget = null;

    private void Update()
    {
        //if (GameManager.Instance.isPaused || playerManager.isDead) { return; }

        if (Input.GetMouseButton(0))
        {
            foreach (var gun in guns)
            {
                if (GimbalTarget != null)
                {
                    gun.UseGimballedAiming = true;
                    gun.GimbalTarget = GimbalTarget.position;
                }
                else
                {
                    gun.UseGimballedAiming = false;
                }

                gun.FireSingleShot();
            }
        }
    }
}
