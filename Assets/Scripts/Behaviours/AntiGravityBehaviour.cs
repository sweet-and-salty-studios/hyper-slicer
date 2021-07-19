using HyperSlicer.UI;
using UnityEngine;

namespace HyperSlicer.Behaviours
{
    public class AntiGravityBehaviour : MonoBehaviour
    {
        private readonly float maxEnergy = 100;
        private readonly float energyConsumptionMultiplier = 10;
        private float currentEnergy = default;

        [SerializeField] private Canvas antiGravityCanvas = default;
        [SerializeField] private AntiGravityDisplay antiGravityDisplay = default;
        
        private Rigidbody rb = default;

        private bool isActive = default;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            Initialize();
        }
     
        private void Update()
        {
            if(isActive == false) 
            {
                if(currentEnergy >= maxEnergy) ModifyEnergy(energyConsumptionMultiplier * Time.deltaTime);
                return;
            }

            if(currentEnergy <= 0)
            {
                Deactivate();
            }

            ModifyEnergy(-energyConsumptionMultiplier * Time.deltaTime);
            antiGravityDisplay.UpdateProgress(maxEnergy, currentEnergy);
        }

        private void Initialize()
        {
            currentEnergy = maxEnergy;

            Shutdown();
        }

        private void ModifyEnergy(float amount)
        {
            currentEnergy = Mathf.Clamp(currentEnergy += amount, 0, maxEnergy);
        }

        public void Shutdown()
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            antiGravityCanvas.gameObject.SetActive(false);
            isActive = false;
        }

        public void Deactivate()
        {
            rb.useGravity = true;

            antiGravityCanvas.gameObject.SetActive(false);
            isActive = false;
        }

        public void Activate()
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;

            antiGravityCanvas.gameObject.SetActive(true);
            isActive = true;
        }
    }
}