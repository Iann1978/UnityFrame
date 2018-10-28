Shader "Custom/GradientText" {
    Properties {
		_MainTex("Main Texture", 2D) = "white" {}
		_TopColor("Top Color", Color) = (0,0,0,1)
		_BottomColor("Bottom Color", Color) = (1,1,1,1)
    }
		
    SubShader {
    
        Pass {
           
            
			ZTest Always
			Blend SrcAlpha OneMinusSrcAlpha
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			sampler2D _MainTex;
			uniform float4 _TopColor;
            uniform float4 _BottomColor;
            struct appdata {
                float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
            };
						
            struct v2f {
                float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 pos1 : TEXCOORD1;
            };

			v2f vert (appdata v) {
                v2f o;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.pos1 = v.vertex;
				o.uv = v.uv;
                return o;
            }
			
            fixed4 frag(v2f i) : COLOR {
				fixed4 c = tex2D(_MainTex, i.uv);
				c.rgb = i.pos1.y/256;//lerp(_TopColor, _BottomColor, i.pos1.y);
				//c.r = 1;
				//c.g = 1;
				//c.b = 1;
				c.a = 1;
				return c;
            }
            ENDCG
        }        
    }
    
}
