using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze {
	public class MazeWall : MazeCellEdge {
		[SerializeField] Transform wall;

		public override void Initialize(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
			base.Initialize(cell, otherCell, direction);
			wall.GetComponent<Renderer>().material = cell.room.settings.wallMaterial;
		}
	}
}