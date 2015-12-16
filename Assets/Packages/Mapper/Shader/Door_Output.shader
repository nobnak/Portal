Shader "Door/Output" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (0.0, 0.5, 0.5, 0.5)
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Overlay" "Replacement"="Door" }
		Blend SrcAlpha OneMinusSrcAlpha
		ZTest Always ZWrite Off Cull Off Fog { Mode Off }

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
			struct v2f {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			float4x4 _InputCameraVP;

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			
			v2f vert(appdata v) {
				v2f o;
				o.vertex = float4(2 * v.uv - 1, 0, 1);
				//float4 posClip = mul(_InputCameraVP, mul(_Object2World, v.vertex));
				//o.uv = (0.5 / posClip.w) * posClip.xy + 0.5;
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag(v2f i) : SV_Target {
				fixed4 col = tex2D(_MainTex, i.uv);
				return col * _Color;
			}
			ENDCG
		}
	}
}
