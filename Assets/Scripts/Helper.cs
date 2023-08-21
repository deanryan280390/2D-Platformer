using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public static class Helper
    {
        /// <summary>
        /// Global function that will create an GameObject in the scene from resources folder
        /// Creates game object, sets the name, sets the position,adds to a list if list is not null
        /// returns game object
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="objectList"></param>
        /// <returns></returns>
        public static GameObject CreateGameObject(string path, string name, Vector2 position, ref List<GameObject> objectList)
        {
            var createdObject = Object.Instantiate(Resources.Load(path, typeof(GameObject))) as GameObject;
            createdObject.name = name;
            createdObject.transform.position = position;
            objectList?.Add(createdObject);
            return createdObject;
        }

    }
}