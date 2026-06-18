Shader "Hidden/SeparableGaussian"
{
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Offset ("Offset", Vector) = (1,0,0,0)
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        Cull Off ZWrite Off ZTest Always
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float2 _Offset;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            v2f vert(appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                float2 texel = _MainTex_TexelSize.xy;
                fixed4 c = tex2D(_MainTex, i.uv) * 0.2270270270;
                c += tex2D(_MainTex, i.uv + _Offset * texel) * 0.3162162162;
                c += tex2D(_MainTex, i.uv - _Offset * texel) * 0.3162162162;
                c += tex2D(_MainTex, i.uv + _Offset * 2.0 * texel) * 0.0702702703;
                c += tex2D(_MainTex, i.uv - _Offset * 2.0 * texel) * 0.0702702703;
                return c;
            }
            ENDCG
        }
    }
}
