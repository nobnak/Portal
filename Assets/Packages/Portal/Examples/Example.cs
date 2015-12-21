using UnityEngine;
using System.Collections;

namespace PortalSystem {
	[ExecuteInEditMode]
	public class Example : MonoBehaviour {
		public enum KeywordEnum { ONLY_COLOR = 0, ONLY_TEXTURE, COLOR_TEXTURE, UV, HIDDEN }
		public enum DebugModeEnum { Release = 0, UV, Setup }

		public DebugModeEnum debugMode;
		public int inputWidth = 1920;
		public int inputHeight = 1080;
		public Camera inputCamera;

		public Material portaiIn;
		public Material portalOut;

		RenderTexture _inputTex;

		void Update () {
			if (_inputTex == null || _inputTex.width != inputWidth || _inputTex.height != inputHeight) {
				ReleaseTex();
				_inputTex = CreateTex(inputWidth, inputHeight);
			}

			if (inputCamera != null)
				inputCamera.targetTexture = _inputTex;

			if (portaiIn != null && portalOut != null) {
				portalOut.mainTexture = _inputTex;
				UpdateDebugMode();
			}
		}

		void UpdateDebugMode() {
			switch (debugMode) {
			case DebugModeEnum.Release:
				SwitchKeyword(portaiIn, KeywordEnum.HIDDEN.ToString());
				SwitchKeyword(portalOut, KeywordEnum.ONLY_TEXTURE.ToString());
				break;
			case DebugModeEnum.UV:
				SwitchKeyword(portaiIn, KeywordEnum.UV.ToString());
				SwitchKeyword(portalOut, KeywordEnum.UV.ToString());
				break;
			case DebugModeEnum.Setup:
				SwitchKeyword(portaiIn, KeywordEnum.ONLY_COLOR.ToString());
				SwitchKeyword(portalOut, KeywordEnum.COLOR_TEXTURE.ToString());
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
