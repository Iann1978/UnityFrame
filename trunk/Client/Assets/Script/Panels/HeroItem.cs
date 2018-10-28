using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script.Frame;
using UnityEngine.UI;

public class HeroItem : PanelBase
{
    
    Image ImageHeroHead;
    Image ImageHeroQuality;
    Image ImageHeroOccupation;
    Text TextHeroName;
    Text TextHeroLevel;
    Text TextChips;
    Text HeroLevel;
    Slider SliderChips;



    public void GetControls()
    {
        ImageHeroHead = GetImage("ImageHeroHead");
        ImageHeroQuality = GetImage("ImageHeroQuality");
        ImageHeroOccupation = GetImage("ImageHeroOccupation");
        HeroLevel = GetText("HeroLevel");
        TextHeroName = GetText("TextHeroName");
        TextHeroLevel = GetText("TextHeroLevel");
        TextChips = GetText("TextChips");
        SliderChips = controls.GetSlider("SliderChips");


        //txCoin = GetText("TextCoin");
        //txDiamond = GetText("TextDiamond");
        //txVigour = GetText("TextVigour");
    }

    public void SetHeroInfo(HeroInfo heroInfo)
    {
        GetControls();
        int heroResData = heroInfo.Id & 0xffff;
        HeroTableData heroTableData = Database.me.GetById<HeroTableData>(heroResData);
        ImageHeroHead.sprite = Assets.Script.SpriteFactory.me.Get("HeroHead", heroResData);
        ImageHeroQuality.sprite = Assets.Script.SpriteFactory.me.Get("HeroQuality", heroInfo.Quality);
        ImageHeroOccupation.sprite = Assets.Script.SpriteFactory.me.Get("HeroOccupation", heroTableData.HeroOccupation);
        TextHeroName.text = heroTableData.HeroName;
        TextHeroLevel.text = "Lv." + heroInfo.Level.ToString();
        HeroLevel.gameObject.SetActive(true);
        SliderChips.gameObject.SetActive(false);
    }

    public void SetHeroData(HeroTableData heroData)
    {
        GetControls();
        HeroLevel.gameObject.SetActive(false);
        SliderChips.gameObject.SetActive(true);
        ImageHeroHead.sprite = Assets.Script.SpriteFactory.me.Get("HeroHead", heroData.Id);
        ImageHeroQuality.sprite = Assets.Script.SpriteFactory.me.Get("HeroQuality", 1);
        ImageHeroOccupation.sprite = Assets.Script.SpriteFactory.me.Get("HeroOccupation", heroData.HeroOccupation);
        TextHeroName.text = heroData.HeroName;
    }

 
}
