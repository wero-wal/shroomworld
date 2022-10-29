using System;
using System.Collections.Generic;
using System.Text;

namespace MyPhysics
{
	public class Body
	{

		public Vector Force
		{
			get => _force;
			internal set
			{
				_force = value;
				OnForceChanged();
				return;
			}
		}
		public Vector Velocity { get => _velocity; internal set => _velocity = value; }
		public Vector Position { get => _position; internal set => _position = value; }
		public float Mass => _mass;
		public Shape Shape => _shape;
		public float E => _constantOfRestitution;


		private Shape _shape;
		private float _constantOfRestitution; // 0 <= e <= 1
		private Vector _force; // in N
		private Vector _velocity; // in m/s
		private Vector _position; // in m
		private readonly float _mass; // in kgs

		public void SetVelocity(Vector newVelocity)
		{
			if (!WorldSettings.AllowBreakingLawsOfPhysics)
			{
				throw new InvalidOperationException("You are attempting to break the laws of physics!");
			}
			else
			{
				_velocity = newVelocity;
			}
		}

		private void OnForceChanged()
		{
			_velocity += _force * (1 / _mass); // f = ma => a = f / m
			_position += _velocity;
		}
	}
}
