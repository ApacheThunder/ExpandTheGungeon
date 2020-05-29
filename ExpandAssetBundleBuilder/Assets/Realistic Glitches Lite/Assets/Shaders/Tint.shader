Shader "Hidden/Tint"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

		_ValueX ("Y Shift", float) = 1
		_ValueY ("U Shift", float) = 1
		_ValueZ ("V Shift", float) = 1
		_Switch ("Swap U and V channels - Full version only", float) = 0
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

			half _ValueX; half _ValueY; half _ValueZ;

			struct v2f {
			   float4 pos : POSITION;
			   half2 uv : TEXCOORD0;
			};

			//Vertex Shader 
			v2f vert (appdata_img v){
			   v2f o;
			   o.pos = UnityObjectToClipPos (v.vertex);
			   o.uv = MultiplyUV (UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
			   return o; 
			}
			
			//sampler2D _MainTex;
			uniform sampler2D _MainTex; //Reference in Pass is necessary to let us use this variable in shaders

			// Fragment shader
			fixed4 frag (v2f_img i) : SV_Target
			{

				float4 m = tex2D(_MainTex, i.uv);
//				i.uv.x -= _ValueZalue * 0.01f;
//				float4 l = tex2D(_MainTex, i.uv);
//				i.uv.x += _ValueZalue * 0.01f;
//				float4 r = tex2D(_MainTex, i.uv);

				float y = (0.299 * m.r + 0.587 * m.g + 0.114 * m.b) * _ValueX;
				float u = (-0.147 * m.r - 0.289 * m.g + 0.436 * m.b) * _ValueY;
				float v = (0.615 * m.r - 0.515 * m.g - 0.100 * m.b) * _ValueZ;


				half4 result = half4(y + 1.140 * v, y - 0.395 * u - 0.581 * v, y + 2.032 * u ,1); 

//				float4 result = m;
//				result.rgb = lerp(m, mix.rgb, 1);
				return result;
			}
			ENDCG
		}
	}
}
