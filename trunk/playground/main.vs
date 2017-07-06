#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 aTexCoord;
layout (location = 2) in vec3 aNormal;

out vec2 st;
out vec3 normal;
out vec3 fragPos;

uniform mat4 MVP;
uniform mat3 normalModel;
uniform mat3 model;

void main()
{
	gl_Position = MVP * vec4(aPos, 1.0);
	st = aTexCoord;
	normal = normalModel * aNormal;
	fragPos = model * aPos;
}