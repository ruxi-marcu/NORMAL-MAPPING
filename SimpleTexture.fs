#version 330 core

in vec2 UV;
in vec3 Position_worldspace;
in vec3 Normal_cameraspace;
in vec3 EyeDirection_cameraspace;
in vec3 LightDirection_cameraspace;

out vec3 color;

uniform sampler2D DiffuseTextureSampler;
uniform sampler2D SpecularTextureSampler;
uniform mat4 mvp;
uniform vec3 LightPosW;

void main(){

	vec3 LightColor = vec3(1,1,1);
	float LightPower = 50.0f;
	
	vec3 MaterialDiffuseColor = texture( DiffuseTextureSampler, UV ).rgb;
	vec3 MaterialAmbientColor = vec3(0.1,0.1,0.1) * MaterialDiffuseColor;
	vec3 MaterialSpecularColor = texture( SpecularTextureSampler, UV ).rgb * 0.3;

	float distance = length( LightPosW - Position_worldspace );

	vec3 n = normalize( Normal_cameraspace );
	vec3 l = normalize( LightDirection_cameraspace );
	float cosTheta = clamp( dot( n,l ), 0,1 );
	
	vec3 E = normalize(EyeDirection_cameraspace);
	vec3 R = reflect(-l,n);
	float cosAlpha = clamp( dot( E,R ), 0,1 );
	
	color = (MaterialAmbientColor + MaterialSpecularColor + MaterialSpecularColor) 
		* ((LightColor * LightPower * cosTheta / (distance*distance))
		+ (LightColor * LightPower * pow(cosAlpha,5) / (distance*distance)));
		

}