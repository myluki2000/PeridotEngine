#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0
	#define PS_SHADERMODEL ps_4_0
#endif

#include "Macros.fxh"

DECLARE_TEXTURE(Texture, 0);
DECLARE_TEXTURE(ShadowMap, 1);

matrix WorldViewProjection;

float4 MixColor;

float2 TexturePosition;
float2 TextureSize;

bool EnableShadows;
matrix LightWorldViewProjection;

float CalculateShadowPresence(float4 positionLightSpace)
{
    positionLightSpace /= positionLightSpace.w;
    
    float2 uv = float2(
        map(positionLightSpace.x, -1, 1, 0, 1),
        map(-positionLightSpace.y, -1, 1, 0, 1)
    );
    
    float shadowCoeff = 0;
    for (float x = -1.5; x < 1.5; x++)
    {
        for (float y = -1.5; y < 1.5; y++)
        {
            float mapDepth = SAMPLE_TEXTURE(ShadowMap, uv + float2(x, y) * 0.0015).r;
            bool inLight = (mapDepth > (positionLightSpace.z - (1 - positionLightSpace.z) * 0.02))
            || uv.x > 1 || uv.x < 0 || uv.y > 1 || uv.y < 0;
            
            shadowCoeff += inLight;
        }
    }
    
    shadowCoeff /= 16;
    
    // return 0.4 if in shadow or 1 if in light
    return 0.4 + 0.6 * shadowCoeff;
    
    // alternate code without multisampling
    
    /*positionLightSpace /= positionLightSpace.w;
    
    float2 uv = float2(
        map(positionLightSpace.x, -1, 1, 0, 1),
        map(-positionLightSpace.y, -1, 1, 0, 1)
    );
    
    float mapDepth = SAMPLE_TEXTURE(ShadowMap, uv).r;
    
    bool inLight = (mapDepth > (positionLightSpace.z - (1 - positionLightSpace.z) * 0.01))
            || uv.x > 1 || uv.x < 0 || uv.y > 1 || uv.y < 0;
    
    // return 0.4 if in shadow or 1 if in light
    return 0.4 + 0.6 * inLight;*/
}

struct VertexInNone
{
	float4 Position : POSITION0;
};

struct PixelInNone
{
	float4 Position : SV_POSITION;
    float4 PositionLightSpace : TEXCOORD0;
};

PixelInNone VS_None(in VertexInNone input)
{
    PixelInNone output = (PixelInNone) 0;

	output.Position = mul(input.Position, WorldViewProjection);
    output.PositionLightSpace = mul(input.Position, LightWorldViewProjection);

	return output;
}

float4 PS_None(PixelInNone input) : COLOR
{
    return MixColor * CalculateShadowPresence(input.PositionLightSpace);

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
    float4 PositionLightSpace : TEXCOORD0;
};

PixelInColor VS_Color(in VertexInColor input)
{
    PixelInColor output = (PixelInColor) 0;

    output.Position = mul(input.Position, WorldViewProjection);
    output.Color = input.Color;
    output.PositionLightSpace = mul(input.Position, LightWorldViewProjection);

    return output;
}

float4 PS_Color(PixelInColor input) : COLOR
{
    return input.Color * MixColor * CalculateShadowPresence(input.PositionLightSpace);
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
    float4 PositionLightSpace : TEXCOORD1;
};

PixelInTexture VS_Texture(in VertexInTexture input)
{
    PixelInTexture output = (PixelInTexture) 0;

    output.Position = mul(input.Position, WorldViewProjection);
    output.TexCoord = input.TexCoord;
    output.PositionLightSpace = mul(input.Position, LightWorldViewProjection);

    return output;
}

float4 PS_Texture(PixelInTexture input) : COLOR
{
    float2 texCoord = TexturePosition + input.TexCoord * TextureSize;
    return SAMPLE_TEXTURE(Texture, texCoord) * MixColor * CalculateShadowPresence(input.PositionLightSpace);
}

struct VertexInColorTexture
{
    float4 Position : POSITION0;
    float4 Color : COLOR0;
    float2 TexCoord : TEXCOORD0;
};

struct PixelInColorTexture
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
    float2 TexCoord : TEXCOORD0;
    float4 PositionLightSpace : TEXCOORD1;
};

PixelInColorTexture VS_ColorTexture(in VertexInColorTexture input)
{
    PixelInColorTexture output = (PixelInColorTexture) 0;

    output.Position = mul(input.Position, WorldViewProjection);
    output.TexCoord = input.TexCoord;
    output.Color = input.Color;
    output.PositionLightSpace = mul(input.Position, LightWorldViewProjection);

    return output;
}

float4 PS_ColorTexture(PixelInColorTexture input) : COLOR
{
    float2 texCoord = TexturePosition + input.TexCoord * TextureSize;
    return SAMPLE_TEXTURE(Texture, texCoord) * input.Color * MixColor * CalculateShadowPresence(input.PositionLightSpace);
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

// vertex color
technique
{
	pass P0
	{
        VertexShader = compile VS_SHADERMODEL VS_Color();
        PixelShader = compile PS_SHADERMODEL PS_Color();
    }
};

// textured
technique
{
    pass P0
    {
        VertexShader = compile VS_SHADERMODEL VS_Texture();
        PixelShader = compile PS_SHADERMODEL PS_Texture();
    }
};

// vertex color + textured
technique
{
    pass P0
    {
        VertexShader = compile VS_SHADERMODEL VS_ColorTexture();
        PixelShader = compile PS_SHADERMODEL PS_ColorTexture();
    }
};