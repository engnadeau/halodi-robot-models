﻿/*
© Halodi Robotics AS, 2019
Author: Jesper Smith (jesper@halodi.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

<http://www.apache.org/licenses/LICENSE-2.0>.

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System.IO;
using UnityEditor;
using UnityEngine;

namespace Halodi.RobotModels
{
    public class OBJAssetPostProcessor : AssetPostprocessor
    {
        private bool isObj;

        public void OnPreprocessModel()
        {
            ModelImporter modelImporter = (ModelImporter)assetImporter;
            isObj = Path.GetExtension(modelImporter.assetPath).ToLowerInvariant() == ".obj";

            if (!isObj)
                return;

        }
        

        public void OnPostprocessModel(GameObject gameObject)
        {
            if (!isObj)
                return;
                

            gameObject.transform.SetPositionAndRotation(
                getPositionFix(gameObject.transform.position),
                Quaternion.Euler(getRotationFix()) * gameObject.transform.rotation);

        }

        public Vector3 getPositionFix(Vector3 position)
        {
            return new Vector3(-position.z, position.y, -position.x);
        }

        
        private static Vector3 getRotationFix()
        {
            return new Vector3(-90, 90, 0);              
        }

    }
}