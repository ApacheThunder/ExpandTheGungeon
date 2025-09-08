Shader "Expand/ChromaKey"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_OverlayTex("Overlay", 2D) = "white" {}
		_ChromaKey("Green Color", Color) = (0,1,0,0)
		_Sensitivity("Threshold Sensitivity", Range(0,1)) = 0.2
		_Smooth("Smoothing", Range(0,1)) = 0.5
		[Toggle]_ShowBackground("Show Only Background", Float) = 0
		[Toggle]_ShowOriginal("Show Original Video", Float) = 0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _OverlayTex;
			float3 _ChromaKey;
			float _Sensitivity;
			float _Smooth;
			float _ShowOriginal;
			float _ShowBackground;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_OverlayTex, i.uv);
				
				if (_ShowBackground)
				{
					fixed4 col2 = tex2D(_MainTex, i.uv);
					col = col2;
				}
				else if (!_ShowOriginal)
				{
					fixed4 col2 = tex2D(_MainTex, i.uv);

					float maskY = 0.2989 * _ChromaKey.r + 0.5866 * _ChromaKey.g + 0.1145 * _ChromaKey.b;
					float maskCr = 0.7132 * (_ChromaKey.r - maskY);
					float maskCb = 0.5647 * (_ChromaKey.b - maskY);

					float Y = 0.2989 * col.r + 0.5866 * col.g + 0.1145 * col.b;
					float Cr = 0.7132 * (col.r - Y);
					float Cb = 0.5647 * (col.b - Y);

					float alpha = smoothstep(_Sensitivity, _Sensitivity + _Smooth, distance(float2(Cr, Cb), float2(maskCr, maskCb)));

					col = (alpha * col) + ((1 - alpha) * col2);
				}
				return col;
			}
			ENDCG
		}
	}
}

