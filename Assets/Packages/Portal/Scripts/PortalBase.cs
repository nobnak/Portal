using UnityEngine;
using System.Collections;
using System.Text;

namespace PortalSystem {
	
	public class PortalBase : MonoBehaviour {
		public static readonly Vector3[] QUAD_VERTICES = new Vector3[]{
			new Vector3(-0.5f, -0.5f, 0f), new Vector3(-0.5f, 0.5f, 0f), new Vector3(0.5f, 0.5f, 0f), new Vector3(0.5f, -0.5f, 0f)
		};
		public static readonly Vector2[] QUAD_UVS = new Vector2[]{
			new Vector2(0f, 0f), new Vector2(0f, 1f), new Vector2(1f, 1f), new Vector2(1f, 0f)
		};
		public static readonly int[] QUAD_TRIANGLES = new int[]{ 0, 1, 2, 0, 2, 3 };

		protected MeshFilter _filt;
		protected Mesh _mesh;
		protected Vector3[] _vertices;
		protected Vector2[] _uvs;

		public void CheckInit() {
			if (_filt == null)
				if ((_filt = GetComponent<MeshFilter>()) == null)
					_filt = gameObject.AddComponent<MeshFilter>();				

			if (_mesh == null) {
				_mesh = GenerateQuad();
				_mesh.MarkDynamic();
				_vertices = _mesh.vertices;
				_uvs = _mesh.uv;
				_filt.sharedMesh = _mesh;
			}
		}
		public void Release() {
			if (Application.isPlaying)
				Destroy(_mesh);
			else
				DestroyImmediate(_mesh);
		}
		public void Connect(Matrix4x4 pairModel, Camera cam, Vector3[] pairVertices) {
			var vertexCount = pairVertices.Length;
			for (var i = 0; i < vertexCount; i++)
				_uvs[i] = (Vector2)cam.WorldToViewportPoint(pairModel.MultiplyPoint3x4(pairVertices[i]));
			_mesh.uv = _uvs;
		}
		public Mesh GenerateQuad() {
			var mesh = new Mesh();
			mesh.name = "Quad Generative";
			mesh.vertices = QUAD_VERTICES;
			mesh.uv = QUAD_UVS;
			mesh.triangles = QUAD_TRIANGLES;
			mesh.RecalculateBounds();
			mesh.RecalculateNormals();
			return mesh;
		}
	}
}