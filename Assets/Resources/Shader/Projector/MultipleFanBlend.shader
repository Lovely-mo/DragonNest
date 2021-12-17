﻿Shader "Custom/Projector/MultipleFanBlend" 
{
	Properties 
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_OutlineColor ("Outline Color", Color) = (1,1,1,1)
		_Arg("x:color transition y:color scale z:outline width w: outline scale",Vector) = (1.1,1,0.1,1)
		_Vector("x:Number y:Angle z:Rota",Vector)=(3,0.5,0,1)
		_Angle("Angle Control ",Float) = 0
	}
	Category 
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off ZWrite Off Fog{ Mode Off }
	
		SubShader 
		{
			Pass 
			{
		
				CGPROGRAM
				#include "../Include/Projector_Include.cginc"
				#pragma vertex vert
				#pragma fragment frag

				fixed4 _TintColor;
				fixed4 _OutlineColor;
				half4 _Arg;
				half4 _Vector;
				half _Angle;

				fixed4 frag (v2f i) : SV_Target
				{				
					//circle outline
					half circle = saturate((0.25 - ( i.uv.x* i.uv.x+ i.uv.y* i.uv.y))*4);
					//circle mask & fan mask
					//half mask = sign(circle*(normalize(i.uv).y - _Angle));
					//distance to center,_Arg.x make distance >0
					half dis = _Arg.y*(_Arg.x-circle);
					//outline
					half cull = saturate(_Arg.z-circle)*_Arg.w;
					fixed4 color = _TintColor;
					color.a *=dis;


					_Vector.z*=3.1415926/180;
					half2x2 rota=half2x2(cos(_Vector.z),-sin(_Vector.z),sin(_Vector.z),cos(_Vector.z));
					i.uv=mul(rota,i.uv);
					half a=atan2(i.uv.y,i.uv.x)/3.1415926*0.5+0.5;
					fixed mask=step( fmod(a*_Vector.x,1),_Vector.y)*ceil( circle);
					return (color+ cull*_OutlineColor)*mask;
				}
				ENDCG 
			}
		}	
	}
}