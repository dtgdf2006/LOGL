#version 330 core
out vec4 FragColor;

in vec3 ourColor;
in vec3 vertex;
void main()
{
   FragColor.rgb = vertex;
}