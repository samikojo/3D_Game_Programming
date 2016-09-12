using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameProgramming3D
{
	public abstract class Unit : MonoBehaviour, IDamageReceiver
	{
		public static Color NormalColor = Color.white;
		public static Color SelectedColor = Color.green;
		public static Color DeadColor = Color.red;
		public const float GRAVITY = 9.81f;

		public static event System.Action< Unit > UnitSelected;
		public static event System.Action< Unit > UnitDied;

		[SerializeField]
		private float _speed;

		[SerializeField]
		private int _health = 10;

		private Renderer _renderer;

		public CharacterController Controller { get; protected set; }
		protected Vector3 MovementVector { get; private set; }

		protected virtual float Gravity
		{
			get { return GRAVITY; }
		}

		public Renderer Renderer
		{
			get
			{
				if ( _renderer == null )
				{
					_renderer = GetComponent< Renderer >();
				}
				return _renderer;
			}
		}

		protected virtual void Awake()
		{
			Controller = gameObject.GetOrAddComponent< CharacterController >();
		}

		protected void Update()
		{
			float y = MovementVector.y;
			y -= Gravity * Time.deltaTime; // y = y - 9.81f;
			MovementVector = new Vector3(MovementVector.x, y, MovementVector.z);
			if ( Controller.enabled )
			{
				Controller.Move( MovementVector );
			}
			MovementVector = Vector3.zero;
		}

		public void Select( bool isSelected )
		{
			Renderer.material.color = isSelected ? SelectedColor : NormalColor;
			if ( isSelected )
			{
				if ( UnitSelected != null )
				{
					UnitSelected( this );
				}
			}
		}

		public void TakeDamage()
		{
			_health = Mathf.Max( 0, _health - 1 );
			if ( _health == 0 )
			{
				Die();
			}
		}

		public virtual void Move()
		{
			var horizontal = Input.GetAxis( "Horizontal" );
			var vertical = Input.GetAxis( "Vertical" );
			var x = horizontal * _speed * Time.deltaTime;
			var y = 0;
			var z = vertical * _speed * Time.deltaTime;
			MovementVector = new Vector3(x, y, z);
		}

		public virtual void Die()
		{
			Renderer.material.color = DeadColor;
			var collider = GetComponent< Collider >();
			if ( collider != null )
			{
				collider.enabled = false;
			}

			if ( UnitDied != null )
			{
				UnitDied( this );
			}
		}
	}
}
