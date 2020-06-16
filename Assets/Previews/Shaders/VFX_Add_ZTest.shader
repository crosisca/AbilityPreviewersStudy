// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "VFX/VFX_Add_ZTest"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_Intensity("Intensity", Range( 0 , 1)) = 1
		[HDR]_Color("Color", Color) = (1,1,1,1)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		ZWrite Off
		ZTest Always
		Blend One One , SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow nofog 
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
		};

		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float4 _Color;
		uniform float _Intensity;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 tex2DNode3 = tex2D( _Albedo, uv_Albedo );
			o.Emission = ( i.vertexColor * tex2DNode3 * _Color * tex2DNode3.a * _Color.a * _Intensity * i.vertexColor.a ).rgb;
			o.Alpha = saturate( ( i.vertexColor.a * tex2DNode3.a * _Color.a * _Intensity ) );
		}

		ENDCG
	}
}
/*ASEBEGIN
Version=17000
7;1;1906;1011;972;492;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;6;-476,275;Float;False;Property;_Intensity;Intensity;2;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-538,-204;Float;True;Property;_Albedo;Albedo;1;0;Create;True;0;0;False;0;None;d8b98c049fc828e48ae240000161c058;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;9;-415,-423;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;4;-467,65;Float;False;Property;_Color;Color;3;1;[HDR];Create;True;0;0;False;0;1,1,1,1;1,1,1,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;130,39;Float;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;13;308,44.5;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;119,-227;Float;False;7;7;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;2;490,-155;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;VFX/VFX_Add_ZTest;False;False;False;False;False;False;False;False;False;True;False;False;False;False;True;False;False;False;False;False;False;Off;2;False;-1;7;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;4;1;False;-1;1;False;-1;2;5;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;11;0;9;4
WireConnection;11;1;3;4
WireConnection;11;2;4;4
WireConnection;11;3;6;0
WireConnection;13;0;11;0
WireConnection;10;0;9;0
WireConnection;10;1;3;0
WireConnection;10;2;4;0
WireConnection;10;3;3;4
WireConnection;10;4;4;4
WireConnection;10;5;6;0
WireConnection;10;6;9;4
WireConnection;2;2;10;0
WireConnection;2;9;13;0
ASEEND*/
//CHKSM=6CD270FCA19CCF01E1FF185A111E8EAA1DBDB192