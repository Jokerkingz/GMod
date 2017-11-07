Shader "Custom/Shd_TransGlow" {
    Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
    }
   
    SubShader {
        Tags { "Queue" = "Geometry" }
       
        Pass {
            GLSLPROGRAM
           
            #ifdef VERTEX
           
            //#extension GL_OES_standard_derivatives : enable

			//precision highp float;

			//uniform vec3 color;
			//uniform float start;
			//uniform float end;
			//uniform float alpha;

			varying vec3 fPosition;
			varying vec3 fNormal;

			void main() {
			    vec3 normal = normalize( fNormal );
			    vec3 eye = normalize( -fPosition.xyz );
			    float rim = smoothstep( start, end, 1.0 - dot( normal, eye ) );
			    float value = clamp( rim * alpha, 0.0, 1.0 );
			    gl_FragColor = vec4( color * value, value );
			}
			           
            #endif
           
            #ifdef FRAGMENT
                       
            //precision highp float;
			//attribute vec3 position;
			//attribute vec3 normal;
			//uniform mat3 normalMatrix;
			//uniform mat4 modelViewMatrix;
			//uniform mat4 projectionMatrix;
			varying vec3 fNormal;
			varying vec3 fPosition;

			void main()
			{
			  fNormal = normalize(normalMatrix * normal);
			  vec4 pos = modelViewMatrix * vec4(position, 1.0);
			  fPosition = pos.xyz;
			  gl_Position = projectionMatrix * pos;
			}
            #endif
           
            ENDGLSL
        }
    }
}
 