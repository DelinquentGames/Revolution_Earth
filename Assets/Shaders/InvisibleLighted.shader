Shader "FX/InvisibleLighted" 

{

    Properties 

    {

        _Multiplier("LightIntensity",Float)=1

    }

    SubShader 

    {

        Tags {"Queue" = "Transparent+10"}

 

        GrabPass { }

 

        CGPROGRAM

        #pragma surface surf Lambert

 

        sampler2D _GrabTexture;

        float _Multiplier;

 

        struct Input 

        {

            float4 screenPos;

        };

 

        void surf (Input IN, inout SurfaceOutput o) 

        {

            float2 screenUV = IN.screenPos.xy / IN.screenPos.w;

            #ifdef SHADER_API_D3D9

            if (_ProjectionParams.x > 0) 

            {

                screenUV.y = 1.0f - screenUV.y;

            }

            #endif

            fixed4 c = tex2D(_GrabTexture, screenUV);

            o.Albedo = c.rgb*_Multiplier*2;

        }

        ENDCG

    }

}