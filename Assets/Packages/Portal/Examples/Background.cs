using UnityEngine;
using System.Collections;

namespace PortalSystem {
	[ExecuteInEditMode]
	[RequireComponent(typeof(MeshRenderer))]
	public class Background : MonoBehaviour {
		public Camera targetCamera;

		Texture _tex;

		void OnEnable() {
			_tex = GetComponent<MeshRenderer>().sharedMaterial.mainTexture;
		}

		void Update() {
			var aspect = (float)_tex.width / _tex.height;
			var cameraHeight = 2f * targetCamera.orthographicSize;

			var forward = targetCamera.transform.forward;
			var distance = Mathf.Lerp(targetCamera.nearClipPlane, targetCamera.farClipPlane, 0.999f);
			transform.forward = forward;
			transform.position = forward * distance + targetCamera.transform.position;
			transform.localScale = new Vector3(aspect * cameraHeight, cameraHeight, 1f);
		}
	}
}