Shader "Unlit/Portal" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (0.0, 0.5, 0.5, 0.5)
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		ZTest Always ZWrite Off Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile COLOR TEXTURE UV HIDDEN
			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
			struct v2f {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			
			v2f vert (appdata v) {
				v2f o;
				#ifdef HIDDEN
				o.vertex = 0;
				#else
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				#endif
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target {
				#if defined(UV)
				return float4(i.uv, 0.0, 0.5);
				#endif

				#
				fixed4 col = tex2D(_MainTex, i.uv);
				return col * _Color;
				#endif
			}
			ENDCG
		}
	}
}
