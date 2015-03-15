using UnityEngine;
using System.Collections;
using Enums;

namespace FAR{

    public class MouseEvent : InteractionEvent {
    
    	/*
    		Passes MouseClick-Information
    		Such as x- and y-Pos of the cursor on screen while clicking
    		The pressed MouseButton (Left,Right,Wheel)
    		And the ButtonState (Up,Down) (Is it pressed? Down. Is it not? Up)
    	*/
    
    	private float m_xPos;
    	private float m_yPos;
    	private MouseButton m_MouseButton;
    	private MouseState m_UpDown;
    
    	
    	public float xPos
    	{
    		get { return m_xPos; }
    	}
    	
    	public float yPos
    	{
    		get { return m_yPos; }
    	}
    	
    	public MouseButton mouseButton
    	{
    		get { return m_MouseButton; }
    	}
    	
    	public MouseState UpDown
    	{
    		get { return m_UpDown; }
    	}
    	
    	
    	public MouseEvent ( float xPos, float yPos, MouseButton mButton , MouseState UpDown , ulong ts, InteractionMethod sender, InteractionEvent base_evt ) : base(ts,sender,base_evt)
    	{
    		this.m_xPos = xPos;
    		this.m_yPos = yPos;
    		this.m_MouseButton = mButton;
    		this.m_UpDown = UpDown;
    	}
    	
    }

}
