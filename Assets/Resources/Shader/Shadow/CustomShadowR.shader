﻿Shader "Custom/Scene/CustomShadowR" 
{
	Properties
	{
		[HideInInspector]_Mask("Mask (A)", 2D) = "white" {}
	}		
	SubShader
	{
		Tags{ "RenderType" = "Opaque" }
		Pass
		{
			Name "ShadowCasterR"

			Fog{ Mode Off }
			ZWrite On ZTest LEqual Cull Off
			Offset 1, 1
			ColorMask R
			CGPROGRAM			
			//include
			#include "../Include/Shadow_Include.cginc"
			#pragma vertex vertCustomCast
			#pragma fragment fragCustomCastR

			ENDCG
		}
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout" }
		Pass
		{
			Name "CutoutShadowCasterR"

			Fog{ Mode Off }
			ZWrite On ZTest LEqual Cull Off
			Offset 1, 1
			ColorMask R
			CGPROGRAM
			//include
			#include "../Include/Shadow_Include.cginc"
			#pragma vertex vertCustomCast
			#pragma fragment fragCustomCastRCutout

			ENDCG
		}
	}

SubShader
	{
		Tags{ "RenderType" = "TransparentCutoutG" }
		Pass
		{
			Name "CutoutShadowCasterG"

			Fog{ Mode Off }
			ZWrite On ZTest LEqual Cull Off
			Offset 1, 1
			ColorMask R
			CGPROGRAM
			#define CUTOUTG
			//include
			#include "../Include/Shadow_Include.cginc"
			#pragma vertex vertCustomCast
			#pragma fragment fragCustomCastRCutout

			ENDCG
		}
	}
}
