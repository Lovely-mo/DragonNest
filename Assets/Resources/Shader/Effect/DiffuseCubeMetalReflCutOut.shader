﻿Shader "Custom/Effect/DiffuseCubeMetalReflCutout" 
{
	Properties 
	{
		_MainTex ("BaseColor", 2D) = "white" {}
		_Mask ("R-光滑度  G-金属性", 2D) = "white" {}      
		//_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5 
		_Cube ("Cubemap", CUBE) = "" {}
	     
		_MetalColor("RGB-反射颜色  ", Color) = (1,1,1,0.5)
		_RimColor ("Rim Color", Color) = (0.353, 0.353, 0.353,0.0)
		_LightArgs("x:MainColor Scale y:Light Scale z:not used w: Rim Power",Vector)= (1.0,1.0,0.7,0.55)
		_EffectArgs("x:Metal Scale y:Glass Scale z:Cap Scale w:Not Used",Vector) = (1.0,1.0,2.0,0.0)
	      
    }
	Category
	{
		Tags{ "Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutoutG" }
		SubShader
		{			
			LOD 100
			Pass
			{
				Tags { "LightMode" = "ForwardBase" }
				CGPROGRAM
				//define
				#define MASKTEX
				#define CUTOUT
				#define CUTOUTG
				#define RIMLIGHT
				#define VERTEXLIGHTON
				#define METALREFL
				#define CUBE
				#define DEFAULTBASECOLOR
				//head
				#include "../Include/CommonHead_Include.cginc"

				//vertex&fragment
				#pragma vertex vert
				#pragma fragment frag 
				//include
				#include "../Include/CommonBasic_Include.cginc"

				ENDCG
			}
		}
	}
}
