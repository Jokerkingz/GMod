///*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:5323,x:32698,y:32664,varname:node_5323,prsc:2|emission-2425-RGB,clip-327-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:5499,x:32411,y:32777,ptovrint:False,ptlb:node_5499,ptin:_node_5499,varname:node_5499,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:bcd545d5818454154bb0afe299278a06,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Append,id:1235,x:32229,y:32600,varname:node_1235,prsc:2|A-9813-OUT,B-4803-OUT;n:type:ShaderForge.SFN_Vector1,id:4803,x:32011,y:32634,varname:node_4803,prsc:2,v1:0;n:type:ShaderForge.SFN_Tex2d,id:2425,x:32455,y:32600,varname:node_2425,prsc:2,tex:bcd545d5818454154bb0afe299278a06,ntxv:0,isnm:False|UVIN-1235-OUT,TEX-5499-TEX;n:type:ShaderForge.SFN_Tex2d,id:2980,x:31604,y:32907,ptovrint:False,ptlb:NOISE,ptin:_NOISE,varname:node_2980,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:3736,x:31665,y:32638,ptovrint:False,ptlb:node_3736,ptin:_node_3736,varname:node_3736,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:327,x:31837,y:32947,varname:node_327,prsc:2|A-3308-OUT,B-2980-R;n:type:ShaderForge.SFN_RemapRange,id:3308,x:31853,y:32728,varname:node_3308,prsc:2,frmn:0,frmx:1,tomn:-0.6,tomx:0.6|IN-7787-OUT;n:type:ShaderForge.SFN_OneMinus,id:7787,x:31665,y:32728,varname:node_7787,prsc:2|IN-3736-OUT;n:type:ShaderForge.SFN_RemapRange,id:2682,x:32010,y:32994,varname:node_2682,prsc:2,frmn:0,frmx:1,tomn:-4,tomx:4|IN-327-OUT;n:type:ShaderForge.SFN_Clamp01,id:608,x:32189,y:32994,varname:node_608,prsc:2|IN-2682-OUT;n:type:ShaderForge.SFN_OneMinus,id:9813,x:32217,y:32753,varname:node_9813,prsc:2|IN-608-OUT;proporder:5499-2980-3736;pass:END;sub:END;*/

Shader "Unlit/NewUnlitShader" {
    Properties {
        _node_5499 ("node_5499", 2D) = "white" {}
        _NOISE ("NOISE", 2D) = "white" {}
        _node_3736 ("node_3736", Range(0, 1)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        LOD 100
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_5499; uniform float4 _node_5499_ST;
            uniform sampler2D _NOISE; uniform float4 _NOISE_ST;
            uniform float _node_3736;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _NOISE_var = tex2D(_NOISE,TRANSFORM_TEX(i.uv0, _NOISE));
                float node_327 = (((1.0 - _node_3736)*1.2+-0.6)+_NOISE_var.r);
                clip(node_327 - 0.5);
////// Lighting:
////// Emissive:
                float2 node_1235 = float2((1.0 - saturate((node_327*8.0+-4.0))),0.0);
                float4 node_2425 = tex2D(_node_5499,TRANSFORM_TEX(node_1235, _node_5499));
                float3 emissive = node_2425.rgb;
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _NOISE; uniform float4 _NOISE_ST;
            uniform float _node_3736;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _NOISE_var = tex2D(_NOISE,TRANSFORM_TEX(i.uv0, _NOISE));
                float node_327 = (((1.0 - _node_3736)*1.2+-0.6)+_NOISE_var.r);
                clip(node_327 - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    //CustomEditor "ShaderForgeMaterialInspector"
}

