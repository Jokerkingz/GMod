// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33283,y:32747,varname:node_3138,prsc:2|diff-7218-OUT,emission-6151-OUT;n:type:ShaderForge.SFN_TexCoord,id:5643,x:32226,y:32949,varname:node_5643,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:2680,x:32407,y:32949,varname:node_2680,prsc:2,spu:1,spv:1|UVIN-5643-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:1601,x:32580,y:32949,ptovrint:False,ptlb:Glow Pattern,ptin:_GlowPattern,varname:node_1601,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-2680-UVOUT;n:type:ShaderForge.SFN_Multiply,id:5804,x:32760,y:32949,varname:node_5804,prsc:2|A-513-OUT,B-1601-R,C-3660-OUT;n:type:ShaderForge.SFN_Power,id:513,x:32580,y:32780,varname:node_513,prsc:2|VAL-9298-OUT,EXP-1117-OUT;n:type:ShaderForge.SFN_Vector1,id:1117,x:32407,y:32882,varname:node_1117,prsc:2,v1:30;n:type:ShaderForge.SFN_OneMinus,id:9298,x:32407,y:32751,varname:node_9298,prsc:2|IN-8993-B;n:type:ShaderForge.SFN_Slider,id:3660,x:32423,y:33142,ptovrint:False,ptlb:Glow Strength,ptin:_GlowStrength,varname:node_3660,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:4,max:4;n:type:ShaderForge.SFN_Multiply,id:6151,x:32951,y:32949,varname:node_6151,prsc:2|A-9097-RGB,B-5804-OUT;n:type:ShaderForge.SFN_Color,id:9097,x:32951,y:32793,ptovrint:False,ptlb:Glow Color,ptin:_GlowColor,varname:node_9097,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.2044766,c2:0.7251074,c3:0.8970588,c4:1;n:type:ShaderForge.SFN_Tex2d,id:8993,x:32226,y:32734,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_8993,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ae2cf0b0f44c1524a9e58a7976b3ba3e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:7218,x:32039,y:32734,varname:node_7218,prsc:2|A-8993-RGB,B-3637-OUT;n:type:ShaderForge.SFN_Vector1,id:3637,x:32024,y:32911,varname:node_3637,prsc:2,v1:2;proporder:1601-3660-9097-8993;pass:END;sub:END;*/

Shader "Shader Forge/CrackGlow" {
    Properties {
        _GlowPattern ("Glow Pattern", 2D) = "white" {}
        _GlowStrength ("Glow Strength", Range(0, 4)) = 4
        _GlowColor ("Glow Color", Color) = (0.2044766,0.7251074,0.8970588,1)
        _Diffuse ("Diffuse", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _GlowPattern; uniform float4 _GlowPattern_ST;
            uniform float _GlowStrength;
            uniform float4 _GlowColor;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 diffuseColor = (_Diffuse_var.rgb*2.0);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 node_7539 = _Time;
                float2 node_2680 = (i.uv0+node_7539.g*float2(1,1));
                float4 _GlowPattern_var = tex2D(_GlowPattern,TRANSFORM_TEX(node_2680, _GlowPattern));
                float3 emissive = (_GlowColor.rgb*(pow((1.0 - _Diffuse_var.b),30.0)*_GlowPattern_var.r*_GlowStrength));
/// Final Color:
                float3 finalColor = diffuse + emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _GlowPattern; uniform float4 _GlowPattern_ST;
            uniform float _GlowStrength;
            uniform float4 _GlowColor;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 diffuseColor = (_Diffuse_var.rgb*2.0);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
