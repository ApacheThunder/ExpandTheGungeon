// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/Scanlines" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Intensity ("Black & White blend", Range (0, 1)) = 0
		_Color("Color - Full version only", Color) = (0,0,0,1) 
    	_ValueX("LinesSize", Range(1,10)) = 1 
	}
	SubShader {
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			half _ValueX; half _Intensity; half _Value;

			struct v2f {
			   float4 pos : POSITION;
			   half2 uv : TEXCOORD0;
			};
			   
			//Our Vertex Shader 
			v2f vert (appdata_img v){
			   v2f o;
			   o.pos = UnityObjectToClipPos (v.vertex);
			   o.uv = MultiplyUV (UNITY_MATRIX_TEXTURE0, v.texcoord.xy);
			   return o; 
			}
			    
			uniform sampler2D _MainTex; //Reference in Pass is necessary to let us use this variable in shaders

			fixed4 frag(v2f i) : COLOR {
				float4 c = tex2D(_MainTex, i.uv);
				
		        fixed p = i.uv.y;
		        if((int)(p*_ScreenParams.y/floor(_ValueX))%2==0) 
		        	return c;
		        else {
			        fixed4 result = c;
					result.rgb = lerp(c.rgb, 0, _Intensity);
					return result; 
				}
         	}

			ENDCG
		}
	}
}
