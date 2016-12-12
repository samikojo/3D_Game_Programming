using System.Collections.Generic;
using UnityEngine;

namespace GameProgramming3D.AI
{
	public class CommandSystem : MonoBehaviour
	{
		[SerializeField] private bool _queueCommands;

		private Queue<PlayerCommandBase> _commandQueue =
			new Queue<PlayerCommandBase> ();
		private PlayerCommandBase _currentCommand;

		public PlayerCommandBase CurrentCommand
		{
			get { return _currentCommand; }
			private set
			{
				_currentCommand = value;
				if(_currentCommand != null)
				{
					_currentCommand.CommandExecuted += HandleCommandExecuted;
					_currentCommand.Begin ();
				}
			}
		}

		private void HandleCommandExecuted ( PlayerCommandBase command )
		{
			command.CommandExecuted -= HandleCommandExecuted;
			if(_commandQueue.Count > 0)
			{
				CurrentCommand = _commandQueue.Dequeue ();
			}
			else
			{
				CurrentCommand = null;
			}
		}

		private void QueueCommand ( PlayerCommandBase command )
		{
			if(_commandQueue.Count > 0 || CurrentCommand != null)
			{
				_commandQueue.Enqueue ( command );
			}
			else
			{
				CurrentCommand = command;
			}
		}

		#region Unity messages
		protected void Update()
		{
			if(CurrentCommand != null)
			{
				CurrentCommand.Update ();
			}
		}

		protected void OnDestroy()
		{
			if(CurrentCommand != null)
			{
				CurrentCommand.CommandExecuted -= HandleCommandExecuted;
			}
		}
		#endregion Unity messages

		public void AddCommand(PlayerCommandBase command)
		{
			if(_queueCommands)
			{
				QueueCommand ( command );
			}
			else
			{
				CurrentCommand = command;
			}
		}
		
	}
}
