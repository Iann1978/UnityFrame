local GameObject = UnityEngine.GameObject

PanelMain = {};
local this = PanelMain;

function PanelMain.OnButtonSettingClick()
    -- self.gameObject:SetActive(false)    
    local frame = Assets.Script.Frame
    local panelMgr = frame.PanelManager.me
    local panel = panelMgr:Get(3)
    panel:Show()
    this.gameObject:SetActive(false)
end