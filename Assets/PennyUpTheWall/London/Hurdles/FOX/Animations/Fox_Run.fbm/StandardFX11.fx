/*********************************************************************NVMH3****
$Revision$

Copyright NVIDIA Corporation 2007
TO THE MAXIMUM EXTENT PERMITTED BY APPLICABLE LAW, THIS SOFTWARE IS PROVIDED
*AS IS* AND NVIDIA AND ITS SUPPLIERS DISCLAIM ALL WARRANTIES, EITHER EXPRESS
OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, IMPLIED WARRANTIES OF MERCHANTABILITY
AND FITNESS FOR A PARTICULAR PURPOSE.  IN NO EVENT SHALL NVIDIA OR ITS SUPPLIERS
BE LIABLE FOR ANY SPECIAL, INCIDENTAL, INDIRECT, OR CONSEQUENTIAL DAMAGES
WHATSOEVER (INCLUDING, WITHOUT LIMITATION, DAMAGES FOR LOSS OF BUSINESS PROFITS,
BUSINESS INTERRUPTION, LOSS OF BUSINESS INFORMATION, OR ANY OTHER PECUNIARY
LOSS) ARISING OUT OF THE USE OF OR INABILITY TO USE THIS SOFTWARE, EVEN IF
NVIDIA HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES.


To learn more about shading, shaders, and to bounce ideas off other shader
    authors and users, visit the NVIDIA Shader Library Forums at:

    http://developer.nvidia.com/forums/

******************************************************************************/
string ParamID = "0x003";

float Script : STANDARDSGLOBAL <
    string UIWidget = "none";
    string ScriptClass = "object";
    string ScriptOrder = "standard";
    string ScriptOutput = "color";
    string Script = "Technique=Main;";
> = 0.8;

//// UN-TWEAKABLES - AUTOMATICALLY-TRACKED TRANSFORMS ////////////////

float4x4 WorldITXf : WorldInverseTranspose < string UIWidget="None"; >;
float4x4 WvpXf : WorldViewProjection < string UIWidget="None"; >;
float4x4 WorldXf : World < string UIWidget="None"; >;
float4x4 ViewIXf : ViewInverse < string UIWidget="None"; >;

#ifdef _MAX_
int texcoord1 : Texcoord
<
	int Texcoord = 1;
	int MapChannel = 0;
	string UIWidget = "None";
>;

int texcoord2 : Texcoord
<
	int Texcoord = 2;
	int MapChannel = -2;
	string UIWidget = "None";
>;

int texcoord3 : Texcoord
<
	int Texcoord = 3;
	int MapChannel = -1;
	string UIWidget = "None";
>;
#endif

//// TWEAKABLE PARAMETERS ////////////////////

/// Point Lamp 0 ////////////
float3 Lamp0Pos : POSITION <
    string Object = "PointLight0";
    string UIName =  "Light Position";
    string Space = "World";
	int refID = 0;
> = {-0.5f,2.0f,1.25f};
#ifdef _MAX_
float3 Lamp0Color : LIGHTCOLOR
<
	int LightRef = 0;
	string UIWidget = "None";
> = float3(1.0f, 1.0f, 1.0f);
#else
float3 Lamp0Color : Specular <
    string UIName =  "Lamp 0";
    string Object = "Pointlight0";
    string UIWidget = "Color";
> = {1.0f,1.0f,1.0f};
#endif

float4 k_a  <
	string UIName = "Ambient";
	string UIWidget = "Color";
> = float4( 0.0f, 0.0f, 0.0f, 1.0f );    // ambient
	
float4 k_d  <
	string UIName = "Diffuse";
	string UIWidget = "Color";
> = float4( 0.47f, 0.47f, 0.47f, 1.0f );    // diffuse
	
float4 k_s  <
	string UIName = "Specular";
	string UIWidget = "Color";
> = float4( 1.0f, 1.0f, 1.0f, 1.0f );    // diffuse    // specular

int n<
	string UIName = "Specular Power";
	string UIWidget = "slider";
	float UIMin = 0.0f;
	float UIMax = 50.0f;	
>  = 15;

bool g_AlphaVertex <
	string UIName = "Vertex Alpha";
> = false;

bool g_AddVertexColor <
	string UIName = "Add Vertex Color";
> =false;

bool g_UseParallax <
	string UIName = "Normal Map Parallax";
> =false;

