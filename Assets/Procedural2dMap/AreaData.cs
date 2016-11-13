using System;
using System.Collections.Generic;
using UnityEngine;
namespace NG
{

	[Serializable] // Serialized so it will show in Unity's inspector.
	public class AreaData
	{
		[HideInInspector]
		public GameObject representation;

		public string name;
		
		/// <summary>
		/// The transitions that are available on this Area
		/// </summary>
		public Direction[] availableTransitions;

		public Coordinates coordinates;

		/// <summary>
		/// The transitions that have been connected to.
		/// </summary>
		public Dictionary<Direction, KeyValuePair<AreaData, Direction>> transitions =
			new Dictionary<Direction, KeyValuePair<AreaData, Direction>>();

		/// <summary>
		/// Constructor with the minimum required values to set up the properties.
		/// </summary>
		/// <param name="name">The name of the area</param>
		/// <param name="availableTransitions">
		/// A list of transitions this area has. This list is copied.
		/// </param>
		public AreaData(string name, Direction[] availableTransitions)
		{
			this.name = name;

			// We need to make a copy of this array since it will be supplied from
			// a template AreaData and we don't want to affect the original.
			this.availableTransitions = new Direction[availableTransitions.Length];
			availableTransitions.CopyTo(this.availableTransitions, 0);
		}


		/// <summary>
		/// When the transition has been used we will add it to the transitions list.
		/// </summary>
		/// <param name="thisAreasTransition">
		/// The transition on this area that is being used up.
		/// </param>
		/// <param name="areaConnectedTo">
		/// The area that this transition connects to.
		/// </param>
		/// <param name="areasConnection">
		/// The connection on the area that this transition connects to (should always be opposite).
		/// </param>
		public void SetTransitionUsed(
			Direction thisAreasTransition, AreaData areaConnectedTo, Direction areasConnection)
		{
			if (transitions == null)
				transitions = new Dictionary<Direction, KeyValuePair<AreaData, Direction>>();

			for (int i = 0; i < availableTransitions.Length; i++)
			{
				if (availableTransitions[i] == thisAreasTransition)
					availableTransitions[i] = Direction.None;
			}

			if (!transitions.ContainsKey(thisAreasTransition))
			{
				transitions.Add(
					thisAreasTransition,
					new KeyValuePair<AreaData, Direction>(areaConnectedTo, areasConnection));
			}
			else
				throw new Exception("transition: {0} has already been used!");
		}


		/// <summary>
		/// Returns a string representation of the suppied direction array.
		/// Used in generating the names of an AreaData.
		/// </summary>
		/// <param name="array">The array of Directions to convert to a string.</param>
		/// <returns>a string representation of the suppied direction array.</returns>
		public static string GetTransitionList(Direction[] array)
		{
			string s = "";
			for (int i = 0; i < array.Length; i++)
			{
				s += array[i].ToString() + " ";
			}

			return s;
		}


		/// <summary>
		/// How many transitions have been set up?
		/// </summary>
		/// <returns>The count of transitions that have been set on this AreaData.</returns>
		public int GetTransitionCount()
		{
			return transitions == null ? 0 : transitions.Count;
		}

		/// <summary>
		/// Tells us if the direction has been used or if it is available on this AreaData at all.
		/// </summary>
		/// <param name="direction">The direction to check.</param>
		/// <returns>
		/// True if the a tranisition in the Direction can be made, otherwise false.
		/// </returns>
		public bool GetIsTransitionAvailable(Direction direction)
		{
			bool isAvailable = false;
			for (int i = 0; i < availableTransitions.Length; i++)
			{
				if (availableTransitions[i] == direction)
				{
					isAvailable = true;
					break;
				}
			}

			if (!isAvailable)
				return false;

			if (transitions == null)
				transitions = new Dictionary<Direction, KeyValuePair<AreaData, Direction>>();

			foreach (var item in transitions)
			{
				if (item.Key == direction)
					return false;
			}

			return true;
		}


		/// <summary>
		/// Does this AreaData have ANY available transitions?
		/// </summary>
		/// <returns>True if there are available transitions, otherwise false.</returns>
		public bool HasAnyAvailableTransition()
		{
			int availableTransitionCount = 0;
			foreach (var item in availableTransitions)
			{
				if (item != Direction.None)
					availableTransitionCount++;
			}


			return availableTransitionCount > (transitions == null ? 0 : transitions.Count);
		}


		/// <summary>
		/// A user-friendly string representation of this AreaData.
		/// </summary>
		/// <returns>A user-friendly string representation of this AreaData.</returns>
		public override string ToString()
		{
			string s = "name : " + name + "\n";

			s += "availableTransitions: \n";
			foreach (var item in availableTransitions)
			{
				s += "\t" + item.ToString() + "\n";
			}

			s += "coordinate: " + coordinates.ToString() + "\n";

			s += "transitions: \n";

			if (transitions == null)
				transitions = new Dictionary<Direction, KeyValuePair<AreaData, Direction>>();

			foreach (var item in transitions)
			{
				s += "\tDirection: " + item.Key.ToString() +
					"  Area: " + item.Value.Key.name + "  Connection: " + item.Value.Value.ToString() + "\n";
			}


			return s;
		}
	}

}