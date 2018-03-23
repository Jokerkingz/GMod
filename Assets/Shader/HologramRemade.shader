// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:32920,y:32691,varname:node_4795,prsc:2|emission-9538-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32235,y:32601,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:33153,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32235,y:32930,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32235,y:33081,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_ValueProperty,id:6632,x:29523,y:32461,ptovrint:False,ptlb:Noise Time,ptin:_NoiseTime,varname:node_6632,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Time,id:8639,x:29551,y:32962,varname:node_8639,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:745,x:29893,y:31981,ptovrint:False,ptlb:Noise Posterized,ptin:_NoisePosterized,varname:node_745,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_Vector1,id:3362,x:29770,y:32196,varname:node_3362,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:276,x:29886,y:32576,varname:node_276,prsc:2|A-6632-OUT,B-8639-TSL;n:type:ShaderForge.SFN_Frac,id:9957,x:30066,y:32576,varname:node_9957,prsc:2|IN-276-OUT;n:type:ShaderForge.SFN_Multiply,id:1169,x:30246,y:32576,varname:node_1169,prsc:2|A-9957-OUT,B-1161-OUT,C-6632-OUT;n:type:ShaderForge.SFN_Round,id:4936,x:30436,y:32576,varname:node_4936,prsc:2|IN-1169-OUT;n:type:ShaderForge.SFN_Add,id:3132,x:30627,y:32576,varname:node_3132,prsc:2|A-4936-OUT,B-4362-UVOUT;n:type:ShaderForge.SFN_Posterize,id:5157,x:30819,y:32576,varname:node_5157,prsc:2|IN-3132-OUT,STPS-1161-OUT;n:type:ShaderForge.SFN_Noise,id:1284,x:30985,y:32576,varname:node_1284,prsc:2|XY-5157-OUT;n:type:ShaderForge.SFN_Noise,id:8636,x:30985,y:32444,varname:node_8636,prsc:2|XY-6557-OUT;n:type:ShaderForge.SFN_Noise,id:5102,x:30985,y:32303,varname:node_5102,prsc:2|XY-1925-OUT;n:type:ShaderForge.SFN_Multiply,id:6398,x:30246,y:32439,varname:node_6398,prsc:2|A-6632-OUT,B-745-OUT,C-9957-OUT;n:type:ShaderForge.SFN_Multiply,id:4055,x:30246,y:32303,varname:node_4055,prsc:2|A-3362-OUT,B-745-OUT,C-6632-OUT,D-9957-OUT;n:type:ShaderForge.SFN_Posterize,id:6557,x:30819,y:32444,varname:node_6557,prsc:2|IN-5726-OUT,STPS-745-OUT;n:type:ShaderForge.SFN_Posterize,id:1925,x:30819,y:32303,varname:node_1925,prsc:2|IN-8582-OUT,STPS-745-OUT;n:type:ShaderForge.SFN_Add,id:5726,x:30627,y:32444,varname:node_5726,prsc:2|A-1846-OUT,B-4362-UVOUT;n:type:ShaderForge.SFN_Add,id:8582,x:30627,y:32303,varname:node_8582,prsc:2|A-3566-OUT,B-4362-UVOUT;n:type:ShaderForge.SFN_Round,id:1846,x:30436,y:32444,varname:node_1846,prsc:2|IN-6398-OUT;n:type:ShaderForge.SFN_Round,id:3566,x:30436,y:32303,varname:node_3566,prsc:2|IN-4055-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1161,x:30167,y:32825,ptovrint:False,ptlb:NoisePosterize,ptin:_NoisePosterize,varname:node_1161,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:70;n:type:ShaderForge.SFN_TexCoord,id:4362,x:30436,y:32762,varname:node_4362,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:5846,x:30306,y:32961,varname:node_5846,prsc:2|A-8639-T,B-8658-OUT;n:type:ShaderForge.SFN_Add,id:2052,x:30481,y:32961,varname:node_2052,prsc:2|A-4362-V,B-5846-OUT;n:type:ShaderForge.SFN_Multiply,id:5807,x:30668,y:32961,varname:node_5807,prsc:2|A-2052-OUT,B-2080-OUT;n:type:ShaderForge.SFN_Frac,id:3346,x:30855,y:32961,varname:node_3346,prsc:2|IN-5807-OUT;n:type:ShaderForge.SFN_Slider,id:8658,x:29982,y:33203,ptovrint:False,ptlb:Speed of Waves,ptin:_SpeedofWaves,varname:node_8658,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-3,cur:3,max:3;n:type:ShaderForge.SFN_Slider,id:2080,x:30349,y:33207,ptovrint:False,ptlb:Segments,ptin:_Segments,varname:node_2080,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-10,cur:4,max:10;n:type:ShaderForge.SFN_Slider,id:5160,x:30698,y:33212,ptovrint:False,ptlb:Power of Waves,ptin:_PowerofWaves,varname:node_5160,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:3,max:3;n:type:ShaderForge.SFN_Slider,id:1311,x:31166,y:32472,ptovrint:False,ptlb:Power of Noise,ptin:_PowerofNoise,varname:node_1311,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3,max:1;n:type:ShaderForge.SFN_Multiply,id:9538,x:32665,y:32768,varname:node_9538,prsc:2|A-6010-OUT,B-6861-OUT;n:type:ShaderForge.SFN_Clamp01,id:4982,x:32235,y:32768,varname:node_4982,prsc:2|IN-6526-OUT;n:type:ShaderForge.SFN_Multiply,id:7142,x:31527,y:32539,varname:node_7142,prsc:2|A-1311-OUT,B-4540-OUT,C-1284-OUT;n:type:ShaderForge.SFN_Add,id:3233,x:31166,y:32648,varname:node_3233,prsc:2|A-5102-OUT,B-8636-OUT;n:type:ShaderForge.SFN_Clamp01,id:4540,x:31348,y:32648,varname:node_4540,prsc:2|IN-3233-OUT;n:type:ShaderForge.SFN_Clamp01,id:3559,x:31348,y:32811,varname:node_3559,prsc:2|IN-7142-OUT;n:type:ShaderForge.SFN_Add,id:6526,x:31577,y:32811,varname:node_6526,prsc:2|A-3559-OUT,B-1040-OUT;n:type:ShaderForge.SFN_Power,id:1040,x:31060,y:32961,varname:node_1040,prsc:2|VAL-3346-OUT,EXP-5160-OUT;n:type:ShaderForge.SFN_Multiply,id:6010,x:32466,y:32768,varname:node_6010,prsc:2|A-6074-RGB,B-4982-OUT,C-797-RGB,D-9248-OUT;n:type:ShaderForge.SFN_Multiply,id:6861,x:32466,y:32964,varname:node_6861,prsc:2|A-6074-A,B-2053-A;proporder:6074-797-6632-745-1161-8658-2080-5160-1311;pass:END;sub:END;*/

