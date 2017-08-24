using System;
using System.Diagnostics;

namespace Sequencer
{
	/// <summary>
	/// Creates 
	/// </summary>
	public class Sequence
	{
		private Node _head;

		private Node Head
		{
			get { return _head; }
			set {
				if (Head == null)
					Current = value;
				_head = value;
			}
		}
		private Node Current;
		private Node Tail
		{
			get
			{
				var tail = Head;
				if (tail != null)
				{
					while (tail.Next != null)
					{
						tail = tail.Next;
					}
				}
				return tail;
			}
		}

		/// <summary>Creates and adds new action to the sequence.</summary>
		/// <param name="action"><see cref="Action"/> to be executed.</param>
		/// <returns>The sequence itself.</returns>
		public Sequence Add(Action action)
		{
			ActionNode newNode = new ActionNode(action);
			if(Head == null)
			{
				Head = newNode;
				return this;
			}
			Tail.Next = newNode;
			return this;
		}
		/// <summary>Creates and adds new delay to the sequence.</summary>
		/// <param name="timeMs">Time to wait (in milliseconds).</param>
		/// <returns>The sequence itself.</returns>
		public Sequence Delay(int timeMs)
		{
			Tail.Next = new DelayNode(timeMs);
			return this;
		}
		/// <summary>
		/// Runs sequence, decreases remaining time of current delay node
		/// </summary>
		/// <param name="delta">Time delta</param>
		/// <returns>Is sequence finished</returns>
		public bool Update(float delta)
		{
			if (Current == null) return false;
			Current.Execute(delta);
			if (Current.Done)
				Current = Current.Next;
			return true;
		}
	}
}