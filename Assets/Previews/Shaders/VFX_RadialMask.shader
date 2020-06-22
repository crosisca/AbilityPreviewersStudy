// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "VFX/VFX_RadialMask"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_Alpha("Alpha", 2D) = "white" {}
		_MaskAngle("Mask Angle", Range( 0 , 360)) = 0
		_Intensity("Intensity", Range( 0 , 10)) = 1
		_BorderSmoothness("BorderSmoothness", Range( 0 , 1)) = 0.05
		_ScrollSpeed("ScrollSpeed", Vector) = (0,0,0,0)
		_AlphaScrollSpeed("AlphaScrollSpeed", Vector) = (0,0,0,0)
		_MaskScrollSpeed("Mask Scroll Speed", Vector) = (0,0,0,0)
		[HDR]_Color0("Color 0", Color) = (1,1,1,1)
		_Mask("Mask", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Custom"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform float4 _Color0;
		uniform sampler2D _Albedo;
		uniform float2 _ScrollSpeed;
		uniform float4 _Albedo_ST;
		uniform sampler2D _Alpha;
		uniform float2 _AlphaScrollSpeed;
		uniform float4 _Alpha_ST;
		uniform float _Intensity;
		uniform float _MaskAngle;
		uniform float _BorderSmoothness;
		uniform sampler2D _Mask;
		uniform float2 _MaskScrollSpeed;
		uniform float4 _Mask_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv0_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float2 panner29 = ( 1.0 * _Time.y * _ScrollSpeed + uv0_Albedo);
			o.Emission = ( _Color0 * tex2D( _Albedo, panner29 ) * i.vertexColor ).rgb;
			float2 uv0_Alpha = i.uv_texcoord * _Alpha_ST.xy + _Alpha_ST.zw;
			float2 panner30 = ( 1.0 * _Time.y * _AlphaScrollSpeed + uv0_Alpha);
			float temp_output_10_0 = ( (360.0 + (_MaskAngle - 0.0) * (0.0 - 360.0) / (360.0 - 0.0)) / 720.0 );
			float smoothstepResult19 = smoothstep( temp_output_10_0 , ( temp_output_10_0 - ( _BorderSmoothness / 10.0 ) ) , abs( ( i.uv_texcoord.x - ( 1.0 - 0.5 ) ) ));
			float2 uv0_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float2 panner41 = ( 1.0 * _Time.y * _MaskScrollSpeed + uv0_Mask);
			float4 clampResult33 = clamp( ( ( tex2D( _Alpha, panner30 ) * _Intensity ) * ( 1.0 - smoothstepResult19 ) * _Color0.a * tex2D( _Mask, panner41 ) * i.vertexColor.a ) , float4( 0,0,0,0 ) , float4( 1,0,0,0 ) );
			o.Alpha = clampResult33.r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17000
7;1;1906;1011;1620.403;-104.5601;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;7;-991.0529,458.6483;Float;False;Constant;_Half;Half;3;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;5;-1155.553,538.048;Float;False;Property;_MaskAngle;Mask Angle;3;0;Create;True;0;0;False;0;0;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-771.9321,738.3297;Float;False;Property;_BorderSmoothness;BorderSmoothness;5;0;Create;True;0;0;False;0;0.05;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-685.6324,830.4046;Float;False;Constant;_10;10;5;0;Create;True;0;0;False;0;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-801.4531,657.4484;Float;False;Constant;_720;720;3;0;Create;True;0;0;False;0;720;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;34;-1305.294,373.0388;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;46;-793.4027,504.5601;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;360;False;3;FLOAT;360;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;43;-800.0146,428.7601;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;10;-519.7532,584.7483;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;32;-1617.657,224.9864;Float;False;Property;_AlphaScrollSpeed;AlphaScrollSpeed;7;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleSubtractOpNode;6;-635.4531,337.7484;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;24;-489.4995,701.0112;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;26;-1547.936,57.55308;Float;False;0;2;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.AbsOpNode;8;-398.2532,348.4483;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;23;-351.3979,600.7852;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;40;-775.2426,910.3712;Float;False;0;37;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;30;-1285.427,197.2312;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;39;-886.9636,1055.804;Float;False;Property;_MaskScrollSpeed;Mask Scroll Speed;8;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;17;-534.053,246.7482;Float;False;Property;_Intensity;Intensity;4;0;Create;True;0;0;False;0;1;0.84;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;31;-1734.939,-38.9053;Float;False;Property;_ScrollSpeed;ScrollSpeed;6;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;28;-1658.057,-174.2943;Float;False;0;1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-832.1907,98.42737;Float;True;Property;_Alpha;Alpha;2;0;Create;True;0;0;False;0;None;949caae0f84b9504fbd50e331fd2dd4b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;41;-554.7336,1028.049;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SmoothstepOpNode;19;-199.9768,373.9314;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;35;-594.3814,-396.4835;Float;False;Property;_Color0;Color 0;9;1;[HDR];Create;True;0;0;False;0;1,1,1,1;1,1,1,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-190.853,175.2481;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;37;-132.4004,701.0678;Float;True;Property;_Mask;Mask;10;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;38;-410.494,-11.01441;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;44;-2.40271,337.5601;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;29;-1268.486,-122.1255;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-786.1907,-140.5726;Float;True;Property;_Albedo;Albedo;1;0;Create;True;0;0;False;0;None;949caae0f84b9504fbd50e331fd2dd4b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;156.4018,196.893;Float;True;5;5;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;COLOR;0,0,0,0;False;4;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;33;300.5719,86.601;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-53.55193,-152.9421;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;471.9,-7.800012;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;VFX/VFX_RadialMask;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;7;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Custom;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;46;0;5;0
WireConnection;43;0;7;0
WireConnection;10;0;46;0
WireConnection;10;1;11;0
WireConnection;6;0;34;1
WireConnection;6;1;43;0
WireConnection;24;0;21;0
WireConnection;24;1;25;0
WireConnection;8;0;6;0
WireConnection;23;0;10;0
WireConnection;23;1;24;0
WireConnection;30;0;26;0
WireConnection;30;2;32;0
WireConnection;2;1;30;0
WireConnection;41;0;40;0
WireConnection;41;2;39;0
WireConnection;19;0;8;0
WireConnection;19;1;10;0
WireConnection;19;2;23;0
WireConnection;18;0;2;0
WireConnection;18;1;17;0
WireConnection;37;1;41;0
WireConnection;44;0;19;0
WireConnection;29;0;28;0
WireConnection;29;2;31;0
WireConnection;1;1;29;0
WireConnection;16;0;18;0
WireConnection;16;1;44;0
WireConnection;16;2;35;4
WireConnection;16;3;37;0
WireConnection;16;4;38;4
WireConnection;33;0;16;0
WireConnection;36;0;35;0
WireConnection;36;1;1;0
WireConnection;36;2;38;0
WireConnection;0;2;36;0
WireConnection;0;9;33;0
ASEEND*/
//CHKSM=606C7E6BBD0F448A8984C8BECBAB960D7EB2892F