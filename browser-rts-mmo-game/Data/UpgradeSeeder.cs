using BrowserGame.Data;
using BrowserGame.Models;
using BrowserGame.Utilities;

namespace BrowserGame.Data
{
    public class UpgradeSeeder
    {
        private readonly ApplicationDbContext _context;

        public UpgradeSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task GenerateAsync(UpgradeSeederSettings upgradeSettings)
        {
            var upgradeNew = new Upgrade()
            {
                BuildingName = upgradeSettings.BuildingName,
                ClayCost = upgradeSettings.Clay,
                IronCost = upgradeSettings.Iron,
                WoodCost = upgradeSettings.Wood,
                CropCost = upgradeSettings.Crop,
                Level = 0,
                AdditionalCropUpkeep = 0,
                IsFinnalUpgrade = upgradeSettings.FinalLevel == 0,
                UpgradeDuration = upgradeSettings.BuildTime,
                ValueChangeDecimal = upgradeSettings.Value
            };
            if (upgradeSettings.UseFixedValue) upgradeNew.ValueChangeDecimal = upgradeSettings.ValueFixedChange;

            _context.Upgrades.Add(upgradeNew);

            for (int level = 1; level <= upgradeSettings.FinalLevel; ++level)
			{
                Upgrade upgradeOld = upgradeNew;

                decimal costFactor = 
                    upgradeSettings.CostScaling * 
                    (decimal)Math.Pow((double)upgradeSettings.CostScalingDelta, level);
                decimal upgradeFactor =
                    upgradeSettings.TimeScaling *
                    (decimal)Math.Pow((double)upgradeSettings.TimeScalingDelta, level);

                upgradeNew = new()
                {
                    BuildingName = upgradeSettings.BuildingName,
                    ClayCost = Convert.ToInt32(upgradeOld.ClayCost * costFactor),
                    IronCost = Convert.ToInt32(upgradeOld.IronCost * costFactor),
                    WoodCost = Convert.ToInt32(upgradeOld.WoodCost * costFactor),
                    CropCost = Convert.ToInt32(upgradeOld.CropCost * costFactor),
                    Level = level,
                    AdditionalCropUpkeep = level / 10,
                    IsFinnalUpgrade = level == upgradeSettings.FinalLevel,
                    UpgradeDuration = Convert.ToInt32(upgradeOld.UpgradeDuration * upgradeFactor),
                };

                if (upgradeSettings.UseFixedValue) upgradeNew.ValueChangeDecimal = upgradeSettings.ValueFixedChange;
                else
				{
                    decimal valueFactor = 
                        upgradeSettings.ValueScaling * 
                        (decimal)Math.Pow((double)upgradeSettings.ValueScalingDelta, level);

                    upgradeNew.ValueChangeDecimal = Convert.ToInt32(upgradeOld.ValueChangeDecimal * valueFactor);
                }

                _context.Upgrades.Add(upgradeNew);
			}

            await _context.SaveChangesAsync();
        }
    }
}
