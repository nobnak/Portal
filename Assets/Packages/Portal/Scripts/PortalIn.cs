using UnityEngine;
using System.Collections;
using System.Text;

namespace PortalSystem {
	
	[ExecuteInEditMode]
	public class PortalIn : PortalBase {
		public Camera targetCamera;
		public PortalOut pair;

		void OnEnable() {
			CheckInit();
		}
		void OnDisable() {
			Release();
		}
		void Update() {
			if (targetCamera != null && pair != null)
				pair.ConnectUV(this, targetCamera);
		}
	}
}