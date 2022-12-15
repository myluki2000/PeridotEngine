#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0
	#define PS_SHADERMODEL ps_4_0
#endif

#include "Macros.fxh"

matrix World;
matrix ViewProjection;

struct VertexShaderInput
{
	float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float2 TexCoord : TEXCOORD0;
	float2 Depth : TEXCOORD1;
};

struct PixelShaderOutput
{
	float4 Color : COLOR0;
	float Depth : COLOR1;
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;

	float4 worldPos = mul(input.Position, World);
	output.Position = mul(worldPos, ViewProjection);
	output.TexCoord = input.TexCoord;
	output.Depth = output.Position.zw;

	return output;
}

PixelShaderOutput MainPS(VertexShaderOutput input)
{
	const float4 baseColor = float4(0.392, 0.584, 0.929, 1);
	const float4 lightColor = float4(116 / 255.0f, 163 / 255.0f, 245 / 255.0f, 1);
	const float4 darkColor = float4(58 / 255.0f, 111 / 255.0f, 204 / 255.0f, 1);

	float4 result = lerp(baseColor, lightColor, saturate(map(input.TexCoord.y, 0.75, 0.9, 0, 1)));
	result = lerp(result, darkColor, saturate(map(input.TexCoord.y, 0.3, 0.1, 0, 1)));
    
	PixelShaderOutput output;
	output.Color = result;
	output.Depth = input.Depth.x / input.Depth.y;

	return output;
}

technique
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};