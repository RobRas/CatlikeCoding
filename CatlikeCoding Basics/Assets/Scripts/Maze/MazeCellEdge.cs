using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze {
	public abstract class MazeCellEdge : MonoBehaviour {
		[HideInInspector] public MazeCell cell, otherCell;

		[HideInInspector] public MazeDirection direction;

		public virtual void Initialize(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
			this.cell = cell;
			this.otherCell = otherCell;
			this.direction = direction;
			cell.SetEdge(direction, this);
			transform.parent = cell.transform;
			transform.localPosition = Vector3.zero;
			transform.localRotation = direction.ToRotation();
		}
	}
}