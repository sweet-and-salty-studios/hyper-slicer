using UnityEngine;
using UnityEngine.UI;

namespace HyperSlicer.UI
{
    public class AntiGravityDisplay : MonoBehaviour
    {
        [SerializeField] private Image fill = default;

        public void UpdateProgress(float maximum, float current)
        {
            fill.fillAmount = current / maximum;
        }
    }
}
