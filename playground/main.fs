#version 330 core
out vec4 FragColor;

in vec2 st;
in vec3 normal;
in vec3 fragPos;

uniform sampler2D texture1;
uniform sampler2D texture2;
uniform float switchTexture;
uniform vec3 lightPos;
uniform vec3 cameraPos;

void main()
{
	float ambientStrength = 0.1f;
	vec3 lightColor = vec3(1.0, 1.0, 1.0);
	//FragColor = mix(texture(texture1, st), texture(texture2, st), switchTexture);
	vec3 lightDir = normalize(lightPos - fragPos);
	vec3 norm = normalize(normal);
	float diffStrength = max(dot(lightDir, norm), 0.0f);
	vec3 diff = lightColor * diffStrength;
	vec3 viewDir = normalize(cameraPos - fragPos);
	float spec = pow(max(dot(reflect(-lightDir, norm), viewDir), 0.0), 32);
	vec3 specular = 0.6 * spec * lightColor;
	FragColor.rgb = vec3(1.0f, 0.5f, 0.31f) * (lightColor * ambientStrength + diff + specular);
}