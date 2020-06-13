Shader "Custom/snowtracks"
{
    Properties
    {
        _Tess ("Tessellation", Range(1,64)) = 4
        _snowColor ("snow_color", Color) = (1,1,1,1) 
        _snowTex ("snow (RGB)", 2D) = "white" {}
        _groundColor ("ground_color", Color) = (1,1,1,1) 
        _groundTex ("ground (RGB)", 2D) = "white" {}
        _splat ("splatmap", 2D) = "black" {}
        _Displacement ("Displacement", Range(0, 3)) = 0.3
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:disp addshadow tessellate:tessDistance

       
        #pragma target 4.6

        #include "Tessellation.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                float2 texcoord : TEXCOORD0;
            };

            float _Tess;

            float4 tessDistance (appdata v0, appdata v1, appdata v2) {
                float minDist = 10.0;
                float maxDist = 25.0;
                return UnityDistanceBasedTess(v0.vertex, v1.vertex, v2.vertex, minDist, maxDist, _Tess);
            }

            sampler2D _splat;
            float _Displacement;

            void disp (inout appdata v)
            {
                float d = tex2Dlod(_splat, float4(v.texcoord.xy,0,0)).r * _Displacement;
                v.vertex.xyz -= v.normal * d;
                v.vertex.xyz += v.normal * _Displacement;
            }

        sampler2D _groundTex;
        fixed4 _groundColor;
        
        sampler2D _snowTex;
        fixed4 _snowColor;
        
        struct Input
        {
            float2 uv_groundTex;
            float2 uv_snowTex;
            float2 uv_splat;
        };

        half _Glossiness;
        half _Metallic;
        

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            half amount = tex2Dlod(_splat, float4(IN.uv_splat, 0, 0)).r;
            fixed4 c = lerp(tex2D (_snowTex, IN.uv_snowTex) * _snowColor, tex2D (_groundTex, IN.uv_groundTex) * _groundColor, amount);
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
