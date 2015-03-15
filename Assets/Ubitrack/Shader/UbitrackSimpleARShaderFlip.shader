Shader "Custom/UbitrackSimpleARShaderFlip" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		 _ImageWidth ("_UbiWidthFactor", Float) = 0.11625
		 _ImageHeight ("_UbiHeightFactor", Float) = 0.119375
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		

		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;		
		float _UbiWidthFactor;
		float _UbiHeightFactor;

		struct Input {			
			float4 screenPos;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			float2 screenUV = (IN.screenPos.xy / IN.screenPos.w);		
						
			
			screenUV.x = screenUV.x * _UbiWidthFactor;
			screenUV.y = (1.0f - screenUV.y) * _UbiHeightFactor;
			
			half4 c = tex2D (_MainTex, screenUV);
			o.Albedo = c.rgb;
			
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
