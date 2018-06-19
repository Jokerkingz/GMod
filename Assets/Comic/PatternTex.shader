// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:417,x:32739,y:32629,varname:node_417,prsc:2|emission-5124-OUT,olwid-324-OUT,olcol-4448-RGB;n:type:ShaderForge.SFN_LightVector,id:1158,x:32074,y:32639,varname:node_1158,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:3496,x:32074,y:32783,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:7199,x:32265,y:32639,varname:node_7199,prsc:2,dt:0|A-1158-OUT,B-3496-OUT;n:type:ShaderForge.SFN_Append,id:6296,x:32287,y:32857,varname:node_6296,prsc:2|A-7199-OUT,B-4204-OUT;n:type:ShaderForge.SFN_Vector1,id:4204,x:32074,y:32989,varname:node_4204,prsc:2,v1:0;n:type:ShaderForge.SFN_Step,id:2372,x:32493,y:32608,varname:node_2372,prsc:2|A-3995-R,B-7199-OUT;n:type:ShaderForge.SFN_Tex2d,id:3995,x:32265,y:32469,ptovrint:False,ptlb:Pattern,ptin:_Pattern,varname:node_3995,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5f0ae3fa9bd0d9b48b89c99f2e6b8c81,ntxv:3,isnm:False|UVIN-4543-OUT;n:type:ShaderForge.SFN_ScreenPos,id:6480,x:32074,y:32469,varname:node_6480,prsc:2,sctp:1;n:type:ShaderForge.SFN_Color,id:2472,x:32486,y:32339,ptovrint:False,ptlb:Dark,ptin:_Dark,varname:node_2472,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Color,id:4162,x:32631,y:32445,ptovrint:False,ptlb:Light,ptin:_Light,varname:node_4162,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Lerp,id:5124,x:32852,y:32321,varname:node_5124,prsc:2|A-2472-RGB,B-4162-RGB,T-2372-OUT;n:type:ShaderForge.SFN_Depth,id:6407,x:31886,y:32479,varname:node_6407,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4543,x:31846,y:32680,varname:node_4543,prsc:2|A-6480-UVOUT,B-6407-OUT;n:type:ShaderForge.SFN_ValueProperty,id:324,x:32377,y:33078,ptovrint:False,ptlb:Outline Width,ptin:_OutlineWidth,varname:node_324,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Color,id:4448,x:32575,y:33124,ptovrint:False,ptlb:Outline Color,ptin:_OutlineColor,varname:node_4448,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;proporder:3995-2472-4162-324-4448;pass:END;sub:END;*/

Shader "Unlit/PatternTex" {
    Properties {
        _Pattern ("Pattern", 2D) = "bump" {}
        _Dark ("Dark", Color) = (0.5,0.5,0.5,1)
        _Light ("Light", Color) = (0.5,0.5,0.5,1)
        _OutlineWidth ("Outline Width", Float ) = 0
        _OutlineColor ("Outline Color", Color) = (0.5,0.5,0.5,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 100
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float _OutlineWidth;
            uniform float4 _OutlineColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_FOG_COORDS(0)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( float4(v.vertex.xyz + v.normal*_OutlineWidth,1) );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                return fixed4(_OutlineColor.rgb,0);
            }
            ENDCG
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
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Pattern; uniform float4 _Pattern_ST;
            uniform float4 _Dark;
            uniform float4 _Light;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 projPos : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
////// Emissive:
                float2 node_4543 = (float2((sceneUVs.x * 2 - 1)*(_ScreenParams.r/_ScreenParams.g), sceneUVs.y * 2 - 1).rg*partZ);
                float4 _Pattern_var = tex2D(_Pattern,TRANSFORM_TEX(node_4543, _Pattern));
                float node_7199 = dot(lightDirection,i.normalDir);
                float3 emissive = lerp(_Dark.rgb,_Light.rgb,step(_Pattern_var.r,node_7199));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
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
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Pattern; uniform float4 _Pattern_ST;
            uniform float4 _Dark;
            uniform float4 _Light;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 projPos : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
////// Lighting:
                float3 finalColor = 0;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
