
-- Read the keyframes we set on the given object
-- Save the keyframes and write them into a .anim file
global filepath = "D://Documentos//Master//tools script//MVDAnimations//20-BasicAnimation//data//assets//"
global MaxToMVD = rotateXMatrix -90

fn QuatToEulerPoint rot = (
	
	local euler = (rot as eulerAngles)
	return [euler.x, euler.y, euler.z]
)

fn GetMetersFromGU =
(
	meters = case units.systemType of
	(
		#Inches: 0.0254
		#Feet: 0.3048
		#Miles: 1609.0
		#Millimeters: 0.001
		#Centimeters: 0.01
		#Meters: 1.0
		#Kilometers: 1000.0
	)
	meters*units.SystemScale
)

struct MVDFrame (

	fr_position,
	fr_rotation,
	fr_scale,
	fr_index
)

-- Read function
-- Loop through the animation and store the data
function fetchAnimation obj nframes = (

	tframes = #()
	with animate on -- Activating animation read mode
	
	-- Iterate through all the animation frames for the given object
	for i = 0 to nframes do (
		at time i (
			local i_frame = MVDFrame()
			i_frame.fr_position = obj.position
			i_frame.fr_rotation = obj.rotation
			i_frame.fr_scale = obj.scale
			i_frame.fr_index = i
			--print i_frame
			append tframes i_frame
		)
	)
	
	return tframes
)

-- This function returns a point as string format
function stringfyPoint vec = (
	
	return (vec.x as string) + " " + (vec.y as string) + " " + (vec.z as string)
)

-- Write function
function writeAnimation filename nframes = (
	allObjects = $*
	-- Create a file with a given filepath and name
	local finalname = filepath + filename + ".anim"
	local multiplier = GetMetersFromGU()
	f = (createfile finalname)
	for obj in allObjects do (

		tframes = fetchAnimation obj nframes
		

		-- Store the header information (name + framerate)
		format (obj.name + "\n") to:f
		format (framerate as string + "\n") to:f
		format (nframes as string + "\n") to:f

		for i = 1 to nframes do (
		
			local frame = tframes[i]
			local frame_index = frame.fr_index
			local frame_pos = frame.fr_position * MaxToMVD * multiplier -- Now we have the position in our engine coordinate system
			local frame_rot = QuatToEulerPoint (frame.fr_rotation * MaxToMVD) -- Now we have the rotation in our engine coordinate system
			local frame_scal = frame.fr_scale * MaxToMVD -- Now we have the position in our engine coordinate system
			
			local final_string = (frame_index as string) + " " + stringfyPoint(frame_pos) +
			" " + stringfyPoint(frame_rot) + " " + stringfyPoint(frame_scal) + "\n"
			
			format (final_string as string) to:f
		)
	)
	
	close f
)

writeAnimation "bounce1" 100