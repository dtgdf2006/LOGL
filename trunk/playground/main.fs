#version 330 core
out vec4 FragColor;

in vec2 st;

uniform sampler2D texture1;
uniform sampler2D texture2;
uniform float switchTexture;

void main()
{
   FragColor = mix(texture(texture1, st), texture(texture2, st), switchTexture);
}