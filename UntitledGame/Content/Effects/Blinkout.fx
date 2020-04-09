﻿#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

int	TextureHeight;
sampler TextureSampler : register(s0);

float4 PixelShaderFunction(float4 position : SV_Position, float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
{
	float newAlpha = 0;
	float4 tex = tex2D(TextureSampler, texCoord);
	if (tex.a != 0)
	{
		if (((int) (texCoord.y * TextureHeight) % 2) == 0)
		{
			tex.a = newAlpha / 255;
		}
	}
	return tex;
}


technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
	}
}