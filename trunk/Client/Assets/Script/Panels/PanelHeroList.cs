using UnityEngine;
using System.Collections;
using Assets.Script.Frame;
using UnityEngine.UI;


public class PanelHeroList : PanelBase {

    RectTransform HeroItem;
    //public RectTransform Content;
    RectTransform OwnedHeros;
    RectTransform NotOwnedHeros;
    ScrollRect ScrollView;
    int ownedHeroCount = 0;
    int notOwnedHeroCount = 0;

    public override void Init()
    {
        base.Init();
        HeroItem = controls.GetObject("HeroItem").GetComponent<RectTransform>();
        OwnedHeros = controls.GetObject("OwnedHeros").GetComponent<RectTransform>();
        NotOwnedHeros = controls.GetObject("NotOwnedHeros").GetComponent<RectTransform>();
        ScrollView = controls.GetObject("ScrollView").GetComponent<ScrollRect>();
        HeroItem.gameObject.SetActive(false);
        //Content = controls.GetObject("")
    }

    HeroItem AddHero(ref RectTransform panel, ref int counter)
    {
        counter++;
        int lines = (counter + 1) / 2;
        GridLayoutGroup glg = panel.GetComponent<GridLayoutGroup>();
        float height = HeroItem.sizeDelta.y * lines + (lines - 1) * glg.spacing.y + glg.padding.top + glg.padding.bottom;
        RectTransform newItem = Instantiate<RectTransform>(HeroItem);
        newItem.SetParent(panel);
        newItem.gameObject.SetActive(true);
        panel.sizeDelta = new Vector2(panel.sizeDelta.x, height);
        return newItem.GetComponent<HeroItem>();
    }


    public void OnButtonComboSkillClick()
    {
        //AddHero();
    }

    public void OnButtonCloseClick()
    {
        Hide();
    } 


    public override void Refresh()
    {
        base.Refresh();
        foreach (HeroInfo hero in Gamedata.me.userinfo.HeroList)
        {
            HeroItem heroItem = AddHero(ref OwnedHeros, ref ownedHeroCount);
            heroItem.SetHeroInfo(hero);
        }

        HeroTableData[] heros = Database.me.GetAll<HeroTableData>();
        foreach (HeroTableData hero in heros)
        {
            HeroItem heroItem = AddHero(ref NotOwnedHeros, ref notOwnedHeroCount);
            heroItem.SetHeroData(hero);
        }
    }



}
