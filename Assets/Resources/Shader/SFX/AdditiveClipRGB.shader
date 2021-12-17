// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SFX/AdditiveClipRGB" 
{
	Properties
	{
		_MainTex("Particle Texture", 2D) = "white" {}
		_ClipRange0("ClipRange", Vector) = (0.0, 0.0, 1.0, 1.0)
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Blend SrcAlpha One
		Cull Off ZWrite Off Fog{ Mode Off }
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			struct appdata_t 
			{
				half4 vertex : POSITION;
				fixed4 color : COLOR;
				half2 texcoord : TEXCOORD0;
			};

			struct v2f 
			{
				half4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				half2 texcoord : TEXCOORD0;
				half2 pos : TEXCOORD1;
			};
			sampler2D _MainTex;
			float4 _MainTex_ST;
			half4 _ClipRange0;
			fixed4 _TintColor;

			v2f vert(appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color*_TintColor * 2;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				o.pos.xy = o.vertex.xy;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 color = i.color * tex2D(_MainTex, i.texcoord);
				half2 inside1 = step(_ClipRange0.xy, i.pos.xy);
				half2 inside2 = step(i.pos.xy, _ClipRange0.zw);
				color.a *= inside1.x * inside1.y * inside2.x * inside2.y;
				return color;
			}
			ENDCG
		}
	}
}