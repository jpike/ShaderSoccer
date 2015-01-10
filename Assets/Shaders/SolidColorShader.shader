/// Colors an object with a solid color.
Shader "Custom/SolidColorShader" 
{
	Properties 
    {
		_Color ("Color", Color) = (0.0, 0.0, 0.0, 1.0)
	}
	SubShader 
    {
		Pass 
        {
            CGPROGRAM

            #pragma enable_d3d11_debug_symbols
            #pragma vertex VertexShading
            #pragma fragment FragmentShading

            /// The color for the object.
            uniform fixed4 _Color;

            /// Transforms the vertex into normalized device coordinates.
            /// @param[in]  vertex - The model space coordinates of the vertex.
            /// @return     The transformed vertex.
            float4 VertexShading(float4 vertex : POSITION) : SV_POSITION
            {
                // Transform the vertex into the canonical view volume.
                return mul(UNITY_MATRIX_MVP, vertex);
            }

            /// Returns the color of the fragment.
            /// @return The solid color configured for this shader.
            fixed4 FragmentShading() : COLOR
            {
                return _Color;
            }

            ENDCG
        }
	} 
}
