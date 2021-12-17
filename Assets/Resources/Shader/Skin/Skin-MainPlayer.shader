﻿Shader "Custom/Skin/MainPlayer" 
{
	Properties
	{  
		_Tex0("Texture0", 2D) = "black" {}		
		_Tex1("Texture1", 2D) = "black" {}
		_Tex2("Texture2", 2D) = "black" {}			
		_Tex3("Texture3", 2D) = "black" {}		
		_Tex4("Texture4", 2D) = "black" {}		
		_Tex5("Texture5", 2D) = "black" {}		
		_Tex6("Texture6", 2D) = "black" {}		
		_Tex7("Texture7", 2D) = "black" {}	
		_HairColor("Hair Color", Color) = (1, 1, 1, 1)
		_Color ("Additive Color", Color) = (1, 1, 1, 1)
		_RimColor("Rim Color", Color) = (0.353, 0.353, 0.353, 0.0)
		_LightArgs("x:MainColor Scale y:Light Scale z:Unused w: Rim Power",Vector) = (1.0,0.21,0.0,3.0)
		_UIRimMask("UI Rim Mask",Vector) = (1,1,0,0)
		//_OutLineColor ("OutLine Color", Color) = (0,0.93,1,0.86)
	}
	SubShader 
	{  
		Tags { "RenderType" = "Opaque" }
        LOD 100      

			//Pass {
			//Name "OutLine"
			//	ZTest Greater
			//	Lighting Off
			//	ZWrite Off
			//	Blend SrcAlpha OneMinusSrcAlpha

			//	CGPROGRAM
			//	//define
			//	#define RIMON

			//	#pragma vertex vert
			//	#pragma fragment frag 
			//	//include
			//	#include "UnityCG.cginc"
			//	//#include "Lighting.cginc"
			//	#include "../Include/OutLine_Include.cginc"
			//	ENDCG 
			//}
                  
        Pass 
		{  
			Tags { "LightMode"="ForwardBase" }  

            Cull Back

            CGPROGRAM 
			//define
			#define SKINTEX
			#define SKINTEX8
			#define RIMLIGHT
			#define SHLIGHTON
			#define VERTEXLIGHTON
			//head
			#include "../Include/CommonHead_Include.cginc"
			#include "../Include/SkinBlend_Include.cginc"
			//vertex&fragment
			#pragma vertex vert
            #pragma fragment frag 

			fixed4 _Color;				
			//custom frag fun
			fixed4 BasicColor(in v2f i, inout fixed4 mask)
			{
				fixed4 c = BlendColor(i);
				c.rgb *= _Color.rgb;
				c.a = 1;
				return c;
			}
			//include
            #include "../Include/CommonBasic_Include.cginc"
            ENDCG
		}
			
	}   
}  