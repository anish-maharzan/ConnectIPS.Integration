using SAPbouiCOM.Framework;

namespace ConnectIPS.Integration.Helpers
{
    public abstract class B1SystemFormBase : SystemFormBase
    {
        #region Members
        private bool CloseEventInitialized = false;
        private event SAPbouiCOM._IApplicationEvents_MenuEventEventHandler _MenuEvent;
        private event SAPbouiCOM._IApplicationEvents_ItemEventEventHandler _ItemEvent; 
        #endregion

        #region Events
        private void InitiliazeCloseAfterEvent()
        {
            if (!CloseEventInitialized)
            {
                this.CloseAfter += B1SystemFormBase_CloseAfter;
                CloseEventInitialized = true;
            }
        }
        protected void B1SystemFormBase_CloseAfter(SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (_MenuEvent != null)
                Application.SBO_Application.MenuEvent -= _MenuEvent;
            if (_ItemEvent != null)
                Application.SBO_Application.ItemEvent -= _ItemEvent;
        }
        protected void AutoManageMenuEvent(SAPbouiCOM._IApplicationEvents_MenuEventEventHandler menuEvent)
        {
            _MenuEvent = menuEvent;
            Application.SBO_Application.MenuEvent += menuEvent;
            InitiliazeCloseAfterEvent(); ;
        }
        protected void AutoManageItemEvent(SAPbouiCOM._IApplicationEvents_ItemEventEventHandler itemEvent)
        {
            _ItemEvent = itemEvent;
            Application.SBO_Application.ItemEvent += itemEvent;
            InitiliazeCloseAfterEvent();
        } 
        #endregion
    }
}
