// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Expand/BlendTextures"
{
    Properties
    {
        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}        
        [NoScaleOffset] _BackgroundTex ("Background", 2D) = "black" {}
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 100
        Cull Off
        Blend One OneMinusSrcAlpha

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
                float4 uv_back : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _BackgroundTex;

            fixed _Opacity;
           
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.uv_back = ComputeGrabScreenPos(o.vertex);
                return o;
            }
           
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // sample the background
                float2 screenuv = i.uv_back.xy / i.uv_back.w;
                fixed4 back = tex2D(_BackgroundTex, screenuv);

                // blend main color and background together
                return fixed4(lerp(back.rgb, col.rgb, _Opacity) * col.a, col.a);
            }
            ENDCG
        }
    }
}