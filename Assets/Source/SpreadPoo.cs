﻿using Entitas;
using Entitas.Unity.VisualDebugging;
using PicaVoxel;
using UnityEngine;
using UnityEngine.UI;

public class SpreadPoo : IInitializeSystem, IExecuteSystem, ISetPool
{
	private Pool _pool;
	private Group _group;

	public void SetPool(Pool pool)
	{
		_pool = pool;
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Dirty, Matcher.View, Matcher.Charge));
	}

	private PicaVoxel.Volume _floor;

	private Color RandomColor(bool dirty)
	{
		if (!dirty) return new Color(UnityEngine.Random.Range(0.8f, 0.85f), UnityEngine.Random.Range(0.85f, 0.9f), UnityEngine.Random.Range(0.75f, 0.8f), 1.0f);

		return new Color(UnityEngine.Random.Range(0.3f, 0.35f), UnityEngine.Random.Range(0.2f, 0.3f), UnityEngine.Random.Range(0.0f, 0.1f), 1.0f);
	}

	public void Execute()
	{
		//Show dirty voxels from the filth layer in circle around dirty object's position
		foreach (var entity in _group.GetEntities())
		{
			int score = 0;

			//Stop spreading if battery is dead.
			if (entity.charge.value <= 0) continue;

			var pos = entity.view.transform.position;
			pos.x += UnityEngine.Random.Range(-0.2f, 0.2f);
			pos.z += UnityEngine.Random.Range(-0.2f, 0.2f);

			var voxel = _floor.GetVoxelAtWorldPosition(pos);
			if (voxel != null && voxel.Value.State != VoxelState.Active)
			{
				_floor.SetVoxelStateAtWorldPosition(pos, VoxelState.Active);
				score++;
			}

			pos = entity.view.transform.position;
			pos.x += UnityEngine.Random.Range(-0.08f, 0.08f);
			pos.z += UnityEngine.Random.Range(-0.08f, 0.08f);

			voxel = _floor.GetVoxelAtWorldPosition(pos);
			if (voxel != null && voxel.Value.State != VoxelState.Active)
			{
				_floor.SetVoxelStateAtWorldPosition(pos, VoxelState.Active);
				score++;
			}
			pos = entity.view.transform.position;
			pos.x += UnityEngine.Random.Range(-0.06f, 0.06f);
			pos.z += UnityEngine.Random.Range(-0.06f, 0.06f);

			voxel = _floor.GetVoxelAtWorldPosition(pos);
			if (voxel != null && voxel.Value.State != VoxelState.Active)
			{
				_floor.SetVoxelStateAtWorldPosition(pos, VoxelState.Active);
				score++;
			}

			//Mark tiles as dirty
			_pool.tileGrid.tiles[entity.gridPosition.x, entity.gridPosition.y].isDirty = true;

			float is_spread = _pool.GetGroup(Matcher.AllOf(Matcher.Tile, Matcher.Dirty).NoneOf(Matcher.Impassible)).count;
			float to_spread = _pool.GetGroup(Matcher.AllOf(Matcher.Tile).NoneOf(Matcher.Impassible)).count;

			_pool.ReplaceScore(_pool.score.value + score);
			_pool.ReplacePercentage(Mathf.Min(100.0f, is_spread/to_spread*100.0f));

			GameObject.FindGameObjectWithTag("Percentage").GetComponent<Text>().text = string.Format("Spread {0}%", (int)Mathf.Round(_pool.percentage.value));
			GameObject.FindGameObjectWithTag("Score").GetComponent<Text>().text = string.Format("{0:0000000}", _pool.score.value);
		}
	}

	public void Initialize()
	{
		_floor = GameObject.FindGameObjectWithTag("Floor").GetComponent<Volume>();
	}
}