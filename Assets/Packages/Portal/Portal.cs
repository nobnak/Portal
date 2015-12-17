using UnityEngine;
using System.Collections;
using System.Text;

namespace PortalSystem {
	
	[ExecuteInEditMode]
	public class Portal : MonoBehaviour {
		public const string KEYWORD_DEFAULT = "DEFAULT";
		public const string KEYWORD_UV = "UV";
		public const string KEYWORD_HIDDEN = "HIDDEN";

		public Camera targetCamera;
		public Portal pair;
		public Mesh sharedMesh;

		MeshFilter _filt;
		Mesh _mesh;
		Vector3[] _vertices;
		Vector2[] _uvs;

		void OnEnable() {
			_filt = GetComponent<MeshFilter>();
			_mesh = Instantiate(sharedMesh);
			_mesh.MarkDynamic();
			_vertices = _mesh.vertices;
			_uvs = _mesh.uv;
			_filt.sharedMesh = _mesh;
		}
		void OnDisable() {
			if (Application.isPlaying)
				Destroy(_mesh);
			else
				DestroyImmediate(_mesh);
		}
		void Update() {


			if (targetCamera != null && pair != null)
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