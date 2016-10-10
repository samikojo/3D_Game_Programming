using System.Collections;
using UnityEngine;

namespace GameProgramming3D
{
	public class CountdownTimer : MonoBehaviour
	{
		public event System.Action TimerFinished;

		public float CurrentTime { get; set; }
		public float InitialTime { get; private set; }

		private bool _isRunning = false;
		private Coroutine _timerCoroutine;

		public bool SetTime( float seconds )
		{
			var result = false;
			if ( !_isRunning )
			{
				CurrentTime = seconds;
				InitialTime = seconds;
				result = true;
			}
			return result;
		}

		public bool StartTimer()
		{
			var result = false;
			if ( !_isRunning )
			{
				_timerCoroutine = StartCoroutine( TimerCoroutine() );
				_isRunning = true;
				result = true;
			}
			return result;
		}

		public bool Stop()
		{
			var result = false;
			if ( _isRunning )
			{
				if ( _timerCoroutine != null )
				{
					StopCoroutine( _timerCoroutine );
					_timerCoroutine = null;
				}
				result = true;
				_isRunning = false;
			}
			return result;
		}

		public bool Reset()
		{
			var result = false;
			if ( !_isRunning )
			{
				CurrentTime = InitialTime;
				result = true;
			}
			return result;
		}

		private IEnumerator TimerCoroutine()
		{
			while ( CurrentTime > 0 )
			{
				CurrentTime -= Time.deltaTime;
				yield return null;
			}
			if ( TimerFinished != null )
			{
				TimerFinished();
			}
		}
	}
}
