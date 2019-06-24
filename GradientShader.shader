// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unlit alpha-blended shader.
 // - no lighting
 // - no lightmap support
 // - no per-material color
 
 Shader "Custom/GradientShader" {
 Properties {
     _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
     _ToonShade ("ToonShader Cubemap(RGB)", CUBE) = "" { }
	 _TopC("Top Colour", Color) = (.5,.5,.5,1)
	 _BotC("Bottom Colour", Color) = (.5,.5,.5,1)
	_disp("Displacement",float)= 1.
	_scale("Scale",float) = 1.
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
                 float3 normal : NORMAL;
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
                 o.normal = v.normal;
                 UNITY_TRANSFER_FOG(o,o.vertex);
                 return o;
             }
             
             fixed4 frag (v2f i) : SV_Target
             {

				 //fixed4 c = lerp(_TopC,_BotC,sin((i.vertex.y*_scale)+_disp+i.normal.x*_faceDisp));
				
                float y = i.vertex.y;

                
                float x = (sin((y*_scale)+_disp+dot(i.normal,float3(1.0,1.0,1.0)))+1.0)*0.5;


                 fixed4 c = lerp(_TopC,_BotC,clamp(x,0,1));
                 
				c.w = 1.0;

                 fixed4 cube = texCUBE(_ToonShade, i.cubenormal);


                 //fixed4 col =  tex2D(_MainTex, i.texcoord);

                 

                 float atten = LIGHT_ATTENUATION(i);
                 fixed4 fCol = fixed4(2.0f * cube.rgb * c.rgb, c.a)*c;
                //fixed4 fCol = fixed4(c*cube.rgb,1.0 )*atten;
                

                 UNITY_APPLY_FOG(i.fogCoord, col);
                 
                 return fCol;
             }
         ENDCG
     }
 }
 
 }