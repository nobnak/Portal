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
		public void ConnectUV(PortalBase pair, Camera cam) {
			var pair2world = pair.transform.localToWorldMatrix;
			var vertices = pair._vertices;
			var vertexCount = vertices.Length;
			var uvs = pair._uvs;
			for (var i = 0; i < vertexCount; i++)
				uvs[i] = (Vector2)cam.WorldToViewportPoint(pair2world.MultiplyPoint3x4(vertices[i]));
			_mesh.uv = uvs;
			pair._mesh.uv = uvs;
		}
		public void DrawGizmo(Color color) {
			Gizmos.color = color;
			Gizmos.DrawWireMesh(_mesh, transform.position, transform.rotation, transform.lossyScale);
		}
		public static Mesh GenerateQuad() {
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