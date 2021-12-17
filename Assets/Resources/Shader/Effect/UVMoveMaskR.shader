﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Effect/UVMoveMaskR" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Mask("Mask (A)", 2D) = "white" {}
		_UVLength("UVLength",Float) = 8
		_UVTime("UVTime",Float) = 0.1
	}
	SubShader
	{
		LOD 100
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		
		Cull Off
		ZWrite Off
		Fog { Mode Off }
		Offset -1, -1
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
				
			#include "UnityCG.cginc"
	
			struct appdata_t
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};
	
			struct v2f
			{
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
			};
	
        	sampler2D _MainTex;				
			float4 _MainTex_ST;
			sampler2D _Mask;
			fixed _UVLength;
			fixed _UVTime;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				half timeInOneCycle = fmod(_Time.y,_UVLength*_UVTime);
				timeInOneCycle = floor(timeInOneCycle/_UVTime);
				o.texcoord.x += timeInOneCycle*(1/_UVLength);
				return o;
			}
				
			fixed4 frag (v2f i) : COLOR
			{
				fixed4 c = tex2D(_MainTex, i.texcoord);
				c.a = tex2D(_Mask, i.texcoord).r;
				return c;
			}
			ENDCG
		}
	}
}
