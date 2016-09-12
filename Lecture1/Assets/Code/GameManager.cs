using UnityEngine;

namespace GameProgramming3D
{
	public class GameManager : MonoBehaviour
	{
		private static GameManager _instance;

		public static GameManager Instance
		{
			get
			{
				if ( _instance == null )
				{
					var go = new GameObject("GameManager");
					_instance = go.AddComponent< GameManager >();
					_instance.Init();
				}
				return _instance;
			}
		}

		private void Init()
		{
			AllUnits = FindObjectsOfType<Unit> ();
		}

		private Unit _selectedUnit;

		public Unit SelectedUnit
		{
			get { return _selectedUnit; }
			private set
			{
				if ( _selectedUnit != null )
				{
					_selectedUnit.Select( false );
				}
				_selectedUnit = value;
				if ( _selectedUnit != null )
				{
					_selectedUnit.Select( true );
				}
			}
		}

		private GUIManager _guiManager;

		public GUIManager GUIManager
		{
			get
			{
				if ( _guiManager == null )
				{
					_guiManager = FindObjectOfType< GUIManager >();
					if ( _guiManager == null )
					{
						var guiManagerGo = new GameObject("GUIManager");
						_guiManager = guiManagerGo.AddComponent< GUIManager >();
					}
				}
				return _guiManager;
			}
		}

		public Unit[] AllUnits { get; private set; }

		protected void Awake()
		{
			if ( _instance == null )
			{
				_instance = this;
				Init ();
			}
			else if ( _instance != this )
			{
				Destroy( this );
			}
		}

		protected void Update()
		{
			if ( Input.GetMouseButtonDown( 0 ) )
			{
				var ray = Camera.main.ScreenPointToRay( Input.mousePosition );
				RaycastHit hit;
				if ( Physics.Raycast( ray, out hit, 50 ) )
				{
					var unit = hit.collider.GetComponent< Unit >();
					if ( unit != null )
					{
						SelectedUnit = unit;
					}
				}
			}

			if ( Input.GetMouseButtonDown( 1 ) )
			{
				var ray = Camera.main.ScreenPointToRay( Input.mousePosition );
				RaycastHit hit;
				if ( Physics.Raycast( ray, out hit, 50 ) )
				{
					var damageReceiver = hit.collider.GetComponent< IDamageReceiver >();
					if ( damageReceiver != null )
					{
						damageReceiver.TakeDamage();
					}
				}
			}

			if ( SelectedUnit != null )
			{
				SelectedUnit.Move();
			}
		} // Update
	}
}
