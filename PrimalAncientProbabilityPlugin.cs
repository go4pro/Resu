// https://github.com/User5981/Resu
// Primal Ancient Probability Plugin for TurboHUD Version 27/01/2018 07:24

using System;
using System.Globalization;
using Turbo.Plugins.Default;
using System.Linq;

namespace Turbo.Plugins.Resu
{
    public class PrimalAncientProbabilityPlugin : BasePlugin, IInGameTopPainter
    {
      
        public TopLabelDecorator ancientDecorator { get; set; }
		public TopLabelDecorator primalDecorator { get; set; }
		public string ancientText{ get; set; }
        public string primalText{ get; set; }
		public double ancientMarker{ get; set; }
        public double primalMarker{ get; set; }
		public double legendaryCount{ get; set; }
		public double legendaryDrop { get; set; }
		public double ancientDrop { get; set; }
		public double primalDrop { get; set; }
        		
        public PrimalAncientProbabilityPlugin()
        {
            Enabled = true;
	    }
		
		public override void Load(IController hud)
        {
            base.Load(hud);
           	ancientText = "";
			primalText = "";
			ancientMarker = 0;
			primalMarker = 0;
			legendaryCount = 0;
			legendaryDrop = 0;
			ancientDrop = 0;
			primalDrop = 0;
			
		}

		public void PaintTopInGame(ClipState clipState)
        {
           		
			if (Hud.Render.UiHidden) return;
            if (clipState != ClipState.BeforeClip) return;

             ancientDecorator = new TopLabelDecorator(Hud)
            {
                 TextFont = Hud.Render.CreateFont("arial", 7, 220, 227, 153, 25, true, false, 255, 0, 0, 0, true),
                 TextFunc = () => ancientText,
                 HintFunc = () => "Chance for the next Legendary drop to be Ancient." 
              
            };
			
			 primalDecorator = new TopLabelDecorator(Hud)
            {
                 TextFont = Hud.Render.CreateFont("arial", 7, 180, 255, 64, 64, true, false, 255, 0, 0, 0, true),
                 TextFunc = () => primalText,
                 HintFunc = () => "Chance for the next Legendary drop to be Primal Ancient." 
              
            };
			
			if (Hud.Game.CurrentAccountTodayOnCurrentDifficulty.DropLegendary != legendaryDrop)
			   {
				 if (Hud.Game.CurrentAccountTodayOnCurrentDifficulty.DropLegendary != legendaryDrop + 1) legendaryDrop = Hud.Game.CurrentAccountTodayOnCurrentDifficulty.DropLegendary;
				 else
				 {
				   legendaryDrop = Hud.Game.CurrentAccountTodayOnCurrentDifficulty.DropLegendary;
				   legendaryCount++;
			     }
			   }
			   
            if (Hud.Game.CurrentAccountTodayOnCurrentDifficulty.DropAncient != ancientDrop)
			   {
				 if (Hud.Game.CurrentAccountTodayOnCurrentDifficulty.DropAncient != ancientDrop + 1) ancientDrop = Hud.Game.CurrentAccountTodayOnCurrentDifficulty.DropAncient;
				 else
				 {
				   legendaryDrop = Hud.Game.CurrentAccountTodayOnCurrentDifficulty.DropLegendary;
				   ancientDrop = Hud.Game.CurrentAccountTodayOnCurrentDifficulty.DropAncient;
				   legendaryCount++;
				   ancientMarker = legendaryCount;
			     }
			   }

             if (Hud.Game.CurrentAccountTodayOnCurrentDifficulty.DropPrimalAncient != primalDrop)
			   {
				 if (Hud.Game.CurrentAccountTodayOnCurrentDifficulty.DropPrimalAncient != primalDrop + 1) primalDrop = Hud.Game.CurrentAccountTodayOnCurrentDifficulty.DropPrimalAncient;
				 else
				 {
				   legendaryDrop = Hud.Game.CurrentAccountTodayOnCurrentDifficulty.DropLegendary;
				   primalDrop = Hud.Game.CurrentAccountTodayOnCurrentDifficulty.DropPrimalAncient;
				   legendaryCount++;
				   primalMarker = legendaryCount;
			     }
			   }

			double probaAncient = 0;
			double probaPrimal = 0;
			double powAncient = legendaryCount-ancientMarker;
			double powPrimal = legendaryCount-primalMarker;
			double ancientMaths = 90.00/100;
			double primalMaths = 99.78/100;
						
			if (powAncient == 0) powAncient = 1;
			if (powPrimal == 0) powPrimal = 1;
			
			
			
			probaAncient = (1 - Math.Pow(ancientMaths, powAncient))*100;
			probaPrimal = (1 - Math.Pow(primalMaths, powPrimal))*100;
			
			   
			probaAncient = Math.Round(probaAncient, 2);   
			probaPrimal = Math.Round(probaPrimal, 2);   
	
			
			ancientText = "A: " + probaAncient  + "%";
			primalText =  "P: " + probaPrimal  + "%";

            var uiRect = Hud.Render.GetUiElement("Root.NormalLayer.game_dialog_backgroundScreenPC.game_progressBar_manaBall").Rectangle;
			
            ancientDecorator.Paint(uiRect.Left - (uiRect.Width/3f), uiRect.Bottom - (uiRect.Height/5.7f), 50f, 50f, HorizontalAlign.Left);
            primalDecorator.Paint(uiRect.Left + (uiRect.Width/10f), uiRect.Bottom - (uiRect.Height/5.7f), 50f, 50f, HorizontalAlign.Left);

        }
    }
}