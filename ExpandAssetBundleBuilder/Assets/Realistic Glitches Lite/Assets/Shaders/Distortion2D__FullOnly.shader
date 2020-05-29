Shader "Hidden/Distortion2D"
{
	Properties
	{
		_Intensity ("Displacement value", Range(0,1)) = 0.01
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Texture ("Displacement map (RGB)", 2D) = "black" {}
	}
	SubShader
	{
		Pass {
			CGPROGRAM

			}
			ENDCG
		}
	}
}
