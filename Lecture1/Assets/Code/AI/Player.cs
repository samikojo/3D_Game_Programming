using UnityEngine;
using GameProgramming3D.WaypointSystem;
using GameProgramming3D.Utility;

namespace GameProgramming3D.AI
{
	[RequireComponent(typeof(Health), typeof(CommandSystem), typeof(Mover))]
	public class Player : MonoBehaviour
	{
		[SerializeField] private float _speed;
		[SerializeField] private Health _health;
		[SerializeField] private Transform _shootingPoint;
		[SerializeField] private float _shootingInterval;
		[SerializeField] private CommandSystem _commandSystem;

		public Vector3 Position { get { return transform.position; } }
		public Health Health { get { return _health; } }
		public Transform ShootingPoint { get { return _shootingPoint; } }
		public Mover Mover { get; private set; }
		public Shooter Shooter { get; private set; }

		protected void Awake()
		{
			if(_health == null)
			{
				_health = gameObject.GetOrAddComponent<Health> ();
			}

			Mover = gameObject.GetOrAddComponent< Mover >();
			Mover.Init( GetComponentInChildren<Collider>(), _speed, Direction.Forward );
			_commandSystem = gameObject.GetOrAddComponent<CommandSystem> ();
			Shooter = gameObject.GetOrAddComponent<Shooter> ();
			Shooter.Init ( ShootingPoint );
		}

		protected void Update()
		{
			if(Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay ( Input.mousePosition );
				RaycastHit hit;
				int layerMask = Flags.CreateMask ( LayerMask.NameToLayer ( "Ground" ) );
				if( Physics.Raycast(ray, out hit, 100f, layerMask) )
				{
					Vector3 movePosition = hit.point;
					MoveCommand moveCommand = new MoveCommand ( this, movePosition );
					_commandSystem.AddCommand ( moveCommand );
				}
			}

			if(Input.GetMouseButtonDown(1))
			{
				Ray ray = Camera.main.ScreenPointToRay ( Input.mousePosition );
				RaycastHit hit;
				int layerMask = Flags.CreateMask ( LayerMask.NameToLayer ( "Ground" ),
					LayerMask.NameToLayer ( "Enemy" ) );
				if(Physics.Raycast(ray, out hit, 100f, layerMask))
				{
					Vector3 shootPosition = hit.point;
					ShootCommand shootCommand = new ShootCommand ( this, shootPosition,
						_shootingInterval, _shootingPoint );
					_commandSystem.AddCommand ( shootCommand );
				}
			}

			float vertical = Input.GetAxis ( "Vertical" );
			float horizontal = Input.GetAxis ( "Horizontal" );
			Vector3 position = Position;
			position += transform.forward * vertical * _speed * Time.deltaTime;
			position += transform.right * horizontal * _speed * Time.deltaTime;
			transform.position = position;
		}

		public void ApplyExplosionForce ( float force, Vector3 position, float radius )
		{	}
	}
}
