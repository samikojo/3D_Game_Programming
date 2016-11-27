using UnityEngine;
using GameProgramming3D.SaveLoad;
using System.Collections;
using System.Collections.Generic;
using System;
using GameProgramming3D.GUI;

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

		public event System.Action<Unit, int> DamageTaken;

		[SerializeField] private float _speed;
		[SerializeField] private int _health = 10;
		[SerializeField] private float _rotationSpeed;
		[SerializeField] private int _id;

		private Renderer _renderer;

		/// <summary>
		/// Returns the unique id of the unit.
		/// </summary>
		public int Id { get { return _id; } }
		protected Vector3 MovementVector { get; set; }
		protected float MoveAmount { get; set; }
		protected float RotationAmount { get; set; }
		public Player AssociatedPlayer { get; protected set; }
		public bool IsAlive { get; protected set; }
		protected UnitGUI GUI { get; set; }

		public int Health
		{
			get { return _health; }
			set
			{
				_health = value;
				if ( _health <= 0 )
				{
					gameObject.SetActive( false );
				}
			}
		}

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
			
		}

		protected void Update()
		{
			UpdateMovement ();
		}

		protected abstract void UpdateMovement ();

		public virtual void Select( bool isSelected )
		{
			Renderer.material.color = isSelected ? SelectedColor : NormalColor;
			if ( isSelected )
			{
				OnUnitSelected();
			}
		}

		public virtual void Init( Player player )
		{
			IsAlive = true;
			AssociatedPlayer = player;
			GUI = GetComponentInChildren< UnitGUI >();
			GUI.Init( this );
		}

		protected void OnUnitSelected()
		{
			if ( UnitSelected != null )
			{
				UnitSelected ( this );
			}
		}

		public void TakeDamage(float amount)
		{
			_health = Mathf.Max( 0, _health - Mathf.RoundToInt( amount ) );
			if(DamageTaken != null)
			{
				DamageTaken ( this, _health );
			}

			if ( _health == 0 )
			{
				Die();
			}
		}

		public virtual void Move( float input )
		{
			MoveAmount = input * _speed * Time.deltaTime;
			var x = MovementVector.x;
			var y = MovementVector.y;
			var z = MoveAmount;
			MovementVector = new Vector3(x, y, z);
		}

		public virtual void Turn(float amount)
		{
			RotationAmount = amount * _rotationSpeed * Time.deltaTime;
		}

		public virtual void Die()
		{
			Renderer.material.color = DeadColor;
			var collider = GetComponent< Collider >();
			if ( collider != null )
			{
				collider.enabled = false;
			}

			OnUnitDied ();
		}

		protected void OnUnitDied()
		{
			if ( UnitDied != null )
			{
				UnitDied ( this );
			}
		}

		protected void ResetMovement()
		{
			RotationAmount = 0;
			MoveAmount = 0;
			MovementVector = Vector3.zero;
		}

		public void ApplyExplosionForce ( float force, Vector3 position, float radius )
		{
			if ( _health > 0 )
			{
				var rigidbody = GetComponent<Rigidbody> ();
				if ( rigidbody != null )
				{
					rigidbody.AddExplosionForce ( force, position, radius );
				}
			}
		}

		public virtual void Shoot()
		{

		}

		public void SetUnit(UnitData unitData)
		{
			Health = unitData.Health;
			transform.position = (Vector3)unitData.Position;
			transform.eulerAngles = (Vector3)unitData.Rotation;
		}
	}
}
