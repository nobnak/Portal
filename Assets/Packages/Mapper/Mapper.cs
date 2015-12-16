using UnityEngine;
using System.Collections;

namespace MapperSystem {
	[RequireComponent(typeof(Camera))]
	public class Mapper : MonoBehaviour {
		public const string REPLACE_TAG = "Replacement";
		public const string GLOBAL_SHADER_PROP_INPUT_CAM_VP = "_InputCameraVP";

		public Camera inputCamera;
		public Shader outputShader;

		Camera _myCam;

		void OnEnable() {
			_myCam = GetComponent<Camera>();
			_myCam.SetReplacementShader(outputShader, REPLACE_TAG);
		}
		void OnDisable() {
			_myCam.ResetReplacementShader();
		}
		void Update() {
			var projMat = GL.GetGPUProjectionMatrix(inputCamera.projectionMatrix, false);
			Shader.SetGlobalMatrix(GLOBAL_SHADER_PROP_INPUT_CAM_VP, projMat * inputCamera.worldToCameraMatrix);
		}
	}
}