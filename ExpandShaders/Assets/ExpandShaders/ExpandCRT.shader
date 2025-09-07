Shader "Hidden/CrtPostProcess" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	
	SubShader {
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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

			v2f vert (appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;

			uniform float u_time;
			uniform float u_bend;
			uniform float u_scanline_size_1;
			uniform float u_scanline_speed_1;
			uniform float u_scanline_size_2;
			uniform float u_scanline_speed_2;
			uniform float u_scanline_amount;
			uniform float u_vignette_size;
			uniform float u_vignette_smoothness;
			uniform float u_vignette_edge_round;
			uniform float u_noise_size;
			uniform float u_noise_amount;
			uniform half2 u_red_offset;
			uniform half2 u_green_offset;
			uniform half2 u_blue_offset;

			half2 crt_coords(half2 uv, float bend) {
				uv -= 0.5;
				uv *= 2.;
				uv.x *= 1. + pow(abs(uv.y) / bend, 2.);
				uv.y *= 1. + pow(abs(uv.x) / bend, 2.);

				uv /= 2.5;
				return uv + 0.5;
			}

			float vignette(half2 uv, float size, float smoothness, float edgeRounding) {
				uv -= .5;
				uv *= size;
				float amount = sqrt(pow(abs(uv.x), edgeRounding) + pow(abs(uv.y), edgeRounding));
				amount = 1. - amount;
				return smoothstep(0, smoothness, amount);
			}

			float scanline(half2 uv, float lines, float speed) {
				return sin(uv.y * lines + u_time * speed);
			}

			float random(half2 uv) {
				return frac(sin(dot(uv, half2(15.1511, 42.5225))) * 12341.51611 * sin(u_time * 0.03));
			}

			float noise(half2 uv) {
				half2 i = floor(uv);
				half2 f = frac(uv);

				float a = random(i);
				float b = random(i + half2(1., 0.));
				float c = random(i + half2(0, 1.));
				float d = random(i + half2(1., 1.));

				half2 u = smoothstep(0., 1., f);

				return lerp(a, b, u.x) + (c - a) * u.y * (1. - u.x) + (d - b) * u.x * u.y;
			}

			fixed4 frag (v2f i) : SV_Target {
				half2 crt_uv = crt_coords(i.uv, u_bend);
				fixed4 col;
				col.r = tex2D(_MainTex, crt_uv + u_red_offset).r;
				col.g = tex2D(_MainTex, crt_uv + u_green_offset).g;
				col.b = tex2D(_MainTex, crt_uv + u_blue_offset).b;
				col.a = tex2D(_MainTex, crt_uv).a;

				float s1 = scanline(i.uv, u_scanline_size_1, u_scanline_speed_1);
				float s2 = scanline(i.uv, u_scanline_size_2, u_scanline_speed_2);

				col = lerp(col, fixed(s1 + s2), u_scanline_amount);

				return lerp(col, fixed(noise(i.uv * u_noise_size)), u_noise_amount) * vignette(i.uv, u_vignette_size, u_vignette_smoothness, u_vignette_edge_round);
			}
			ENDCG
		}
	}
}