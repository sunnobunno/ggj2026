shader_type spatial;
render_mode unshaded;

uniform sampler2D color : filter_nearest;

void vertex() {
	POSITION = vec4(VERTEX.xy, 1.0, 1.0);
}

void fragment() {
	ALBEDO = texture(color, UV).xyz;
	ALPHA = texture(color, UV).a;
}

//void light() {
//	// Called for every pixel for every light affecting the material.
//	// Uncomment to replace the default light processing function with this one.
//}
