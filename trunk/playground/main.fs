#version 330 core
out vec4 FragColor;

in VS_OUT {
    vec3 FragPos;
    vec3 Normal;
    vec2 TexCoords;
	vec4 LightSpaceCoords;
} fs_in;

uniform sampler2D diffuseTexture;
uniform sampler2D depthMap;

uniform vec3 lightPos;
uniform vec3 viewPos;

float ShadowCaculation(vec4 LightSpaceCoords, float bias)
{
	vec3 projCoords = LightSpaceCoords.xyz / LightSpaceCoords.w;
	projCoords = projCoords / 2.0 + 0.5;
	float closestDepth = texture(depthMap, projCoords.xy).r + bias;
	float currentDepth = clamp(projCoords.z, 0.0, 1.0);
	return currentDepth > closestDepth ? 1.0 : 0.0;
}

void main()
{           
    vec3 color = texture(diffuseTexture, fs_in.TexCoords).rgb;
    vec3 normal = normalize(fs_in.Normal);
    vec3 lightColor = vec3(0.3);
    // ambient
    vec3 ambient = 0.3 * color;
    // diffuse
    vec3 lightDir = normalize(lightPos - fs_in.FragPos);
    float diff = max(dot(lightDir, normal), 0.0);
    vec3 diffuse = diff * lightColor;
    // specular
    vec3 viewDir = normalize(viewPos - fs_in.FragPos);
    vec3 reflectDir = reflect(-lightDir, normal);
    float spec = 0.0;
    vec3 halfwayDir = normalize(lightDir + viewDir);  
    spec = pow(max(dot(normal, halfwayDir), 0.0), 64.0);
    vec3 specular = spec * lightColor;    
    //float bias = max(0.05 * (1.0 - dot(normal, lightDir)), 0.005);
	float shadow = ShadowCaculation(fs_in.LightSpaceCoords, 0.0);  
	
    vec3 lighting = (ambient + (1.0 - shadow) * (diffuse + specular)) * color;    
    
    FragColor = vec4(lighting, 1.0);
}