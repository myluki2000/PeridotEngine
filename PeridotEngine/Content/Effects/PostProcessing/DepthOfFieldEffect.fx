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

float AspectRatio;

#define SAMPLE_COUNT 50

float depthToAbsolute(float depth) {
	float rel = (2.0f * NearPlane) / (FarPlane + NearPlane - depth * (FarPlane - NearPlane));
    float abs = (FarPlane - NearPlane) * rel + NearPlane;
	return abs;
}


struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float2 TexCoord : TEXCOORD0;
};

float4 BlurHorizontal(VertexShaderOutput input) : COLOR {
	float depth = depthToAbsolute(SAMPLE_TEXTURE(DepthTexture, input.TexCoord).r);
	float kernelSize = min(0.02, depth / 2000);
	float stepSize = kernelSize / SAMPLE_COUNT;

	float3 result = float3(0, 0, 0);

	int i = 0;

	[unroll(SAMPLE_COUNT)]
	for(float x = -kernelSize / 2; x <= kernelSize / 2; x += stepSize) {
		float2 sampleTexCoord = input.TexCoord + float2(x, 0);
		float sampleDepth = depthToAbsolute(SAMPLE_TEXTURE(DepthTexture, sampleTexCoord).r);

		float4 sampleColor = SAMPLE_TEXTURE(ColorTexture, sampleTexCoord);
		bool included = sampleDepth > (depth * (0.30 + random(sampleTexCoord) * 0.05));
		result += sampleColor.xyz * included;
		i += included;
	}

	result /= i;
	return float4(result, 1);
}

float4 BlurVertical(VertexShaderOutput input) : COLOR {
	float depth = depthToAbsolute(SAMPLE_TEXTURE(DepthTexture, input.TexCoord).r);
	float kernelSize = min(0.02, depth / 2000);
	float stepSize = kernelSize / SAMPLE_COUNT;

	float3 result = float3(0, 0, 0);

	int i = 0;

	[unroll(SAMPLE_COUNT)]
	for(float x = -kernelSize / 2; x <= kernelSize / 2; x += stepSize) {
		float2 sampleTexCoord = input.TexCoord + float2(0, x);
		float sampleDepth = depthToAbsolute(SAMPLE_TEXTURE(DepthTexture, sampleTexCoord).r);

		float4 sampleColor = SAMPLE_TEXTURE(ColorTexture, sampleTexCoord);
		bool included = sampleDepth > (depth * (0.30 + random(sampleTexCoord) * 0.05));
		result += sampleColor.xyz * included;
		i += included;
	}

	result /= i;
	return float4(result, 1);
}

technique
{
	pass {
		PixelShader = compile PS_SHADERMODEL BlurHorizontal();
	}
};
technique
{
	pass {
		PixelShader = compile PS_SHADERMODEL BlurVertical();
	}
};