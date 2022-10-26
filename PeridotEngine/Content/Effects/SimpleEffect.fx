#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

#include "Macros.fxh"

DECLARE_TEXTURE(Texture, 0);

matrix WorldViewProjection;
float4 MixColor;
float2 TexturePosition;
float2 TextureSize;

struct VertexInNone
{
	float4 Position : POSITION0;
};

struct PixelInNone
{
	float4 Position : SV_POSITION;
};

PixelInNone VS_None(in VertexInNone input)
{
    PixelInNone output = (PixelInNone) 0;

	output.Position = mul(input.Position, WorldViewProjection);

	return output;
}

float4 PS_None(PixelInNone input) : COLOR
{
	return MixColor;
}

struct VertexInColor
{
    float4 Position : POSITION0;
    float4 Color : COLOR0;
};

struct PixelInColor
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
};

PixelInColor VS_Color(in VertexInColor input)
{
    PixelInColor output = (PixelInColor) 0;

    output.Position = mul(input.Position, WorldViewProjection);
    output.Color = input.Color;

    return output;
}

float4 PS_Color(PixelInColor input) : COLOR
{
    return input.Color * MixColor;
}

struct VertexInTexture
{
    float4 Position : POSITION0;
    float2 TexCoord : TEXCOORD0;
};

struct PixelInTexture
{
    float4 Position : SV_POSITION;
    float2 TexCoord : TEXCOORD0;
};

PixelInTexture VS_Texture(in VertexInTexture input)
{
    PixelInTexture output = (PixelInTexture) 0;

    output.Position = mul(input.Position, WorldViewProjection);
    output.TexCoord = input.TexCoord;

    return output;
}

float4 PS_Texture(PixelInTexture input) : COLOR
{
    float2 texCoord = TexturePosition + input.TexCoord * TextureSize;
    return SAMPLE_TEXTURE(Texture, texCoord) * MixColor;
}

// appearance "none" (just solid white)
technique
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL VS_None();
		PixelShader = compile PS_SHADERMODEL PS_None();
	}
};

technique
{
	pass P0
	{
        VertexShader = compile VS_SHADERMODEL VS_Color();
        PixelShader = compile PS_SHADERMODEL PS_Color();
    }
};

technique
{
	pass P0
	{
        VertexShader = compile VS_SHADERMODEL VS_Texture();
        PixelShader = compile PS_SHADERMODEL PS_Texture();
    }
}