float g_ParallaxScale <
	string UIName = "Parallax Scale";
	string UIWidget = "slider";
	float UIMin = -0.50f;
	float UIMax = 0.5f;
	float UIStep = 0.01f;
>   = 0.02f;
									
float g_ParallaxBias <
	string UIName = "Parallax Bias";
	string UIWidget = "slider";
	float UIMin = -0.5f;
	float UIMax = 0.5f;
	float UIStep = 0.01f;
>   = 0.0f;

bool g_AmbientOccEnable <
	string UIName = "Ambient Occlusion Enable";
> = false;


//////// COLOR & TEXTURE /////////////////////

Texture2D <float4> g_AmbientOccTexture < 
	string UIName = "Ambient Occlusion";
	string ResourceType = "2D";
>;

bool g_TopDiffuseEnable <
	string UIName = "Top Diffuse Color Enable";
> = false;

Texture2D <float4> g_TopTexture : DiffuseMap< 
	string UIName = "Top Diffuse";
	string ResourceType = "2D";
	int Texcoord = 0;
	int MapChannel = 1;
>;

bool g_BottomDiffuseEnable <
	string UIName = "Bottom Diffuse Color Enable";
	string UIWidget = "checkbox";
> = false;

Texture2D <float4> g_BottomTexture < 
	string UIName = "Bottom Diffuse";
	string ResourceType = "2D";
	int Texcoord = 4;
	int MapChannel = 1;
>;

bool g_SpecularEnable <
	string UIName = "Specular Level Enable";
> = false;

Texture2D <float4> g_SpecularTexture < 
	string UIName = "Specular Level";
	string ResourceType = "2D";
>;

bool g_NormalEnable <
	string UIName = "Normal Enable";
> = false;

bool g_FlipGreen <
	string UIName = "Flip Green in Normal Map";
> = false;

// Use MikkT way to interpret the tangents and bitangents.
// This parameter is controlled by 3ds Max according to the "Tangents and Bitangents" preference.
bool useMikkT
<
	string UIName = "Use Mikk-T (Set by 3ds Max)";
> = true;

// Re-calculate the bitangent on each pixel
// This parameter is controlled by 3ds Max according to the "Tangents and Bitangents" preference.
bool calculateBitangentPerPixel
<
	string UIName = "Calculate Bitangent per Pixel (Set by 3ds Max)";
> = true;

// Orthogonalize tangent and bitangent on each pixel, otherwise use the interpolated values.
// This parameter is controlled by 3ds Max according to the "Tangents and Bitangents" preference.
bool orthogonalizeTangentBitangentPerPixel
<
	string UIName = "Orthogonalize per Pixel (Set by 3ds Max)";
> = false;

Texture2D <float4> g_NormalTexture < 
	string UIName = "Normal";
	string ResourceType = "2D";
>;

float g_BumpScale <
	string UIName = "Bump Amount";
	string UIWidget = "slider";
	float UIMin = 0.0f;
	float UIMax = 10.0f;
	float UIStep = 0.01f;
>   = 1.0f;

bool g_ReflectionEnable <
	string UIName = "Reflection Enable";
> = false;

TextureCube g_ReflectionTexture < 
	string UIName = "Reflection";
	string ResourceType = "CUBE";
>;
float g_ReflectAmount <
	string UIName = "Reflection Amount";
	float UIMin = 0.0f;
	float UIMax = 1.0f;
	float UIStep = 0.01f;
	string UIWidget = "slider";
>   = 1.0f;


SamplerState g_AmbientOccSampler
{
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Wrap;
    AddressV = Wrap;
};


SamplerState g_TopSampler
{
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Wrap;
    AddressV = Wrap;
	
};
	
SamplerState g_BottomSampler
{
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Wrap;
    AddressV = Wrap;
	
};

SamplerState g_SpecularSampler
{
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Wrap;
    AddressV = Wrap;
	
};

SamplerState g_NormalSampler
{
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Wrap;
    AddressV = Wrap;
	
};

SamplerState g_ReflectionSampler
{
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Clamp;
    AddressV = Clamp;	
	AddressW = Clamp;	
};

/* data from application vertex buffer */
struct appdata {
	float4 Position		: POSITION;
	float3 Normal		: NORMAL;
	float3 Tangent		: TANGENT;
	float3 Binormal		: BINORMAL;
	float2 UV0		: TEXCOORD0;	
	float3 Colour		: TEXCOORD1;
	float3 Alpha		: TEXCOORD2;
	float3 Illum		: TEXCOORD3;
	float3 UV1		: TEXCOORD4;
	float3 UV2		: TEXCOORD5;
	float3 UV3		: TEXCOORD6;
	float3 UV4		: TEXCOORD7;
};

