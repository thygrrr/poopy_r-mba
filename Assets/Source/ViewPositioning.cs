﻿using System.Collections.Generic;
using Entitas;
using UnityEngine;


public class UpdateViewPositions : IExecuteSystem, ISetPool
{
	private Group _movers;

	public void SetPool(Pool pool)
	{
		_movers = pool.GetGroup(Matcher.AllOf(Matcher.View, Matcher.GridPosition));
	}

	public void Execute()
	{
		foreach (var mover in _movers.GetEntities())
		{
			mover.view.transform.position = Vector3.MoveTowards(mover.view.transform.position, mover.gridPosition.WorldPosition(), Time.deltaTime);
		}
	}

	public TriggerOnEvent trigger
	{
		get { return Matcher.View.OnEntityAdded(); }
	}

	public IMatcher ensureComponents
	{
		get { return Matcher.AllOf(Matcher.View, Matcher.GridPosition); }
	}
}


public class InitViewPositions : IReactiveSystem, IEnsureComponents
{
	public void Execute(List<Entity> entities)
	{
		foreach (var entity in entities)
		{
			entity.view.transform.position = entity.gridPosition.WorldPosition();
		}
	}

	public TriggerOnEvent trigger
	{
		get { return Matcher.View.OnEntityAdded(); }
	}

	public IMatcher ensureComponents
	{
		get { return Matcher.AllOf(Matcher.View, Matcher.GridPosition); }
	}
}



public class LinkViewsToEntities : IReactiveSystem, ISetPool
{
	private Pool _pool;
	public void Execute(List<Entity> entities)
	{
		foreach (var entity in entities)
		{
			//Bind Gameobject
			entity.view.transform.gameObject.Link(entity, _pool);

			//Bind Rigidbody
			var rigidbody = entity.view.transform.GetComponent<Rigidbody>();
			if (rigidbody != null)
			{
				entity.AddPhysics(rigidbody);
			}
		}
	}

	public TriggerOnEvent trigger
	{
		get
		{
			return Matcher.View.OnEntityAdded();
		}
	}

	public void SetPool(Pool pool)
	{
		_pool = pool;
	}
}