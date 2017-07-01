#version 330 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 texCoord;

out vec2 st;

uniform mat4 MVP;

void main()
{
   gl_Position = MVP * vec4(aPos.xyz, 1.0);
   st = texCoord;
}