/* data passed from vertex shader to pixel shader */
struct vertexOutput {
    float4 HPosition	: SV_Position;
    float4 UV0		: TEXCOORD0;
    // The following values are passed in "World" coordinates since
    //   it tends to be the most flexible and easy for handling
    //   reflections, sky lighting, and other "global" effects.
    float3 LightVec	: TEXCOORD1;
    float3 WorldNormal	: TEXCOORD2;
    float3 WorldTangent	: TEXCOORD3;
    float3 WorldBinormal : TEXCOORD4;
    float3 WorldView	: TEXCOORD5;
	float4 UV1		: TEXCOORD6;
	float4 UV2		: TEXCOORD7;
	float4 wPos		: TEXCOORD8;
};
 
///////// VERTEX SHADING /////////////////////

/*********** Generic Vertex Shader ******/

vertexOutput std_VS(appdata IN) {
    vertexOutput OUT = (vertexOutput)0;
    OUT.WorldNormal = mul(IN.Normal,WorldITXf).xyz;
    OUT.WorldTangent = mul(IN.Tangent,WorldITXf).xyz;
    OUT.WorldBinormal = mul(IN.Binormal,WorldITXf).xyz;
    // Mikkt support
    // To align with other applications, normalize normal/tangent/bitangent after transform to world space
    if (useMikkT)
    {
        OUT.WorldNormal = normalize(OUT.WorldNormal);
        OUT.WorldTangent = normalize(OUT.WorldTangent);
        OUT.WorldBinormal = normalize(OUT.WorldBinormal);
    }
    float4 Po = float4(IN.Position.xyz,1);
    float3 Pw = mul(Po,WorldXf).xyz;
    OUT.LightVec = (Lamp0Pos - Pw);
    OUT.WorldView = normalize(ViewIXf[3].xyz - Pw);
    OUT.HPosition = mul(Po,WvpXf);
	OUT.wPos = mul(IN.Position, WorldXf);
	
// UV bindings
// Encode the color data
 	float4 colour;
   	colour.rgb = IN.Colour * IN.Illum;
   	colour.a = IN.Alpha.x;
   	OUT.UV0.z = colour.r;
   	OUT.UV0.a = colour.g;
  	OUT.UV1.z = colour.b;
   	OUT.UV1.a = colour.a;

// Pass through the UVs
	OUT.UV0.xy = IN.UV0.xy;
   	OUT.UV1.xy = IN.UV1.xy;
   	OUT.UV2.xyz = IN.UV2.xyz;
// 	OUT.UV3 = OUT.UV3;
// 	OUT.UV4 = OUT.UV4;
    return OUT;
}

///////// PIXEL SHADING //////////////////////

// Utility function for phong shading

void phong_shading(vertexOutput IN,
		    float3 LightColor,
		    float3 Nn,
		    float3 Ln,
		    float3 Vn,
		    out float3 DiffuseContrib,
		    out float3 SpecularContrib)
{
	float3 specLevel = float3(1.0,1.0,1.0);
	if(g_SpecularEnable)
		specLevel = g_SpecularTexture.Sample(g_SpecularSampler, IN.UV0.xy).xyz;
		
    float3 Hn = normalize(Vn + Ln);
    float4 litV = lit(dot(Ln,Nn),dot(Hn,Nn),n);
    DiffuseContrib = litV.y * LightColor;
    SpecularContrib = litV.y * litV.z * k_s * LightColor * specLevel;
}

