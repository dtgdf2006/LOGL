#version 330 core
out vec4 FragColor;

in vec2 st;
in vec3 normal;
in vec3 fragPos;


struct Light {
	vec3 position;

	vec3 ambient;
	vec3 diffuse;
	vec3 specular;
};

struct Material {
	sampler2D diffuse;
	sampler2D specular;
	
	float shininess;
};


uniform Light light;
uniform Material material;
uniform vec3 cameraPos;

void main()
{
	vec3 ambient = light.ambient * texture(material.diffuse, st).rgb;
	vec3 lightDir = normalize(light.position - fragPos);
	vec3 norm = normalize(normal);
	float diffStrength = max(dot(lightDir, norm), 0.0f);
	vec3 diffuse = light.diffuse * diffStrength * texture(material.diffuse, st).rgb;
	vec3 viewDir = normalize(cameraPos - fragPos);
	float spec = pow(max(dot(reflect(-lightDir, norm), viewDir), 0.0), material.shininess);
	vec3 specular = spec * light.specular * texture(material.specular, st).rgb;
	FragColor.rgb = ambient + diffuse + specular;
	
	//FragColor = texture(material.diffuse, st);
}