using UnityEngine;
using System.Collections;
using System.Text;

namespace PortalSystem {
	public class Portal : MonoBehaviour {
		public Camera targetCamera;
		public Portal pair;

		MeshFilter _filt;
		Mesh _mesh;
		Vector3[] _vertices;
		Vector2[] _uvs;

		void OnEnable() {
			_filt = GetComponent<MeshFilter>();
			_mesh = _filt.mesh;
			_mesh.MarkDynamic();
			_vertices = _mesh.vertices;
			_uvs = _mesh.uv;
		}
		void OnDisable() {
			Destroy(_mesh);
		}
		void Update() {
			if (targetCamera == null || pair == null)
				return;

			pair.Connect(transform.localToWorldMatrix, targetCamera, _vertices);			
		}

		void Connect(Matrix4x4 pairModel, Camera cam, Vector3[] pairVertices) {
			var vertexCount = pairVertices.Length;
			for (var i = 0; i < vertexCount; i++)
				_uvs[i] = (Vector2)cam.WorldToViewportPoint(pairModel.MultiplyPoint3x4(pairVertices[i]));
			_mesh.uv = _uvs;
		}
	}
}