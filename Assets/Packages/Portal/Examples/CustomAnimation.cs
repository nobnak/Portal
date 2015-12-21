using UnityEngine;
using System.Collections;

namespace PortalSystem {
		
	public class CustomAnimation : MonoBehaviour {
		public float timeScale = 0.01f;
		public float distScale = 10f;

		public AnimationCurve posX;
		public AnimationCurve posY;
		public AnimationCurve posZ;

		Vector3 _startPos;

		void Start() {
			_startPos = transform.localPosition;
		}

		void Update () {
			var t = timeScale * Time.timeSinceLevelLoad;
			var relativePos = new Vector3(distScale * posX.Evaluate(t), distScale * posY.Evaluate(t), distScale * posZ.Evaluate(t));
			transform.localPosition = relativePos + _startPos;
		}
	}
}