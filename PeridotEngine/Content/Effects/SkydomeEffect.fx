#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

#include "Macros.fxh"

matrix World;
matrix ViewProjection;

float FogStart;
float FogEnd;
float4 FogColor;
float3 CameraPosition;

struct VertexShaderInput
{
	float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float2 TexCoord : TEXCOORD0;
	float4 WorldPosition : POSITION1;
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;

	float4 worldPos = mul(input.Position, World);
	output.Position = mul(worldPos, ViewProjection);
	output.TexCoord = input.TexCoord;
	output.WorldPosition = worldPos;

	return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	const float4 baseColor = float4(0.392, 0.584, 0.929, 1);
	const float4 lightColor = float4(116 / 255.0f, 163 / 255.0f, 245 / 255.0f, 1);
	const float4 darkColor = float4(58 / 255.0f, 111 / 255.0f, 204 / 255.0f, 1);

	float4 result = lerp(baseColor, lightColor, saturate(map(input.TexCoord.y, 0.75, 0.9, 0, 1)));
	result = lerp(result, darkColor, saturate(map(input.TexCoord.y, 0.3, 0.1, 0, 1)));

	float dist = distance(CameraPosition, input.WorldPosition);
    float fogBlend = clamp(map(dist, FogStart, FogEnd, 0, 1), 0, 1);

    result = lerp(result, FogColor, fogBlend);

	return result;
}

technique
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};