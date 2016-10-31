using UnityEngine;
using UnityEngine.UI;

namespace GameProgramming3D.GUI
{
	public class HealthBar : MonoBehaviour
	{
		[SerializeField] private Gradient _gradient;
		[SerializeField] private Graphic _graphic;

		private float _maxWidth;
		private float _maxHealth;
		private RectTransform _transform;

		protected void Awake()
		{
			// The transform of our health bar graphics
			_transform = _graphic.GetComponent< RectTransform >();
			_maxWidth = _transform.rect.width;
		}

		public void Init( float maxHealth )
		{
			_maxHealth = maxHealth;
			SetHealth( _maxHealth );
		}

		public void SetHealth( float currentHealth )
		{
			float healtValue = currentHealth / _maxHealth;
			Vector2 sizeDelta = _transform.sizeDelta;
			sizeDelta.x = _maxWidth * healtValue;
			_transform.sizeDelta = sizeDelta;
			_graphic.color = _gradient.Evaluate( healtValue );
		}
	}
}
