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

float AspectRatio;

float KernelSize;

#define SAMPLE_COUNT 50


struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float2 TexCoord : TEXCOORD0;
};

float4 BlurHorizontal(VertexShaderOutput input) : COLOR {
	float stepSize = KernelSize / SAMPLE_COUNT;

	float3 result = float3(0, 0, 0);

	[unroll(SAMPLE_COUNT)]
	for(float x = -KernelSize / 2; x <= KernelSize / 2; x += stepSize) {
		float2 sampleTexCoord = input.TexCoord + float2(x, 0);
		float4 sampleColor = SAMPLE_TEXTURE(ColorTexture, sampleTexCoord);
		result += sampleColor.xyz;
	}

	result /= SAMPLE_COUNT;
	return float4(result, 1);
}

float4 BlurVertical(VertexShaderOutput input) : COLOR {
	float stepSize = KernelSize / SAMPLE_COUNT;

	float3 result = float3(0, 0, 0);

	[unroll(SAMPLE_COUNT)]
	for(float x = -KernelSize / 2; x <= KernelSize / 2; x += stepSize) {
		float2 sampleTexCoord = input.TexCoord + float2(0, x * AspectRatio);
		float4 sampleColor = SAMPLE_TEXTURE(ColorTexture, sampleTexCoord);
		result += sampleColor.xyz;
	}

	result /= SAMPLE_COUNT;
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