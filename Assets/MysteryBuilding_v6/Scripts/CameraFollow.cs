/*

The MIT License (MIT)

Copyright (c) 2015-2017 Secret Lab Pty. Ltd. and Yarn Spinner contributors.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

using UnityEngine;
using System.Collections;

namespace Yarn.Unity.Example {

    /// Control the position of the camera and its behaviour
    /** Camera should have minPosition and maxPosition of the
     * same because we're dealing with 2D. The movement speed
     * shouldn't be too fast nor too slow
     */
    public class CameraFollow : MonoBehaviour {

        /// Target of the camera
         Transform target;

        /// Minimum position of camera
        public float minXPosition = -5.3f;

        /// Maximum position of camera
        public float maxXPosition = 5.3f;

        /// Minimum position of camera
        public float minYPosition = 1.5f;

        /// Maximum position of camera
        public float maxYPosition = 1.5f;

        /// Movement speed of camera
        public float moveSpeed = 1.0f;
        public float yoffset;


         void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            yoffset = transform.position.y - target.transform.position.y;
        }

        // Update is called once per frame
        void Update () {
            if (target == null) {
                return;
            }
            var newPosition = Vector3.Lerp(transform.position, target.position, moveSpeed * Time.deltaTime);

            newPosition.x = Mathf.Clamp(newPosition.x, minXPosition, maxXPosition);
            newPosition.y = Mathf.Clamp(newPosition.y + yoffset, minYPosition, maxYPosition) ;
            newPosition.z = transform.position.z;

            transform.position = newPosition;
        }
    }
}

