using UnityEngine;
using System.Linq;

namespace Pathfinding {
	/** Moves the target in example scenes.
	 * This is a simple script which has the sole purpose
	 * of moving the target point of agents in the example
	 * scenes for the A* Pathfinding Project.
	 *
	 * It is not meant to be pretty, but it does the job.
	 */
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_target_mover.php")]
	public class TargetMover : MonoBehaviour {
		/** Mask for the raycast placement */
		public LayerMask mask;
        public bool willWalk;

		public Transform target;
		IAstarAI[] ais;

		/** Determines if the target position should be updated every frame or only on double-click */
		public bool onlyOnDoubleClick;
		public bool use2D;
        ChangeCursor cursor;
		Camera cam;

		public void Start () {
			//Cache the Main Camera
			cam = Camera.main;
            cursor = GetComponent<ChangeCursor>();
            // Slightly inefficient way of finding all AIs, but this is just an example script, so it doesn't matter much.
            // FindObjectsOfType does not support interfaces unfortunately.
            ais = FindObjectsOfType<MonoBehaviour>().OfType<IAstarAI>().ToArray();
			useGUILayout = false;
            target = GameObject.FindWithTag("Target").transform;
        }

		public void OnGUI () {
			if (onlyOnDoubleClick && cam != null && !cursor.dialog && Event.current.type == EventType.MouseDown && Event.current.clickCount == 1) {
				UpdateTargetPosition();
               


            }
		}

		/** Update is called once per frame */
		void Update () {
			if (!onlyOnDoubleClick && !cursor.dialog && cam != null ) {
                
				UpdateTargetPosition();
			}
		}

		public void UpdateTargetPosition () {
			Vector3 newPosition = Vector3.zero;
			bool positionFound = false;

			if (use2D) {
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity, mask)) {
                    //newPosition = cam.ScreenToWorldPoint(Input.mousePosition);

                    newPosition = cam.ScreenToWorldPoint(Input.mousePosition);

                    //new
                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = 10; // select distance = 10 units from the camera


                    newPosition = cam.ScreenToWorldPoint(mousePos);
                    //end new
                    newPosition.z = 0;
                    positionFound = true;


                }

			} else {
				// Fire a ray through the scene at the mouse position and place the target where it hits
				RaycastHit hit;
				if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, mask) && !cam.GetComponent<ChangeCursor>().dialog) {


					newPosition = hit.point;
					positionFound = true;
                   // cam.GetComponent<ChangeCursor>().TheClicks();
				}
			}

			if (positionFound && newPosition != target.position) {
                if (cursor.canWalk == true)
                {
                   // Debug.Log("is walkable");


                    target.position = newPosition;
                }

				if (onlyOnDoubleClick) {
					for (int i = 0; i < ais.Length; i++) {
						if (ais[i] != null) ais[i].SearchPath();
					}
				}
			}
		}
	}
}
