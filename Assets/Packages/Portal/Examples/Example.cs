using UnityEngine;
using System.Collections;

namespace PortalSystem {
	[ExecuteInEditMode]
	public class Example : MonoBehaviour {
		public enum DebugModeEnum { Default = 0, UV, Setup }

		public DebugModeEnum debugMode;
		public Camera targetCamera;
		public Transform background;
		public Texture2D inputSize;
		public Material portalIn;
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
			}

            targetCamera.targetTexture = _inputTex;
            portalOut.mainTexture = _inputTex;

            ApplyDebugMode();
		}

		void ApplyDebugMode() {
			switch (debugMode) {
			default:
				SwitchKeyword(portalIn, Portal.KEYWORD_HIDDEN);
                SwitchKeyword(portalOut, Portal.KEYWORD_ONLY_TEXTURE);
				break;
			case DebugModeEnum.UV:
				SwitchKeyword(portalIn, Portal.KEYWORD_UV);
				SwitchKeyword(portalOut, Portal.KEYWORD_UV);
				break;
			case DebugModeEnum.Setup:
                SwitchKeyword(portalIn, Portal.KEYWORD_ONLY_COLOR);
                SwitchKeyword(portalOut, Portal.KEYWORD_COLOR_TEXTURE);
				break;
			}
		}
		void SwitchKeyword(Material mat, string keyword) {
			mat.shaderKeywords = null;
			mat.EnableKeyword(keyword);
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