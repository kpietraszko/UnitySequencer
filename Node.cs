using System;
namespace Sequencer
{
	internal abstract class Node
	{
		internal Node Next;
		internal bool Done = false;

		public abstract void Execute(float delta);
	}
	class ActionNode : Node
	{
		Action Action;

		public ActionNode(Action action)
		{
			Action = action;
		}

		public override void Execute(float delta)
		{
			Action();
			Done = true;
		}
	}
	class DelayNode : Node
	{
		float RemainingTime; //seconds
		public DelayNode(int timeMs)
		{
			RemainingTime = Math.Max(timeMs/1000f, 0f);
		}

		public override void Execute(float delta)
		{
			//if time passed activate next
			RemainingTime -= delta;
			if (RemainingTime <= 0)
				Done = true;
		}
	}
}