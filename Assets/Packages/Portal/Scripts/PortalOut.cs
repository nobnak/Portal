using UnityEngine;
using System.Collections;
using System.Text;

namespace PortalSystem {
	
	[ExecuteInEditMode]
	public class PortalOut : PortalBase {
		public static readonly Color GIZMO_COLOR = Color.green;

		void OnEnable() {
			CheckInit();
		}
		void OnDisable() {
			Release();
		}
		void OnDrawGizmos() {
			DrawGizmo(GIZMO_COLOR);
		}
	}
}