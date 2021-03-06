﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace JWF
{
	public class JWFMenuStart : JWFMenuBase
	{
		// Set in inspector.
		public GameObject[] StartMenuButtons;

		private JWFMenuManager _MenuManager;
		private enum JWFStartMenuState
		{
			Options = 0,
			Play = 1,
			Quit = 2
		}
		private List<JWFMenuBase> _StartMenuStates = new List<JWFMenuBase>();
		private JWFStartMenuState _State = JWFStartMenuState.Play;

		void Start()
		{
			_StartMenuStates.Add( gameObject.GetComponent<JWFMenuOptions>() ); // 0 
			_StartMenuStates.Add( gameObject.GetComponent<JWFMenuLobby>() ); // 1
			_MenuManager = gameObject.GetComponent<JWFMenuManager>();
			iTween.ScaleTo( StartMenuButtons[(int) _State], Vector3.one * _ButtonScaleUp, _ButtonScaleTime );
		}

		public override Vector3 GetCameraPosition()
		{
			return new Vector3( 0, 0.71f, -10.36f );
		}

		public override JWFMenuState GetState()
		{
			return JWFMenuState.MenuStart;
		}

		public override void EnterPressed()
		{
			_MenuManager.ChangeStateTo( _StartMenuStates[(int) _State] );
		}

		public override void LeftPressed()
		{
			// Returns the button the left. If left most, then return itself.
			if ( _State == 0 ) return;
			iTween.ScaleTo( StartMenuButtons[(int) _State], Vector3.one * _ButtonScaleDown, _ButtonScaleTime );
			--_State;
			iTween.ScaleTo( StartMenuButtons[(int) _State], Vector3.one * _ButtonScaleUp, _ButtonScaleTime );
		}

		public override void RightPressed()
		{
			if ( (int) _State >= System.Enum.GetNames( typeof( JWFStartMenuState ) ).Length - 1 ) return;
			iTween.ScaleTo( StartMenuButtons[(int) _State], Vector3.one * _ButtonScaleDown, _ButtonScaleTime );
			++_State;
			iTween.ScaleTo( StartMenuButtons[(int) _State], Vector3.one * _ButtonScaleUp, _ButtonScaleTime );
		}
	}
}
