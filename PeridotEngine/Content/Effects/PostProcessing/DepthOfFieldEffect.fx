#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0
	#define PS_SHADERMODEL ps_4_0
#endif

#include "../Macros.fxh"

DECLARE_TEXTURE(ColorTexture, 0);
DECLARE_TEXTURE(DepthTexture, 1);

float NearPlane;
float FarPlane;

float depthToAbsolute(float depth) {
	float rel = (2.0f * NearPlane) / (FarPlane + NearPlane - depth * (FarPlane - NearPlane));
    float abs = (FarPlane - NearPlane) * rel + NearPlane;
	return abs;
}

struct VertexShaderInput
{
	float4 Position : POSITION0;
	float2 TexCoord : TEXCOORD0;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float2 TexCoord : TEXCOORD0;
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
	VertexShaderOutput output = (VertexShaderOutput)0;

	output.Position = input.Position;
	output.TexCoord = input.TexCoord;

	return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float depth = depthToAbsolute(SAMPLE_TEXTURE(DepthTexture, input.TexCoord).r);

	float kernelSize = min(0.02, depth / 2000);
	const int sampleCount = 100;
	float stepSize = kernelSize / sqrt(sampleCount);

	float3 result = float3(0, 0, 0);
	int i = 0;
	[loop]
	for(float x = -kernelSize / 2; x <= kernelSize / 2; x += stepSize) {
		[loop]
		for(float y = -kernelSize / 2; y <= kernelSize / 2; y += stepSize) {
			float sampleDepth = depthToAbsolute(SAMPLE_TEXTURE(DepthTexture, input.TexCoord).r);
			if(sampleDepth < depth - 1.0f) continue;

			float4 sampleColor = SAMPLE_TEXTURE(ColorTexture, input.TexCoord + float2(x, y));
			result += sampleColor.xyz;
			i++;
		}
	}

	result /= i;
	return float4(result, 1);
}

technique BasicColorDrawing
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};