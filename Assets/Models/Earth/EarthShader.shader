Shader "RevolutionEarth/EarthShader"
{
	Properties 
	{
_MainTex("_MainTex", 2D) = "black" {}
_Normals("_Normals", 2D) = "black" {}
_Lights("_Lights", 2D) = "black" {}
_AtmosNear("_AtmosNear", Color) = (0.5215687,0.8235294,0.8941177,1)
_AtmosFar("_AtmosFar", Color) = (0.2588235,0.3176471,0.6352941,1)
_Clouds("_Clouds", 2D) = "White" {}
_Roughness("_Roughness", Float) = 0.08148026
_TimeScale("_TimeScale", Float) = 0.01
_AtmosFalloff("_AtmosFalloff", Float) = 3.03
_Mask("_Mask", 2D) = "black" {}
_ColorSpec("_ColorSpec", Color) = (0.8823529,0.9254902,0.9686275,1)
_LightScale("_LightScale", Float) = 3.090286
_CloudHeight("_CloudHeight", Range(0,-0.1) ) = 0
_AnsioCloudPower("_AnsioCloudPower", Range(0,0.1) ) = 0
_EdgeLength ("Edge length", Range(3,50)) = 10
_Smoothness ("Smoothness", Range(0,1)) = 0.5

	}
	
	SubShader 
	{
		Tags
		{
"Queue"="Geometry"
"IgnoreProjector"="False"
"RenderType"="Opaque"

		}

		
Cull Back
ZWrite On
ZTest LEqual
ColorMask RGBA
Fog{
}


		CGPROGRAM
#pragma surface surf BlinnPhongEditor  vertex:vert
#pragma target 3.0
#include "Tessellation.cginc"

struct appdata {
	float4 vertex : POSITION;
	float4 tangent : TANGENT;
	float3 normal : NORMAL;
	float2 texcoord : TEXCOORD0;
	float2 texcoord1 : TEXCOORD1;
};

float _EdgeLength;
float _Smoothness;

float4 tessEdge (appdata v0, appdata v1, appdata v2)
{
	return UnityEdgeLengthBasedTessCull (v0.vertex, v1.vertex, v2.vertex, _EdgeLength, 0.0);
}

void disp (inout appdata v)
{
	// do nothing
}

sampler2D _MainTex;
sampler2D _Normals;
sampler2D _Lights;
float4 _AtmosNear;
float4 _AtmosFar;
sampler2D _Clouds;
float _Roughness;
float _TimeScale;
float _AtmosFalloff;
sampler2D _Mask;
float4 _ColorSpec;
float _LightScale;
float _CloudHeight;
float _AnsioCloudPower;

			struct EditorSurfaceOutput {
				half3 Albedo;
				half3 Normal;
				half3 Emission;
				half3 Gloss;
				half Specular;
				half Alpha;
				half4 Custom;
			};
			
			inline half4 LightingBlinnPhongEditor_PrePass (EditorSurfaceOutput s, half4 light)
			{
half3 spec = light.a * s.Gloss;
half4 c;
c.rgb = (s.Albedo * light.rgb + light.rgb * spec);
c.a = s.Alpha;
return c;

			}

			inline half4 LightingBlinnPhongEditor (EditorSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
			{
				half3 h = normalize (lightDir + viewDir);
				
				half diff = max (0, dot ( lightDir, s.Normal ));
				
				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, s.Specular*128.0);
				
				half4 res;
				res.rgb = _LightColor0.rgb * diff;
				res.w = spec * Luminance (_LightColor0.rgb);
				res *= atten * 2.0;

				return LightingBlinnPhongEditor_PrePass( s, res );
			}

			inline half4 LightingBlinnPhongEditor_DirLightmap (EditorSurfaceOutput s, fixed4 color, fixed4 scale, half3 viewDir, bool surfFuncWritesNormal, out half3 specColor)
			{
				UNITY_DIRBASIS
				half3 scalePerBasisVector;
				
				half3 lm = DirLightmapDiffuse (unity_DirBasis, color, scale, s.Normal, surfFuncWritesNormal, scalePerBasisVector);
				
				half3 lightDir = normalize (scalePerBasisVector.x * unity_DirBasis[0] + scalePerBasisVector.y * unity_DirBasis[1] + scalePerBasisVector.z * unity_DirBasis[2]);
				half3 h = normalize (lightDir + viewDir);
			
				float nh = max (0, dot (s.Normal, h));
				float spec = pow (nh, s.Specular * 128.0);
				
				// specColor used outside in the forward path, compiled out in prepass
				specColor = lm * _SpecColor.rgb * s.Gloss * spec;
				
				// spec from the alpha component is used to calculate specular
				// in the Lighting*_Prepass function, it's not used in forward
				return half4(lm, spec);
			}
			
			struct Input {
				float3 viewDir;
float2 uv_MainTex;
float2 uv_Clouds;
float2 uv_Normals;
float2 uv_Lights;
float2 uv_Mask;

			};

			void vert (inout appdata_full v, out Input o) {
float4 VertexOutputMaster0_0_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_1_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_2_NoInput = float4(0,0,0,0);
float4 VertexOutputMaster0_3_NoInput = float4(0,0,0,0);


			}
			

			void surf (Input IN, inout EditorSurfaceOutput o) {
				o.Normal = float3(0.0,0.0,1.0);
				o.Alpha = 1.0;
				o.Albedo = 0.0;
				o.Emission = 0.0;
				o.Gloss = 0.0;
				o.Specular = 0.0;
				o.Custom = 0.0;
				
float4 Fresnel0_1_NoInput = float4(0,0,1,1);
float4 Fresnel0=(1.0 - dot( normalize( float4( IN.viewDir.x, IN.viewDir.y,IN.viewDir.z,1.0 ).xyz), normalize( Fresnel0_1_NoInput.xyz ) )).xxxx;
float4 Pow0=pow(Fresnel0,_AtmosFalloff.xxxx);
float4 Saturate0=saturate(Pow0);
float4 Lerp0=lerp(_AtmosNear,_AtmosFar,Saturate0);
float4 Multiply1=Lerp0 * Saturate0;
float4 Sampled2D0=tex2D(_MainTex,IN.uv_MainTex.xy);
float4 Add0=Multiply1 + Sampled2D0;
float4 Multiply2=_Time * _TimeScale.xxxx;
float4 UV_Pan0=float4((IN.uv_Clouds.xyxy).x + Multiply2.y,(IN.uv_Clouds.xyxy).y,(IN.uv_Clouds.xyxy).z,(IN.uv_Clouds.xyxy).w);
float4 ParallaxOffset0_2_NoInput = float4(0,0,0,0);
float4 ParallaxOffset0= ParallaxOffset( _CloudHeight.xxxx.x, ParallaxOffset0_2_NoInput.x, float4( IN.viewDir.x, IN.viewDir.y,IN.viewDir.z,1.0 ).xyz).xyxy;
float4 Add1=UV_Pan0 + ParallaxOffset0;
float4 Tex2D0=tex2D(_Clouds,Add1.xy);
float4 Lerp1=lerp(Add0,Tex2D0,Tex2D0);
float4 Sampled2D1=tex2D(_Normals,IN.uv_Normals.xy);
float4 UnpackNormal0=float4(UnpackNormal(Sampled2D1).xyz, 1.0);
float4 Normalize0=normalize(float4( IN.viewDir.x, IN.viewDir.y,IN.viewDir.z,1.0 ));
float4 Negative0= -Normalize0; 
 float4 Invert1= float4(1.0, 1.0, 1.0, 1.0) - _AnsioCloudPower.xxxx;
float4 Lerp4=lerp(Negative0,float4( 0,0,1,0),Invert1);
float4 Lerp3=lerp(UnpackNormal0,Lerp4,Tex2D0);
float4 Sampled2D2=tex2D(_Lights,IN.uv_Lights.xy);
float4 Multiply0=Sampled2D2 * _LightScale.xxxx;
float4 Lerp2_1_NoInput = float4(0,0,0,0);
float4 Lerp2=lerp(Multiply0,Lerp2_1_NoInput,Tex2D0);
float4 Sampled2D3=tex2D(_Mask,IN.uv_Mask.xy);
float4 Invert0= float4(1.0, 1.0, 1.0, 1.0) - Tex2D0;
float4 Multiply4=Sampled2D3 * Invert0;
float4 Multiply3=Multiply4 * _ColorSpec;
float4 Master0_5_NoInput = float4(1,1,1,1);
float4 Master0_7_NoInput = float4(0,0,0,0);
float4 Master0_6_NoInput = float4(1,1,1,1);
o.Albedo = Lerp1;
o.Normal = Lerp3;
o.Emission = Lerp2;
o.Specular = Multiply3;
o.Gloss = _Roughness.xxxx;

				o.Normal = normalize(o.Normal);
			}
		ENDCG
	}
	Fallback "Diffuse"
}