Shader "Butt/Plug" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _NoiseTime ("Noise Time", Float ) = 2
        _NoisePosterized ("Noise Posterized", Float ) = 5
        _NoisePosterize ("NoisePosterize", Float ) = 70
        _SpeedofWaves ("Speed of Waves", Range(-3, 3)) = 3
        _Segments ("Segments", Range(-10, 10)) = 4
        _PowerofWaves ("Power of Waves", Range(0, 3)) = 3
        _PowerofNoise ("Power of Noise", Range(0, 1)) = 0.3
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform float _NoiseTime;
            uniform float _NoisePosterized;
            uniform float _NoisePosterize;
            uniform float _SpeedofWaves;
            uniform float _Segments;
            uniform float _PowerofWaves;
            uniform float _PowerofNoise;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_8639 = _Time;
                float node_9957 = frac((_NoiseTime*node_8639.r));
                float2 node_1925 = floor((round((2.0*_NoisePosterized*_NoiseTime*node_9957))+i.uv0) * _NoisePosterized) / (_NoisePosterized - 1);
                float2 node_5102_skew = node_1925 + 0.2127+node_1925.x*0.3713*node_1925.y;
                float2 node_5102_rnd = 4.789*sin(489.123*(node_5102_skew));
                float node_5102 = frac(node_5102_rnd.x*node_5102_rnd.y*(1+node_5102_skew.x));
                float2 node_6557 = floor((round((_NoiseTime*_NoisePosterized*node_9957))+i.uv0) * _NoisePosterized) / (_NoisePosterized - 1);
                float2 node_8636_skew = node_6557 + 0.2127+node_6557.x*0.3713*node_6557.y;
                float2 node_8636_rnd = 4.789*sin(489.123*(node_8636_skew));
                float node_8636 = frac(node_8636_rnd.x*node_8636_rnd.y*(1+node_8636_skew.x));
                float2 node_5157 = floor((round((node_9957*_NoisePosterize*_NoiseTime))+i.uv0) * _NoisePosterize) / (_NoisePosterize - 1);
                float2 node_1284_skew = node_5157 + 0.2127+node_5157.x*0.3713*node_5157.y;
                float2 node_1284_rnd = 4.789*sin(489.123*(node_1284_skew));
                float node_1284 = frac(node_1284_rnd.x*node_1284_rnd.y*(1+node_1284_skew.x));
                float3 emissive = ((_MainTex_var.rgb*saturate((saturate((_PowerofNoise*saturate((node_5102+node_8636))*node_1284))+pow(frac(((i.uv0.g+(node_8639.g*_SpeedofWaves))*_Segments)),_PowerofWaves)))*_TintColor.rgb*2.0)*(_MainTex_var.a*i.vertexColor.a));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
