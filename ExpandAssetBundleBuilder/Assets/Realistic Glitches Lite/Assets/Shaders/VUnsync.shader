Shader "Hidden/VUnsync"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ValueX("height shift", Range(-1,1)) = 0.5
	}
	SubShader
	{
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			half _ValueX;

			struct v2f {
			   float4 pos : POSITION;
			   half2 uv : TEXCOORD0;
			};

//			int rand(int n){
//				n = (n << 13) ^ n; 
//  				return (n * (n*n*15731+789221) + 1376312589) & 0x7fffffff;
//			}
			   
			//Our Vertex Shader 
			v2f vert (appdata_img v){
			   v2f o;
			   o.pos = UnityObjectToClipPos (v.vertex);
			   o.uv = MultiplyUV (UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
			   return o; 
			}

			    
			uniform sampler2D _MainTex; //Reference in Pass is necessary to let us use this variable in shaders
			uniform sampler2D _ProcessedTex;

			fixed4 frag(v2f i) : COLOR {

				_ProcessedTex = _MainTex;

				v2f j = i;
				i.uv.y += _ValueX;
				if (i.uv.y > 0)
					j.uv.y = i.uv.y - 1;
				else
					j.uv.y = i.uv.y + 1;

		       	float4 m = tex2D(_MainTex, i.uv);
				float4 p = tex2D(_ProcessedTex, j.uv);

				if (_ValueX < 0 && -i.uv.y + _ValueX < _ValueX || _ValueX > 0 && 1 - i.uv.y + _ValueX > _ValueX ) 
					return m;
				else
					return p;	
         	}
			ENDCG
		}
	}
}
