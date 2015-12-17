using UnityEngine;
using System.Collections;
using System.Text;

namespace PortalSystem {
	[ExecuteInEditMode]
	public class Portal : MonoBehaviour {
		public Camera targetCamera;
		public Portal pair;

		Mesh _mesh;
		Vector3[] _vertices;
		Vector2[] _uvs;

		void OnEnable() {
			var filt = GetComponent<MeshFilter>();
			_mesh = Instantiate(filt.sharedMesh);
			_mesh.MarkDynamic();
			_vertices = _mesh.vertices;
			_uvs = _mesh.uv;
		}
		void OnDisable() {
			if (Application.isPlaying)
				Destroy(_mesh);
			else
				DestroyImmediate(_mesh);
		}
		void Update() {
			if (targetCamera == null || pair == null)
				return;

			Connect(transform.localToWorldMatrix, targetCamera, _vertices);			
		}

		void Connect(Matrix4x4 pairModel, Camera cam, Vector3[] pairVertices) {
			var vertexCount = pairVertices.Length;
			for (var i = 0; i < vertexCount; i++)
				_uvs[i] = (Vector2)cam.WorldToViewportPoint(pairModel.MultiplyPoint3x4(pairVertices[i]));
			_mesh.uv = _uvs;

			var buf = new StringBuilder("UV :");
			for (var i = 0; i < _uvs.Length; i++)
				buf.AppendFormat(" <{0:f2},{1:f2}>", _uvs[i].x, _uvs[i].y);
			Debug.LogFormat("Update UV : {0}", buf);
		}
	}
}