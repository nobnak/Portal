using UnityEngine;
using System.Collections;

namespace PortalSystem {
	[ExecuteInEditMode]
	public class Example : MonoBehaviour {
		public Camera targetCamera;
		public Transform background;
		public Texture2D inputSize;
		public Material portalOut;

		RenderTexture _inputTex;

		void OnEnable() {
			var height = 2f * targetCamera.orthographicSize;
			background.localScale = new Vector3(height * targetCamera.aspect, height, 1f);
		}
		void Update () {
			if (_inputTex == null || _inputTex.width != inputSize.width || _inputTex.height != inputSize.height) {
				ReleaseTex();
				_inputTex = CreateTex(inputSize.width, inputSize.height);
				targetCamera.targetTexture = _inputTex;
				portalOut.mainTexture = _inputTex;
			}
		}

		void ReleaseTex() {
			if (Application.isPlaying)
				Destroy(_inputTex);
			else
				DestroyImmediate(_inputTex);
		}
		RenderTexture CreateTex(int width, int height) {
			var tex = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);
			tex.wrapMode = TextureWrapMode.Clamp;
			tex.filterMode = FilterMode.Bilinear;
			tex.antiAliasing = (QualitySettings.antiAliasing == 0 ? 1 : QualitySettings.antiAliasing);
			return tex;
		}
	}
}