float4 std_PS(vertexOutput IN) : SV_Target {
    float3 diffContrib;
    float3 specContrib;
    float3 Ln = normalize(IN.LightVec);
    float3 Vn = normalize(IN.WorldView);
    float3 Nn = normalize(IN.WorldNormal);
    float3 Tn = normalize(IN.WorldTangent);
    float3 Bn = normalize(IN.WorldBinormal);
	float4 vertColour = float4(IN.UV0.z,IN.UV0.w,IN.UV1.z,IN.UV1.w);	
	float3 BottomCol = k_d.rgb; 
	if(g_NormalEnable)
	{
		float3 bump = 2.0f * (g_NormalTexture.Sample(g_NormalSampler,IN.UV0).rgb - 0.5f);

		// Flip green value because by default green means -y in the normal map generated by 3ds Max.
		bump.g = -bump.g;

		if (g_FlipGreen)
		{
			bump.g = -bump.g;
		}

		// Mikkt support
		if (useMikkT)
		{
			// To align with other applications, use the UNNORMALIZED interpolated normal/tangent/bitangent.
			Nn = IN.WorldNormal;
			Tn = IN.WorldTangent;
			Bn = -IN.WorldBinormal;
			if (calculateBitangentPerPixel)
			{
				// Calculate bitangent with unnormalized Normal and Tangent
				float3 bitangent = cross(Nn, Tn);
				Bn = sign(dot(bitangent, Bn)) * bitangent;
			}
		}
		else
		{
			if (orthogonalizeTangentBitangentPerPixel)
			{
				float3 bitangent = normalize(cross(Nn, Tn));
				Tn = normalize(cross(bitangent, Nn));
				// Bitangent need to be flipped if the map face is flipped. We don't have map face handedness in shader so make
				// the calculated bitangent point in the same direction as the interpolated bitangent which has considered the flip.
				Bn = sign(dot(bitangent, Bn)) * bitangent;
			}
		}

		Nn = g_BumpScale * (bump.x * Tn + bump.y * Bn) + bump.z * Nn;
	}
	Nn = normalize(Nn);

	if(g_UseParallax && g_NormalEnable)
	{
		float height = g_NormalTexture.Sample(g_NormalSampler, IN.UV0).a;
		float Altitude = height + g_ParallaxBias; 
		float3 normalUV  = Altitude * g_ParallaxScale*Vn;
		normalUV +=  IN.UV0;		
		IN.UV0.xyz = normalUV;
	}	

	phong_shading(IN,Lamp0Color,Nn,Ln,Vn,diffContrib,specContrib);

    float4 diffuseColor = k_d;
	float3 ambientColor = k_a;
	if(g_TopDiffuseEnable)
		diffuseColor = g_TopTexture.Sample(g_TopSampler,IN.UV0.xy);
		
	if(g_AmbientOccEnable)
		ambientColor *= g_AmbientOccTexture.Sample(g_AmbientOccSampler, IN.UV0.xy);
		
	if(g_BottomDiffuseEnable)
		BottomCol = g_BottomTexture.Sample(g_BottomSampler, IN.UV1.xy);		

// Perform the alpha blends ops		
	float alpha = diffuseColor.a;
	if(g_AlphaVertex)
		alpha = vertColour.a;
		
	diffuseColor.rgb = diffuseColor.rgb * alpha;
	diffuseColor.rgb += BottomCol * (1.0f - alpha);
// add in the vert colour	
	if(g_AddVertexColor)
		diffuseColor.rgb *= vertColour.rgb;		
#ifndef _MAX_		
	float3 result =  specContrib+(diffuseColor*(diffContrib.rgb+ambientColor ));
#else	
	float3 result = specContrib + (diffContrib * diffuseColor) + ambientColor;
#endif	
	if(g_ReflectionEnable)
	{
	#ifdef _MAX_	
		float3 R = reflect(Vn,Nn);
		R.z = -R.z;
		float3 reflColor = g_ReflectAmount * g_ReflectionTexture.Sample(g_ReflectionSampler,R.xzy).rgb;
	#else
		float3 R = reflect(Vn,Nn);
		float3 reflColor = g_ReflectAmount * g_ReflectionTexture.Sample(g_ReflectionSampler,R.xyz).rgb;
	#endif
    	result *= diffuseColor+reflColor;
		
	}
    return float4(result,1);
}

///// TECHNIQUES /////////////////////////////
fxgroup dx11
{
technique11 Main_11 <
	string Script = "Pass=p0;";
> {
	pass p0 <
	string Script = "Draw=geometry;";
    > 
    {
        SetVertexShader(CompileShader(vs_5_0,std_VS()));
        SetGeometryShader( NULL );
		SetPixelShader(CompileShader(ps_5_0,std_PS()));
    }
}
}
fxgroup dx10
{
technique10 Main_10 <
	string Script = "Pass=p0;";
> {
	pass p0 <
	string Script = "Draw=geometry;";
    > 
    {
        SetVertexShader(CompileShader(vs_4_0,std_VS()));
        SetGeometryShader( NULL );
		SetPixelShader(CompileShader(ps_4_0,std_PS()));
    }
}
}
/////////////////////////////////////// eof //
