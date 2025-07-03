Shader "Outer Outline"
{
    Properties
    {
        _MainTex      ("Main Texture",    2D)    = "white" {}
        _OutlineColor ("Outline Color",   Color)= (1,1,1,1)
        _OutlineSize  ("Outline Size(px)", Float)= 1.0
    }
    SubShader
    {
        Tags
        {
            "Queue"         = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType"    = "Transparent"
            "CanvasShader"  = "True"
        }
        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            Name "OUTLINE_AND_MAIN"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4   _MainTex_ST;
            float4   _OutlineColor;
            float    _OutlineSize;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
                float4 color  : COLOR;
            };
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv     : TEXCOORD0;
                float4 color  : COLOR;
            };

            v2f vert(appdata IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.uv     = TRANSFORM_TEX(IN.uv, _MainTex);
                OUT.color  = IN.color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                // 原图像素
                fixed4 src = tex2D(_MainTex, IN.uv) * IN.color;

                // 计算屏幕空间中的像素偏移
                float2 texel = _OutlineSize / _ScreenParams.xy;

                // 采样周围八个方向 alpha
                fixed alphaSum =
                    tex2D(_MainTex, IN.uv + float2( texel.x,  0)).a +
                    tex2D(_MainTex, IN.uv + float2(-texel.x,  0)).a +
                    tex2D(_MainTex, IN.uv + float2( 0,  texel.y)).a +
                    tex2D(_MainTex, IN.uv + float2( 0, -texel.y)).a +
                    tex2D(_MainTex, IN.uv + float2( texel.x,  texel.y)).a +
                    tex2D(_MainTex, IN.uv + float2(-texel.x,  texel.y)).a +
                    tex2D(_MainTex, IN.uv + float2( texel.x, -texel.y)).a +
                    tex2D(_MainTex, IN.uv + float2(-texel.x, -texel.y)).a;

                // 如果当前像素基本透明，但周围有不透明像素，则输出纯描边
                if (src.a < 0.1 && alphaSum > 0.0)
                {
                    return _OutlineColor;
                }

                // 否则只输出原图
                return src;
            }
            ENDCG
        }
    }
}





