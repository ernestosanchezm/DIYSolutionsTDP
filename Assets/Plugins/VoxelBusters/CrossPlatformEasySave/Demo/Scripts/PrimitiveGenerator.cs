using UnityEngine;
using System.Collections.Generic;

namespace VoxelBusters.Serialization.Demo
{
	public class PrimitiveGenerator : MonoBehaviour 
	{
		#region Properties

		private 				List<GameObject>		m_spawnedGOList;
		[SerializeField]
		private					List<GameObject>		m_prefabs;

		#endregion

		#region Methods

		public void SpawnNewObject ()
		{
			if (m_spawnedGOList == null)
			{
				m_spawnedGOList = new List<GameObject>();
			}

			// Instantiate
			int 		randomIndex		= Random.Range(0, m_prefabs.Count);
			GameObject 	newCloneGO		= SerializationUtility.Instantiate(m_prefabs[randomIndex]);

			// Create clone in random position with random rotation
			Vector3		randPosition	= Random.insideUnitSphere * 5f;
			randPosition.y				= randPosition.y * 0.5f + 4.5f;

			Transform 	newTransform	= newCloneGO.transform;
			newTransform.parent			= transform;
			newTransform.localPosition	= randPosition;
			newTransform.localRotation	=  Random.rotationUniform;

			// Add it to the list					
			m_spawnedGOList.Add(newCloneGO);
		}

		public void Serialize ()
		{
			SerializationManager.Serialize<List<GameObject>>("primitive-generator", m_spawnedGOList);
		}

		public void Deserialize ()
		{
			Clear();
			m_spawnedGOList = SerializationManager.Deserialize<List<GameObject>>("primitive-generator");
		}

		public void Clear ()
		{
			if (m_spawnedGOList != null)
			{
				foreach (GameObject spawnedGO in m_spawnedGOList)
				{
					SerializationUtility.Destroy(spawnedGO);
				}
				m_spawnedGOList.Clear();
			}
		}

		#endregion
	}
}