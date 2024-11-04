using StarterAssets;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif


[RequireComponent(typeof(TrajectoryPredictor))]
public class ArrowShooter : MonoBehaviour
{
    TrajectoryPredictor trajectoryPredictor;

    [SerializeField]
    Rigidbody arrowPrefab;

    [SerializeField, Range(0.0f, 50.0f)]
    float shootingForce;

    [SerializeField]
    Transform bowPosition;

    [SerializeField]
    GameObject player;
    private ThirdPersonController thirdPersonController;
    public bool HasShoot = false;
    void Start()
    {
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();
        thirdPersonController = player.GetComponent<ThirdPersonController>(); 
    }

    void Update()
    {
        if (thirdPersonController._input.isAiming)
        {
            Predict();
            if (thirdPersonController._input.isShooting && !HasShoot)
            {
                ShootArrow();
                HasShoot = true;
            }
        }
     else
      {
           trajectoryPredictor.SetTrajectoryVisible(false);
        }

        if (!thirdPersonController._input.isShooting)
        {
            HasShoot = false;
        }


    }

    void Predict()
    {
        trajectoryPredictor.PredictTrajectory(ProjectileData());
    }

    ProjectileProperties ProjectileData()
    {
        ProjectileProperties properties = new ProjectileProperties();
        Rigidbody r = arrowPrefab.GetComponent<Rigidbody>();

        properties.direction = bowPosition.up;
        properties.initialPosition = bowPosition.position;
        properties.initialSpeed = shootingForce;
        properties.mass = r.mass;
        properties.drag = r.drag;

        return properties;
    }

    public void ShootArrow()
    {
        Rigidbody arrowInstance = Instantiate(arrowPrefab, bowPosition.position, bowPosition.rotation);
        arrowInstance.AddForce(bowPosition.up * shootingForce, ForceMode.Impulse);
    }
}