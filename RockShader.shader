// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unlit alpha-blended shader.
 // - no lighting
 // - no lightmap support
 // - no per-material color
 
 Shader "Custom/RockShader" {
 Properties {
     _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
     _ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { }
	 _TopC("Top Colour", Color) = (.5,.5,.5,1)
	 _BotC("Bottom Colour", Color) = (.5,.5,.5,1)
	_disp("d",float)= 1.
	_scale("s",float) = 1.
 }
 
 SubShader {
     Tags { "IgnoreProjector"="True" "RenderType"="Opaque"}
     LOD 100
     
     ZWrite On
     Blend SrcAlpha OneMinusSrcAlpha 
     
     Pass { 
         tags{"LightMode" = "ForwardBase"} 
         CGPROGRAM
            
             
             #pragma vertex vert
             #pragma fragment frag
             #pragma multi_compile_fog
             
             #include "UnityCG.cginc"
             #include "AutoLight.cginc"
 
             struct appdata_t {
                 float4 vertex : POSITION;
                 float2 texcoord : TEXCOORD0;
                 float3 normal : NORMAL;
             };
 
             struct v2f {
                 float4 vertex : SV_POSITION;
                 half2 texcoord : TEXCOORD0;
                 float3 cubenormal : TEXCOORD1;
                 UNITY_FOG_COORDS(1)
             };
 
             sampler2D _MainTex;
             samplerCUBE _ToonShade;
             float4 _MainTex_ST;
			 float4 _TopC;
			 float4 _BotC;
			 float _disp;
			 float _scale;
             
             v2f vert (appdata_t v)
             {
                 v2f o;
                 o.vertex = UnityObjectToClipPos(v.vertex);
                 o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                 o.cubenormal = mul (UNITY_MATRIX_MV, float4(v.normal,0));
                 UNITY_TRANSFER_FOG(o,o.vertex);
                 return o;
             }
             
             fixed4 frag (v2f i) : SV_Target
             {

				 fixed4 c = lerp(_TopC,_BotC,clamp(((i.vertex.y*_scale)+_disp),0.0,1.0));
				 
                 fixed4 col = c * tex2D(_MainTex, i.texcoord);

                 fixed4 cube = texCUBE(_ToonShade, i.cubenormal);

                 float atten = LIGHT_ATTENUATION(i);
                 fixed4 fCol = fixed4(2.0f * cube.rgb * col.rgb, col.a)*atten;

                 UNITY_APPLY_FOG(i.fogCoord, col);
                 
                 return fCol;
             }
         ENDCG
     }
 }
 
 }