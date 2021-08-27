using UnityEngine;
using UnityEngine.UI;

namespace Game.Characters
{
    public class CHpBarController : MonoBehaviour
    {
        [SerializeField] private RectTransform _hpBar;
        [SerializeField] private Image _fill;
        [SerializeField] private bool _isScreenSpace;        

        public void UpdateUi(float percent) => _fill.fillAmount = Mathf.Clamp01(percent);

        private void Update()
        {
            if (_isScreenSpace)
                return;
        
            Vector3 hpBarForwardVec = Camera.main.transform.position - transform.position;
            hpBarForwardVec.y = 0;
            _hpBar.transform.forward = hpBarForwardVec;
        }
    }
}