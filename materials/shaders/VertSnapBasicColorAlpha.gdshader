shader_type spatial;
render_mode depth_draw_opaque, depth_prepass_alpha,
	skip_vertex_transform, diffuse_lambert,
	specular_disabled, vertex_lighting;

uniform vec4 color : source_color = vec4(0.5, 0.5, 0.5, 1.0);
uniform float snap_factor : hint_range(50.0, 200.0, 10.0) = 100.0;

uniform vec3 uv1_scale = vec3(1.0, 1.0, 1.0);
uniform vec3 uv1_offset = vec3(0.0, 0.0, 0.0);

void vertex()
{
	VERTEX = (MODELVIEW_MATRIX * vec4(VERTEX, 1.0)).xyz;
	NORMAL = normalize((MODELVIEW_MATRIX * vec4(NORMAL, 0.0)).xyz);
	BINORMAL = normalize((MODELVIEW_MATRIX * vec4(BINORMAL, 0.0)).xyz);
	TANGENT = normalize((MODELVIEW_MATRIX * vec4(TANGENT, 0.0)).xyz);

	float dist = length(VERTEX);
	float distFactor = 2.0 / dist;
	VERTEX = vec3(ivec3(VERTEX * snap_factor * distFactor)) / (snap_factor * distFactor);

	UV = UV * uv1_scale.xy + uv1_offset.xy;
}


void fragment() {
	ALBEDO = color.xyz * COLOR.xyz;
	ALPHA = color.a;
	// Called for every pixel the material is visible on.
}

//void light() {
	// Called for every pixel for every light affecting the material.
	// Uncomment to replace the default light processing function with this one.
//}
