using UnityEngine;
using System.Collections;

namespace PortalSystem {
		
	public class Demo : MonoBehaviour {
		public const float THREE_SIX_ZERO = 360f;

		public GameObject cubefab;
		public int count;
		public float radius;
		public float noiseTransitionSpeed;
		public float rotationSpeed;

		GameObject[] _cubes;

		void Start() {
			_cubes = new GameObject[count];
			for (var i = 0; i < count; i++) {
				var pos = radius * Random.insideUnitSphere;
				var cube = _cubes[i] = (GameObject)Instantiate(cubefab, pos, Random.rotationUniform);
				cube.transform.SetParent(transform, false);
			}
		}

		void Update() {
			var y = Time.timeSinceLevelLoad * noiseTransitionSpeed;
			transform.localRotation *= Quaternion.Euler(Time.deltaTime * rotationSpeed * new Vector3(
				DerivativeNoise(0f, y), DerivativeNoise(10f, y), DerivativeNoise(20f, y)));
		}

		float DerivativeNoise(float x, float y) {
			return THREE_SIX_ZERO * (Mathf.PerlinNoise(x, y) - 0.5f);
		}
	}
}
