// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Unlit alpha-blended shader.
 // - no lighting
 // - no lightmap support
 // - no per-material color
 
 Shader "Unlit/Transparent" {
 Properties {
     _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	 _TopC("Top Colour", Color) = (.5,.5,.5,1)
	 _BotC("Bottom Colour", Color) = (.5,.5,.5,1)
     _Scale("Scale",float) = 0.0
     _Displacement("Displacement",float) = 0.0
 }
 
 SubShader {
     Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
     LOD 100
     
     ZWrite Off
     Blend SrcAlpha OneMinusSrcAlpha 
     
     Pass {  
         CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
             #pragma multi_compile_fog
             
             #include "UnityCG.cginc"
 
             struct appdata_t {
                 float4 vertex : POSITION;
                 float2 texcoord : TEXCOORD0;
             };
 
             struct v2f {
                 float4 vertex : SV_POSITION;
                 half2 texcoord : TEXCOORD0;
                 UNITY_FOG_COORDS(1)
             };
 
             sampler2D _MainTex;
             float4 _MainTex_ST;
			 float4 _TopC;
			 float4 _BotC;
             float _Scale;
             float _Displacement;
             
             v2f vert (appdata_t v)
             {
                 v2f o;
                 o.vertex = UnityObjectToClipPos(v.vertex);
                 o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                 UNITY_TRANSFER_FOG(o,o.vertex);
                 return o;
             }
             
             fixed4 frag (v2f i) : SV_Target
             {

				 fixed4 c = lerp(_TopC,_BotC,clamp((i.texcoord.y*_Scale)+_Displacement,0.0,1.0));
				 
                 fixed4 col = c * tex2D(_MainTex, i.texcoord);

				 if(i.texcoord.y>0.95){
					 col = fixed4(0.0,0.0,0.0,0.0);
				 }
                 UNITY_APPLY_FOG(i.fogCoord, col);
                 return col;
             }
         ENDCG
     }
 }
 
 }