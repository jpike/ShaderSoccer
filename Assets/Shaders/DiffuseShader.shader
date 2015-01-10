/// Colors an object with diffuse or Lambertian shading.
/// Ambient light and a single point light source are both taken into account.
Shader "Custom/DiffuseShader" 
{
	Properties 
    {
		_DiffuseColor ("DiffuseColor", Color) = (0.0, 0.0, 0.0, 1.0)
        _AmbientColor ("AmbientColor", Color) = (0.0, 0.0, 0.0, 1.0)
        _LightColor ("LightColor", Color) = (0.0, 0.0, 0.0, 1.0)
        _LightWorldPosition ("LightWorldPosition", Vector) = (0.0, 0.0, 0.0, 1.0)
	}
	SubShader 
    {
		Pass 
        {
            CGPROGRAM

            #pragma enable_d3d11_debug_symbols
            #pragma vertex VertexShading
            #pragma fragment FragmentShading

            /// The diffuse color of the surface of the object.
            uniform fixed4 _DiffuseColor;
            /// The ambient light color.  A custom ambient light color,
            /// rather than Unity's built-in uniform, was chosen because
            /// to get shading close to Unity's built-in shaders, the
            /// ambient light must be doubled (not sure why), and I didn't
            /// want to inject arbitrary doubling without understanding why.
            uniform fixed4 _AmbientColor;
            /// @todo   Consider using Unity's built-in light uniforms?
            /// The point light color.
            uniform fixed4 _LightColor;
            /// The point light's world position.
            uniform float4 _LightWorldPosition;

            /// The input to the vertex shader.
            struct VertexInput
            {
                /// The model space position of the vertex.
                float4 vertex : POSITION;
                /// The normal of the vertex.
                float3 normal : NORMAL;
            };

            /// The output of the vertex shader, also
            /// used as input to the fragment shader.
            struct VertexOutput
            {
                /// The transformed vertex position.
                float4 transformedPosition : SV_POSITION;
                /// The color.
                fixed4 color : COLOR;
            };

            /// Transforms the vertex into normalized device coordinates
            /// and computes the diffuse color for the vertex.
            /// @param[in]  vertex - Information about the vertex.
            /// @return     The transformed vertex and diffuse color.
            VertexOutput VertexShading(VertexInput vertex)
            {
                // CALCULATE THE DIRECTION FROM THE VERTEX TO THE LIGHT.
                float3 vertexWorldPosition = mul(_Object2World, vertex.vertex).xyz;
                float3 lightDirection = _LightWorldPosition.xyz - vertexWorldPosition;
                float3 unitLightDirection = normalize(lightDirection);

                // NORMALIZE THE VERTEX'S NORMAL.
                float3 unitNormal = normalize(vertex.normal);
                
                // COMPUTE THE AMOUNT OF LIGHT ILLUMINATING THE VERTEX.
                // The illumination amount is clamped to a minimum of 0 to ensure that
                // negative dot products are not used, which could result in unexpected color values.
                // A negative dot product indicates the vertex is facing away from the light
                // so it should receive no illumination (and have a color of black).
                float vertexIllumination = max(0.0, dot(unitNormal, unitLightDirection));

                // COMPUTE THE DIFFUSE REFLECTANCE COLOR.
                fixed3 diffuseReflectance = _DiffuseColor.rgb * (
                    _AmbientColor.rgb + _LightColor.rgb * vertexIllumination);

                VertexOutput finalVertex;
                finalVertex.color = fixed4(diffuseReflectance, 1.0);

                // TRANSFORM THE VERTEX POSITION INTO THE CANONICAL VIEW VOLUME.
                finalVertex.transformedPosition = mul(UNITY_MATRIX_MVP, vertex.vertex);

                return finalVertex;
            }

            /// Returns the color of the fragment.
            /// @return The diffuse color, as computed in the vertex shader and interpolated.
            fixed4 FragmentShading(VertexOutput input) : COLOR
            {
                return input.color;
            }

            ENDCG
        }
	} 
}
