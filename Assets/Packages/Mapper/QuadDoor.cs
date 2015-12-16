using UnityEngine;
using System.Collections;

namespace MapperSystem {
	[RequireComponent(typeof(MeshRenderer))]
	[RequireComponent(typeof(MeshFilter))]
	public class QuadDoor : MonoBehaviour {
		public const int QUAD_NUMBER = 4;

		Mesh _mesh;

		void OnEnable() {
			var filt = GetComponent<MeshFilter>();
			_mesh = filt.mesh;
			_mesh.MarkDynamic();
			_mesh.bounds = new Bounds(Vector3.zero, 1000f * Vector3.one);
		}
		void OnDisable() {
			Destroy(_mesh);
		}
		void Update() {
			
		}

		void FixedSizeArray<T>(ref T[] inputs, int fixedLength) {
			if (inputs == null) {
				inputs = new T[fixedLength];
				return;
			}
			if (inputs.Length == fixedLength)
				return;
			
			var tmp = inputs;
			inputs = new T[fixedLength];
			System.Array.Copy (tmp, inputs, Mathf.Min(tmp.Length, inputs.Length));
		}
	}
}
