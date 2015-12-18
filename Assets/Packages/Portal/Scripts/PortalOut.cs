using UnityEngine;
using System.Collections;
using System.Text;

namespace PortalSystem {
	
	[ExecuteInEditMode]
	public class PortalOut : PortalBase {

		void OnEnable() {
			CheckInit();
		}
		void OnDisable() {
			Release();
		}
	}
}