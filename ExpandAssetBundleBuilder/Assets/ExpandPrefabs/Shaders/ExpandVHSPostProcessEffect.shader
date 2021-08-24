Shader "Expand/VHSPostProcessEffect" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_VHSTex ("Base (RGB)", 2D) = "white" {}
		_yScanline ("Vertical Shift", Float) = 1
		_xScanline ("Horizontal Shift", Float) = 1
		// Intensity of shifting from one region compared to regions above and below.
		_xShiftIntensity ("Horizontal Tiling Intensity", Float) = 500
		// This controls how much the screen shifts to the left near the bottom of the screen. Higher values = more of bottom is shifted then the top is.
		_xShift ("Horizontal Tiling Bottom Bias", Float) = 0.025
		// Added to allow disabling of color bleed effect
		[MaterialToggle] _colorBleedToggle("Color Bleed (0 = off)", Float) = 1
	}

	SubShader {
		Pass {
			ZTest Always Cull Off ZWrite Off
			Fog { Mode off }
					
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest 
			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _VHSTex;
			
			float _yScanline;
			float _xScanline;
			float _xShift;
			float _xShiftIntensity;
			float _colorBleedToggle;
			float rand(float3 co){
			     return frac(sin( dot(co.xyz ,float3(12.9898,78.233,45.5432) )) * 43758.5453);
			}
 
			fixed4 frag (v2f_img i) : COLOR{
				fixed4 vhs = tex2D (_VHSTex, i.uv);
				
				float dx = 1-abs(distance(i.uv.y, _xScanline));
				float dy = 1-abs(distance(i.uv.y, _yScanline));
				
				dy = ((int)(dy*15))/15.0;
				dy = dy;
				i.uv.x += dy * _xShift + rand(float3(dy,dy,dy)).r/_xShiftIntensity; //0.025;
				
				float white = (vhs.r+vhs.g+vhs.b)/3;
				
				if(dx > 0.99)
					i.uv.y = _xScanline;				
				// Disabled this as it caused some repeat of left side of screen on the right side.
				// i.uv.x = i.uv.x % 1;
				// i.uv.y = i.uv.y % 1;
				
				fixed4 c = tex2D (_MainTex, i.uv);
				
				if (_colorBleedToggle == 1) 
				{
					float bleed = tex2D(_MainTex, i.uv + float2(0.01, 0)).r;
					bleed += tex2D(_MainTex, i.uv + float2(0.02, 0)).r;
					bleed += tex2D(_MainTex, i.uv + float2(0.01, 0.01)).r;
					bleed += tex2D(_MainTex, i.uv + float2(0.02, 0.02)).r;
					bleed /= 6;
				
					if(bleed > 0.1){
						vhs += fixed4(bleed * _xScanline, 0, 0, 0);
					}
				}
				
				float x = ((int)(i.uv.x*320))/320.0;
				float y = ((int)(i.uv.y*240))/240.0;
				
				c -= rand(float3(x, y, _xScanline)) * _xScanline / 5;
				return c + vhs;
			}
			ENDCG
		}
	}
Fallback off
}