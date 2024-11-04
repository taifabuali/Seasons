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

    void Start()
    {
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();
        thirdPersonController = player.GetComponent<ThirdPersonController>(); 
    }

    void Update()
    {
        if (thirdPersonController != null && thirdPersonController._input != null)
        {
            var input = thirdPersonController._input as StarterAssetsInputs; 
            if (input != null)
            {
                if (input.isAiming)
                {
                    Predict();
                    if (input.isShooting)
                    {
                        ShootArrow();
                    }
                }
            }
        }
    
        else
        {
            trajectoryPredictor.SetTrajectoryVisible(false);
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

        properties.direction = bowPosition.forward;
        properties.initialPosition = bowPosition.position;
        properties.initialSpeed = shootingForce;
        properties.mass = r.mass;
        properties.drag = r.drag;

        return properties;
    }

    void ShootArrow()
    {
        Rigidbody arrowInstance = Instantiate(arrowPrefab, bowPosition.position, Quaternion.identity);
        arrowInstance.AddForce(bowPosition.forward * shootingForce, ForceMode.Impulse);
    }
}