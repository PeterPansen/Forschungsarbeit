using UnityEngine;
using System.Collections;

namespace FAR{

	public class FileBrowserEvent : InteractionEvent {
	
		private string m_path;
		
		private Enums.SL_State m_ExpImp;
		
		public string Path
		{
			get	{	return m_path;	}
		}
		
		public Enums.SL_State ExpImp
		{
			get	{	return m_ExpImp;	}	
		}
		
		
		public FileBrowserEvent ( string PathWay, Enums.SL_State ImportExport ,ulong ts , InteractionMethod sender, InteractionEvent base_Evt) : base(ts,sender,base_Evt)
		{
			this.m_path = PathWay;
			this.m_ExpImp = ImportExport;	
		}
	}

}
