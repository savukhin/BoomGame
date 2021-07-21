Shader "Unlit/GradientTransperentShader"
{
    Properties
    {
        _BottomColor ("Bottom Color", Color) = (0, 0, 0, 0)
        _UpperColor ("Upper Color", Color) = (0, 0, 0, 0)
        _GradientPower ("Gradient Power", float) = 1.4
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            fixed4 _BottomColor;
            fixed4 _UpperColor;
            float _GradientPower;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);

                fixed4 col = (_BottomColor * max(1 - i.uv.y * _GradientPower, 0) + _UpperColor * i.uv.y);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                col.a = max(1 - i.uv.y * _GradientPower, 0);
                return col;
            }
            ENDCG
        }
    }
}
