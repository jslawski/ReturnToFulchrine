using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerControls
{
	public TwoAxisInputControl movementControl;
	public TwoAxisInputControl rotateControl;
	public TwoAxisInputControl characterSelectControl;
	public InputControl characterSelectLeft;
	public InputControl characterSelectRight;
	public InputControl attackButton;
	public InputControl interactButton;

	//Initialize PlayerControls stuct with default controls
	public PlayerControls(InputDevice device)
	{
		this.movementControl = device.LeftStick;
		this.rotateControl = device.LeftStick;
		this.characterSelectControl = device.RightStick;
		this.characterSelectLeft = device.LeftBumper;
		this.characterSelectRight = device.RightBumper;
		this.attackButton = device.Action1;
		this.interactButton = device.Action3;
	}

	public PlayerControls(TwoAxisInputControl movementControl, TwoAxisInputControl rotateControl, TwoAxisInputControl characterSelectControl, InputControl characterSelectLeft, InputControl characterSelectRight, InputControl attackButton, InputControl interactButton)
	{
		this.movementControl = movementControl;
		this.rotateControl = rotateControl;
		this.characterSelectControl = characterSelectControl;
		this.characterSelectLeft = characterSelectLeft;
		this.characterSelectRight = characterSelectRight;
		this.attackButton = attackButton;
		this.interactButton = interactButton;
	}

	public void DisableControls()
	{
		this.movementControl = null;
		this.rotateControl = null;
		this.characterSelectControl = null;
		this.characterSelectLeft = null;
		this.characterSelectRight = null;
		this.attackButton = null;
		this.interactButton = null;
	}
}