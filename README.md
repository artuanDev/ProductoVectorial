# Cross Product Examples

A small Unity project with visual examples of practical uses for the cross product.

## Requirements

- Unity 6000.6.0f1

## Examples

- **Right / Left** - Uses the sign of a cross product to determine which side of an object contains a target.
- **Mesh Normals** - Calculates a triangle's face normal from two edges and demonstrates how winding changes its direction.
- **Camera Orientation** - Builds the camera's forward, right, and corrected up vectors with cross products. It also shows the failure case where forward and the reference up are parallel.

## Running the examples

Open a scene from `Assets/Scenes` and view it in the Scene window with Gizmos enabled. Move the target objects to see the vectors update in real time.

The example scripts are in `Assets/ProductoVectorial/Scripts`.
