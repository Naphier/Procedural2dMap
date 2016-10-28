using UnityEditor;

namespace NG
{
	[CustomEditor(typeof(Area))]
	public class AreaInspector : Editor
	{
		private Area _area;

		public override void OnInspectorGUI()
		{
			_area = (Area)target;

			if (_area.areaData != null)
				EditorGUILayout.HelpBox("AreaData: \n" + _area.areaData.ToString(), MessageType.None, true);
			else
				EditorGUILayout.HelpBox("Area data is null", MessageType.Error);
		}
	}
}